using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SettingSO audioSettings;

    [Header("Slider Music")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;

    [Header("Buttons")]
    [SerializeField] private Button buttonPause;
    [SerializeField] private Button buttonDoubt;
    [SerializeField] private Button buttonSettings;



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
    [SerializeField] private Vector2 initPositionSettings;
    [SerializeField] private Vector2 initPositionPause;
    [SerializeField] private Vector2 initPositionDoubt;

    private bool isInteract;
    private Tween tween;
    private void OnEnable()
    {
        buttonPause.onClick.AddListener(OnClickPause);
        buttonDoubt.onClick.AddListener(OnClickDoubt);
        buttonSettings.onClick.AddListener(OnClickSettings);
    }
    private void OnDisable()
    {
        buttonPause.onClick.RemoveListener(OnClickPause);
        buttonDoubt.onClick.RemoveListener(OnClickDoubt);
        buttonSettings.onClick.RemoveListener(OnClickSettings);
    }

    private void Awake()
    {
        audioSettings.LoadVolumes();
    }
    private void Start()
    {
        sliderMaster.value = audioSettings.GetMasterVolume();
        sliderMaster.onValueChanged.AddListener(UpdateMasterVolume);

        sliderMusic.value = audioSettings.GetMusicVolume();
        sliderMusic.onValueChanged.AddListener(UpdateMusicVolume);

        sliderSFX.value = audioSettings.GetSFXVolume();
        sliderSFX.onValueChanged.AddListener(UpdateSFXVolume);

        UpdateMasterVolume(sliderMaster.value);
        UpdateMusicVolume(sliderMusic.value);
        UpdateSFXVolume(sliderSFX.value);

        initPositionSettings = panelSettings.position;
        initPositionPause = panelPause.position;
        initPositionDoubt = panelDoubt.position;

    }
    private void OnDestroy()
    {
        tween.Kill();
    }
    private void UpdateMasterVolume(float value)
    {
        audioSettings.SetMasterVolume(value);
    }

    private void UpdateMusicVolume(float value)
    {
        audioSettings.SetMusicVolume(value);
    }

    private void UpdateSFXVolume(float value)
    {
        audioSettings.SetSFXVolume(value);
    }
    private void OnClickPause()
    {
        MovePanel(panelPause,Vector2.zero, easePause, timePause);
    }
    private void OnClickDoubt()
    {
        MovePanel(panelDoubt,Vector2.zero, easeDoubt,timeDoubt);
    }
    private void OnClickSettings()
    {
        MovePanel(panelSettings,Vector2.zero, easeSettings, timeSettings);
    }
    private void MovePanel(RectTransform rect,Vector2 position,Ease ease,float time)
    {
        if (isInteract) return;
        isInteract = true;

        tween = rect.DOMove(position, time);
        tween.SetEase(ease);
        tween.OnComplete(DefaulInteract);
    }
    private void DefaulInteract()
    {
        isInteract = false;
    }
}
