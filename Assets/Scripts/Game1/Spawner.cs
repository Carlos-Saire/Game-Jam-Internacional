using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Game1
{
    public class Spawner : StartableEntity
    {
        [Header("Objects")]
        [SerializeField] private Transform objecta;

        [Header("Spawn Limits")]
        [SerializeField] private Vector2 xLimit;

        private List<Vector2> recentPositions = new List<Vector2>();
        private const int maxStoredPositions = 3;
        private const float minDistance = 0.5f; 

        protected override void StartGame()
        {
            base.StartGame();
            Generate();
        }

        private void Generate()
        {
            Instantiate(objecta, GetRandomPosition(), transform.rotation);
            Invoke(nameof(Generate), 2f);
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
            for (int i = 0; i < recentPositions.Count; i++)
            {
                if (Vector2.Distance(recentPositions[i], newPos) < minDistance)
                    return true;
            }
            return false;
        }
    }
}
