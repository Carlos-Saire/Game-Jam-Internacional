using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("")]
    public int CountSweet { get; private set; }
    public int CurrentMaxLevel { get; private set; }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

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
    



}
