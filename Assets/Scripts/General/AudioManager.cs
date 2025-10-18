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
 
}
