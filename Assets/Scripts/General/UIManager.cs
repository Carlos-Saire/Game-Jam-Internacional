using UnityEngine;
using DG.Tweening;
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
    private void Start()
    {
        if (introduccion == null)
        {
            history.GoDialogue(Explain);
        }
        else
        {
            introduccion.GoDialogue(Fade);
        }
    }
    private void Fade()
    {
        Tweener fadeTween = canvasGroupDialogue.DOFade(0, 1);
        fadeTween.OnComplete(() => GameManager.instance.StartGame());
    }
    private void Explain()
    {
        loadSceneManager.LoadScene(scene);
    }
}
