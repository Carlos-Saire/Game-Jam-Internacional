using Game1;
using UnityEngine;
using DG.Tweening;
namespace Game1
{
    public class SweetBadController : Items
    {
        [Header("Rotation Settings")]
        [SerializeField] private float rotationSpeed = 2f;     
        [SerializeField] private Vector2 rotationRange = new Vector2(-10f, 10f); 

        private Sequence rotationSequence;

        private void Start()
        {
            CreateRotationSequence();
        }

        private void CreateRotationSequence()
        {
            rotationSequence = DOTween.Sequence();

            Tween rotateRight = transform.DORotate(
                new Vector3(0, 0, rotationRange.y),
                rotationSpeed,
                RotateMode.Fast
            );

            Tween rotateLeft = transform.DORotate(
                new Vector3(0, 0, rotationRange.x),
                rotationSpeed,
                RotateMode.Fast
            );

            rotateRight.SetEase(Ease.Linear);
            rotateLeft.SetEase(Ease.Linear);

            rotationSequence.Append(rotateRight);
            rotationSequence.Append(rotateLeft);

            rotationSequence.SetLoops(-1, LoopType.Restart);
        }

        private void OnDestroy()
        {
            rotationSequence.Kill();
        }
    }
}

