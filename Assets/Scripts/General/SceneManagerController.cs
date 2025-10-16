using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerController : MonoBehaviour
{
    [SerializeField] private string[] games;
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void CurrentLoadScene()
    {
        LoadScene(games[GameManager.instance.CurrentMaxLevel-1]);
    }
    public void ReLoad()
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
