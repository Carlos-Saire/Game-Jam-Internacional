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
        [SerializeField] private Transform[] powerUps;

        [Header("Spawn Settings")]
        [SerializeField] private Vector2 xLimit;
        [SerializeField] private float spawnInterval = 2f;

        [Header("Spawn Chances")]
        [SerializeField, Range(0f, 1f)] private float badObjectChance = 0.3f;
        [SerializeField, Range(0f, 1f)] private float powerUpChance = 0.1f;

        [Header("Position Control")]
        [SerializeField] private int maxStoredPositions = 3;
        [SerializeField] private float minDistance = 0.5f;

        [Header("Difficulty Settings")]
        [SerializeField] private float difficultyIncreaseRate = 0.05f; 
        [SerializeField] private float minSpawnInterval = 0.5f;        
        [SerializeField] private float maxBadChance = 0.8f;           
        [SerializeField] private float minPowerUpChance = 0.02f;       

        private List<Vector2> recentPositions = new List<Vector2>();
        private bool firstSpawnDone = false;
        private float difficultyMultiplier = 1f;

        protected override void StartGame()
        {
            base.StartGame();
            InvokeRepeating(nameof(IncreaseDifficulty), 5f, 5f); 
            Generate();
        }

        private void Generate()
        {
            Transform prefabToSpawn = GetRandomPrefab();
            Instantiate(prefabToSpawn, GetRandomPosition(), transform.rotation);

            Invoke(nameof(Generate), spawnInterval);
        }

        private Transform GetRandomPrefab()
        {
            if (!firstSpawnDone)
            {
                firstSpawnDone = true;
                return goodObjects[Random.Range(0, goodObjects.Length)];
            }

            float roll = Random.value;

            if (roll < powerUpChance && powerUps.Length > 0)
                return powerUps[Random.Range(0, powerUps.Length)];

            if (roll < powerUpChance + badObjectChance && badObjects.Length > 0)
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

        private void IncreaseDifficulty()
        {
            difficultyMultiplier += difficultyIncreaseRate;

            spawnInterval = Mathf.Max(spawnInterval - 0.1f * difficultyMultiplier, minSpawnInterval);

            badObjectChance = Mathf.Min(badObjectChance + 0.02f * difficultyMultiplier, maxBadChance);
            powerUpChance = Mathf.Max(powerUpChance - 0.01f * difficultyMultiplier, minPowerUpChance);
        }
    }
}
