using DG.Tweening;
using UnityEngine;

public class ButtonsMoveCandy : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private Vector2 rotationRange = new Vector2(-1f, 1f);
    private RectTransform rectTransform;

    [SerializeField] private RectTransform target;          
    [SerializeField] private float duration = 2f;       
    [SerializeField] private float zigzagSpeed = 5f;    
    [SerializeField] private float amplitude = 1f;

    private Tween moveY;
    private Tween moveX;
    private Tween dance;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        MoveZigZag();
    }
    private void OnDestroy()
    {
        moveY.Kill(); 
        moveX.Kill();
        dance.Kill();
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target.position, 1);
    }
#endif
    private void MoveZigZag()
    {
        Vector3 startPos = rectTransform.position;
        Vector3 endPos = target.position;

        moveY = rectTransform.DOMoveY(endPos.y, duration);
        moveY.SetEase(Ease.Linear);

        int loopCount = Mathf.Max(1, Mathf.RoundToInt(zigzagSpeed * duration));

        moveX = rectTransform.DOMoveX(startPos.x + amplitude, duration / (loopCount * 2));
        moveX.SetEase(Ease.InOutSine);
        moveX.SetLoops(loopCount * 2, LoopType.Yoyo);

        DOTween.Sequence().Join(moveY).Join(moveX).OnComplete(Dance);
    }
    private void Dance()
    {
        MoveLeft();
    }
    private void MoveLeft()
    {
        dance = rectTransform.DORotate(new Vector3(0, 0, rotationRange.y), rotationSpeed, RotateMode.Fast);
        dance.SetEase(Ease.Linear);
        dance.SetUpdate(true);
        dance.OnComplete(MoveRight);
    }
    private void MoveRight()
    {
        dance = rectTransform.DORotate(new Vector3(0, 0, rotationRange.x), rotationSpeed, RotateMode.Fast);
        dance.SetEase(Ease.Linear);
        dance.SetUpdate(true);
        dance.OnComplete(MoveLeft);
    }
}
