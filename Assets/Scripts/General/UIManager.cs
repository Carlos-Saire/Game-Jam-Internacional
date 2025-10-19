using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using System;
public class UIManager : MonoBehaviour
{
    [Header("CanvasGroup")]
    [SerializeField] private CanvasGroup canvasGroupDialogue;

    [Header("DialoguesControllers")]
    [SerializeField] private DialogueController history;
    [SerializeField] private DialogueController introduccion;
    [SerializeField] private DialogueController win;
    [SerializeField] private DialogueController fail;

    [Header("LoadSceneManager")]
    [SerializeField] private SceneManagerController loadSceneManager;
    [SerializeField] private string scene;

    [Header("Buttons")]
    [SerializeField] private Button buttonPause;
    [SerializeField] private Button buttonDoubt;
    [SerializeField] private Button buttonSettings;

    [SerializeField] private Button buttonBackPause;
    [SerializeField] private Button buttonBackDoubt;
    [SerializeField] private Button buttonBackSettings;

    [Header("ButtonsReyCalabaza")]
    [SerializeField]private Button[] buttonsReboot;

    [Header("Panel")]
    [SerializeField] private RectTransform panelSettings;
    [SerializeField] private RectTransform panelPause;
    [SerializeField] private RectTransform panelDoubt;

    [Header("Ease")]
    [SerializeField] private Ease easeSettings;
    [SerializeField] private Ease easePause;
    [SerializeField] private Ease easeDoubt;

    [Header("Time")]
    [SerializeField] private float timeSettings;
    [SerializeField] private float timePause;
    [SerializeField] private float timeDoubt;

    [Header("InitPosition")]
    private Vector2 initPositionSettings;
    private Vector2 initPositionPause;
    private Vector2 initPositionDoubt;

    private bool isInteract;
    private Tweener tweener;
    private Tween tween;

    private float timeGame;

    private bool isGameStart;

    [SerializeField] private UnityEvent OnStartGame;
    [SerializeField] private UnityEvent OnFinishGame;
    [SerializeField] private UnityEvent OnWin;
    [SerializeField] private UnityEvent OnFail;
    private void OnEnable()
    {
        GameManager.OnGameWin += CheckWin;
        if (buttonPause != null&& buttonDoubt != null&& buttonSettings != null)
        {

            buttonPause.onClick.AddListener(OnClickPause);
            buttonDoubt.onClick.AddListener(OnClickDoubt);
            buttonSettings.onClick.AddListener(OnClickSettings);

            buttonBackDoubt.onClick.AddListener(OnClickBackDoubt);
            buttonBackPause.onClick.AddListener(OnClickBackPause);
            buttonBackSettings.onClick.AddListener(OnClickBackSettings);
        }
    }
    private void OnDisable()
    {
        GameManager.OnGameWin -= CheckWin;
        if (buttonPause != null && buttonDoubt != null && buttonSettings != null)
        {
            buttonPause.onClick.RemoveListener(OnClickPause);
            buttonDoubt.onClick.RemoveListener(OnClickDoubt);
            buttonSettings.onClick.RemoveListener(OnClickSettings);

            buttonBackDoubt.onClick.RemoveListener(OnClickBackDoubt);
            buttonBackPause.onClick.RemoveListener(OnClickBackPause);
            buttonBackSettings.onClick.RemoveListener(OnClickBackSettings);
        }
    }
    private void Start()
    {
        if (history != null)
        {
            history.GoDialogue(Explain);
        }
        else if(introduccion!=null)
        {
            introduccion.GoDialogue(Fade);
        }

        if (buttonPause != null && buttonDoubt != null && buttonSettings != null)
        {
            initPositionSettings = panelSettings.position;
            initPositionPause = panelPause.position;
            initPositionDoubt = panelDoubt.position;
        }
        CheckButtonReboot();
    }
    private void OnDestroy()
    {
        tweener.Kill();
    }
    private void Fade()
    {
        tweener = canvasGroupDialogue.DOFade(0, 1);
        tweener.OnComplete(StartGame);
    }
    private void StartGame()
    {
        OnClickDoubt();
        canvasGroupDialogue.interactable = false;
        canvasGroupDialogue.blocksRaycasts = false;
    }
    private void Explain()
    {
        loadSceneManager.LoadScene(scene);
    }
    private void CheckWin(bool value)
    {
        Time.timeScale = 0;
        OnFinishGame?.Invoke();
        canvasGroupDialogue.alpha = 1;
        canvasGroupDialogue.interactable = true;
        canvasGroupDialogue.blocksRaycasts = true;
        if (value)
        {
            win.GoDialogue();
            GameManager.instance.CompleteLevel();
            OnWin?.Invoke();
        }
        else
        {
            fail.GoDialogue();
            OnFail?.Invoke();
        }
    }
    private void MovePanel(RectTransform rect, Vector2 position, Ease ease, float time, Action OnFinish = null)
    {
        if (isInteract) return;
        isInteract = true;

        tween = rect.DOMove(position, time);
        tween.SetEase(ease);
        tween.SetUpdate(true);
        tween.OnComplete(() =>
        {
            DefaulInteract();   
            OnFinish?.Invoke(); 
        });

        if (Time.timeScale != 0)
        {
            timeGame = Time.timeScale;
            Time.timeScale = 0;
        }
    }
    private void DefaulTime()
    {
        if (timeGame != 0)
        {
            Time.timeScale = timeGame;
        }
    }
    private void DefaulInteract()
    {
        isInteract = false;
    }
    private void CheckButtonReboot()
    {
        for(int i = 0; i < buttonsReboot.Length; ++i)
        {
            buttonsReboot[i].interactable = GameManager.instance.CountSweet > 0;
        }
    }
    #region ButtonsListener
    private void OnClickPause() => MovePanel(panelPause, Vector2.zero, easePause, timePause);
    private void OnClickDoubt() => MovePanel(panelDoubt, Vector2.zero, easeDoubt, timeDoubt);
    private void OnClickSettings() => MovePanel(panelSettings, Vector2.zero, easeSettings, timeSettings);
    private void OnClickBackPause() => MovePanel(panelPause, initPositionPause, easePause, timePause, DefaulTime);
    private void OnClickBackDoubt() 
    {
        MovePanel(panelDoubt, initPositionDoubt, easeDoubt, timeDoubt,DefaulTime);
        if (!isGameStart)
        {
            isGameStart = true;
            GameManager.instance.StartGame();
            OnStartGame?.Invoke();
        }
    } 

    private void OnClickBackSettings() => MovePanel(panelSettings, initPositionSettings, easeSettings, timeSettings, DefaulTime);
    #endregion
}
