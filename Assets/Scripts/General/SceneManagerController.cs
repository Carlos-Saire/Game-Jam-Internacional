using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerController : MonoBehaviour
{
    [SerializeField] private string[] games;

    [DllImport("__Internal")]
    private static extern void SetTime(string text);

    [DllImport("__Internal")]
    private static extern void SetCandys(string text);

    [DllImport("__Internal")]
    private static extern void SetLife(string text);
    public void LoadScene(string scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
        if(scene == "Nivel2")
        {
#if UNITY_WEBGL && !UNITY_EDITOR
                        SetLife("");
#endif
#if UNITY_WEBGL && !UNITY_EDITOR
                                    SetTime("");
#endif
        }
    }
    public void CurrentLoadScene()
    {
        LoadScene(games[GameManager.instance.CurrentMaxLevel-1]);
    }
    public void Reboot()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
