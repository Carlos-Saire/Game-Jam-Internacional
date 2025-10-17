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
        private Queue<Vector2> positionQueue = new Queue<Vector2>();
        private const int maxStoredPositions = 3;
        private Vector2 GetRandomPosition()
        {
            float randomX = Random.Range(xLimit.x, xLimit.y);
            Vector2 newPosition = new Vector2(randomX, transform.position.y);

            positionQueue.Enqueue(newPosition);

            if (positionQueue.Count > maxStoredPositions)
                positionQueue.Dequeue();

            return newPosition;
        }
    }
}

