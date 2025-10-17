using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonContinue;
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
    }
    private void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Entro");
    }
}
