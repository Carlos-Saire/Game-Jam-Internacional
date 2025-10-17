using UnityEngine;
using DG.Tweening;
namespace Game1
{
    public class SweetGoodController : Items
    {
        [Header("Rotation Settings")]
        [SerializeField] private float rotationSpeed = 100f;
        private Sequence rotationSequence;
        private void Start()
        {
            CreateRotationSequence();
        }
        private void CreateRotationSequence()
        {
            float duration = 360f / rotationSpeed;

            rotationSequence = DOTween.Sequence();

            Tween rotateTween = transform.DORotate(
                new Vector3(0, 0, -360f),
                duration,
                RotateMode.FastBeyond360
            );

            rotateTween.SetEase(Ease.Linear);

            rotationSequence.Append(rotateTween);

            rotationSequence.SetLoops(-1, LoopType.Restart);
        }
        private void OnDestroy()
        {
            rotationSequence.Kill();
        }
        protected override void CollisionPlayer()
        {
            ActiveEventIncrmentScore();
            base.CollisionPlayer();
        }
    }

}

