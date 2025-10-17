using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Game1
{
    public class Spawner : StartableEntity
    {
        [Header("Spawnable Prefabs")]
        [SerializeField] private Transform[] goodObjects; 
        [SerializeField] private Transform[] badObjects; 

        [Header("Spawn Settings")]
        [SerializeField] private Vector2 xLimit;
        [SerializeField] private float spawnInterval = 2f;
        [SerializeField, Range(0f, 1f)] private float badObjectChance = 0.3f; 

        [Header("Position Control")]
        [SerializeField] private int maxStoredPositions = 3;
        [SerializeField] private float minDistance = 0.5f;

        private List<Vector2> recentPositions = new List<Vector2>();
        private bool firstSpawnDone = false;

        protected override void StartGame()
        {
            base.StartGame();
            Generate();
        }

        private void Generate()
        {
            Transform prefabToSpawn = GetRandomPrefab();
            Instantiate(prefabToSpawn, GetRandomPosition(), transform.rotation);

            Invoke("Generate", spawnInterval);
        }

        private Transform GetRandomPrefab()
        {
            if (!firstSpawnDone)
            {
                firstSpawnDone = true;
                return goodObjects[Random.Range(0, goodObjects.Length)];
            }

            bool spawnBad = Random.value < badObjectChance;

            if (spawnBad && badObjects.Length > 0)
                return badObjects[Random.Range(0, badObjects.Length)];

            return goodObjects[Random.Range(0, goodObjects.Length)];
        }

        private Vector2 GetRandomPosition()
        {
            float randomX;
            int attempts = 0;
            const int maxAttempts = 10;

            Vector2 newPos;
            do
            {
                randomX = Random.Range(xLimit.x, xLimit.y);
                newPos = new Vector2(randomX, transform.position.y);
                attempts++;
            }
            while (IsTooClose(newPos) && attempts < maxAttempts);

            if (recentPositions.Count >= maxStoredPositions)
                recentPositions.RemoveAt(0);

            recentPositions.Add(newPos);

            return newPos;
        }

        private bool IsTooClose(Vector2 newPos)
        {
            foreach (var pos in recentPositions)
            {
                if (Vector2.Distance(pos, newPos) < minDistance)
                    return true;
            }
            return false;
        }
    }
}
