using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonContinue;

    [Header("Title")]
    [SerializeField] private Transform title;
    [SerializeField] private Transform target;
    [Header("MoveTitle")]
    [SerializeField] private Ease Ease;
    [SerializeField] private float timeMove;
    [Header("ScaleTitle")]
    [SerializeField] private float timeScale;
    [SerializeField] private Vector3 endScale;

    private Tween tween;
    private void OnEnable()
    {
        buttonPlay.onClick.AddListener(DeletePlayerPrefs);

    }
    private void OnDisable()
    {
        buttonPlay.onClick.RemoveListener(DeletePlayerPrefs);
    }
    private void Start()
    {
        buttonContinue.interactable = GameManager.instance.CountSweet > 0;
        MoveTitle();
    }
    private void OnDestroy()
    {
        tween.Kill();
    }
    private void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    private void MoveTitle()
    {
        tween = title.DOMove(target.position, timeMove);
        tween.SetEase(Ease);
        tween.OnComplete(ScaleTitle);
    }
    private void ScaleTitle()
    {
        tween = title.DOScale(endScale, timeScale);
        tween.SetEase(Ease.InOutSine);
        tween.SetLoops(-1, LoopType.Yoyo);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target.position, 1);
    }
#endif
}
