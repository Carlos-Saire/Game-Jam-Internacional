using DG.Tweening;
using UnityEngine;

public class MoveButtons : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private Vector2 rotationRange = new Vector2(-10f, 10f);

    private Tween tween;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        MoveLeft();
    }

    private void OnDestroy()
    {
        tween.Kill();
    }
    private void MoveLeft()
    {
        tween = rectTransform.DORotate(new Vector3(0, 0, rotationRange.y), rotationSpeed,RotateMode.Fast);
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
