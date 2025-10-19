using DG.Tweening;
using UnityEngine;

public class ButtonsMoveCandy : MonoBehaviour
{
    [Header("Target (anchored position in UI)")]
    [SerializeField] private Vector2 target;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 0.5f; 
    [SerializeField] private float rotationAngle = 15f; 

    [Header("Movement Settings")]
    [SerializeField] private float duration = 2f;        
    [SerializeField] private float zigzagSpeed = 5f;     
    [SerializeField] private float amplitude = 50f;     

    private RectTransform rectTransform;
    private Sequence moveSequence;

    [Header("Rotation Settings")]
    [SerializeField] private Vector2 rotationRange = new Vector2(-1f, 1f);
    private Tween tween;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        MoveZigZagAndDance();
    }

    private void OnDestroy()
    {
        moveSequence?.Kill();
        tween?.Kill();
    }

    private void MoveZigZagAndDance()
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 endPos = target;

        Tween moveY = rectTransform.DOAnchorPosY(endPos.y, duration)
            .SetEase(Ease.Linear)
            .SetUpdate(true);

        int loopCount = Mathf.Max(1, Mathf.RoundToInt(zigzagSpeed * duration));
        float singleXDuration = duration / (loopCount * 2f);

        Tween moveX = rectTransform.DOAnchorPosX(startPos.x + amplitude, singleXDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(loopCount * 2, LoopType.Yoyo)
            .SetUpdate(true);

        rectTransform.DOLocalRotate(new Vector3(0, 0, rotationAngle), rotationSpeed, RotateMode.Fast)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetUpdate(true);

        moveSequence = DOTween.Sequence();
        moveSequence
            .Join(moveY)
            .Join(moveX)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                rectTransform.DOKill();
                MoveLeft();
                rectTransform.localRotation = Quaternion.identity;
            });
    }
    private void MoveLeft()
    {
        tween = rectTransform.DORotate(new Vector3(0, 0, rotationRange.y), rotationSpeed, RotateMode.Fast);
        tween.SetEase(Ease.Linear);
        tween.SetUpdate(true);
        tween.OnComplete(MoveRight);
    }
    private void MoveRight()
    {
        tween = rectTransform.DORotate(new Vector3(0, 0, rotationRange.x), rotationSpeed, RotateMode.Fast);
        tween.SetEase(Ease.Linear);
        tween.SetUpdate(true);
        tween.OnComplete(MoveLeft);
    }
}
