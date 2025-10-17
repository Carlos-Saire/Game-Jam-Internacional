using System;
using Game1;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static event Action OnGameStarted;
    public static GameManager instance;

    [Header("")]
    public int CountSweet { get; private set; }
    public int CurrentMaxLevel { get; private set; }
    private void OnEnable()
    {
        TimerController.OnGameSpeedIncreased += SetGameSpeed;
    }
    private void OnDisable()
    {
        TimerController.OnGameSpeedIncreased -= SetGameSpeed;
    }
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
    private void SetGameSpeed(bool isFast)
    {
        if (isFast)
        {
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void CompleteLevel()
    {
        ++CountSweet;
        ++CurrentMaxLevel;
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("countSweet", CountSweet);
        PlayerPrefs.SetInt("currentMaxLevel", CurrentMaxLevel);
        PlayerPrefs.Save();
    }
    private void LoadData()
    {
        CountSweet = PlayerPrefs.GetInt("countSweet", 0);
        CurrentMaxLevel = PlayerPrefs.GetInt("currentMaxLevel", 1);
    }
    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }

}
