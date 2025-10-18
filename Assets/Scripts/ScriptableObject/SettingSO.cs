using System;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/AudioSettings", order = 1)]
public class SettingSO : ScriptableObject
{
    [SerializeField] private AudioMixer mainMixer;

    [Header("VolumeKey")]
    [SerializeField] private string masterKey;
    [SerializeField] private string musicKey;
    [SerializeField] private string sfxKey;

    [Header("Volume")]
    [Range(0f, 1f)][SerializeField] private float masterVolume;
    [Range(0f, 1f)][SerializeField] private float musicVolume;
    [Range(0f, 1f)][SerializeField] private float sfxVolume;

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp(volume, 0.0001f, 1f);
        mainMixer.SetFloat(masterKey, Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp(volume, 0.0001f, 1f);
        mainMixer.SetFloat(musicKey, Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp(volume, 0.0001f, 1f);
        mainMixer.SetFloat(sfxKey, Mathf.Log10(volume) * 20);
    }
    public void LoadVolumes()
    {
        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }
    public float GetMasterVolume()
    {
        return masterVolume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

}