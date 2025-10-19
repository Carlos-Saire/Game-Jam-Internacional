using UnityEngine;
using System.Collections;
namespace Game3
{
    public class Spawner : StartableEntity
    {
        public GameObject candyPrefab;
        public GameObject pumpkinPrefab;
        public float candySpawnInterval = 1.5f;
        public float pumpkinSpawnInterval = 8f;

        private Camera cam;
        private bool spawningStarted = false; 

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (!isStartGame) return;

            if (GameManagerLevel3.Instance != null &&
                GameManagerLevel3.Instance.gameStarted && !spawningStarted)
            {
                spawningStarted = true;
                StartCoroutine(SpawnCandies());
                StartCoroutine(SpawnPumpkins());
            }
        }

        private IEnumerator SpawnCandies()
        {
            for (int i = 0; i < GameManagerLevel3.Instance.totalCandies; i++)
            {
                if (GameManagerLevel3.Instance.gameEnded) yield break;

                SpawnItems(candyPrefab);
                yield return new WaitForSeconds(candySpawnInterval);
            }
        }

        private IEnumerator SpawnPumpkins()
        {
            while (!GameManagerLevel3.Instance.gameEnded)
            {
                yield return new WaitForSeconds(pumpkinSpawnInterval);
                SpawnItems(pumpkinPrefab);
            }
        }

        private void SpawnItems(GameObject prefab)
        {
            if (GameManagerLevel3.Instance.gameEnded) return;

            Vector2 spawn = GetRandomPosCam();
            Instantiate(prefab, spawn, Quaternion.identity);
        }

        private Vector2 GetRandomPosCam()
        {
            float margin = 1f; 

            float height = cam.orthographicSize;
            float width = height * cam.aspect;

            float x = Random.Range(-width + margin, width - margin);
            float y = Random.Range(-height + margin, height - margin);

            return new Vector2(x, y);
        }
    }
}
