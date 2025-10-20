using System;
using Game1;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static event Action OnGameStarted;
    public static event Action<bool> OnGameWin;
    public static GameManager instance;

    [Header("")]
    public int CountSweet { get; set; }
    public int CurrentMaxLevel { get; private set; }

    [Header("Score")]
    [SerializeField] private ScoreSO score;

    [Header("Game1")]
    [SerializeField] private int scoreToWin;

    private void OnEnable()
    {
        TimerController.OnGameSpeedIncreased += SetGameSpeed;
        TimerController.OnGameFinish += FinishedGame1;
    }
    private void OnDisable()
    {
        TimerController.OnGameSpeedIncreased -= SetGameSpeed;
        TimerController.OnGameFinish -= FinishedGame1;
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
    public void GameOver()
    {
        PlayerPrefs.DeleteKey("countSweet");
        PlayerPrefs.DeleteKey("currentMaxLevel");
        CountSweet = 0;
        CurrentMaxLevel = 1;
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
        score.Score = 0;
    }
    private void FinishedGame1()
    {
        Time.timeScale = 0;
        if (scoreToWin<=score.Score)
        {
            OnGameWin?.Invoke(true);
        }
        else
        {
            OnGameWin?.Invoke(false);
        }
    }
    public void Win()
    {
        OnGameWin?.Invoke(true);
    }
    public void Fail()
    {
        OnGameWin?.Invoke(false);
    }
}
