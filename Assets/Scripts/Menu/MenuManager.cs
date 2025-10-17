using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using System.Collections;
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

    [Header("Light")]
    [SerializeField] private Light2D ligh2D;
    [SerializeField] private float timeEffettSun;

    [Header("SceneManager")]
    [SerializeField] private SceneManagerController scene;
    [SerializeField] private string nameSceneGame1;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    private Tween tween;
    private Tween tweenEffet;

    [Header("Panel")]
    [SerializeField] private Image image;
    [SerializeField] private float timeEffectFade;

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
        audioSource.Play();
        StartCoroutine(EffectSun());
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
    private IEnumerator EffectSun()
    {
        while (ligh2D.intensity <= 4)
        {
            ligh2D.intensity+= timeEffettSun*Time.deltaTime;
            yield return null;
        }
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        tweenEffet = image.DOFade(1, timeEffectFade);
        tweenEffet.OnComplete(LoadScene);
    }
    private void LoadScene()
    {
        scene.LoadScene(nameSceneGame1);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target.position, 1);
    }
#endif
}
