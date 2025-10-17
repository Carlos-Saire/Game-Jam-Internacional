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

    private Tweener tweener; 
    private void OnEnable()
    {
        GameManager.OnGameWin += CheckWin;
    }
    private void OnDisable()
    {
        GameManager.OnGameWin -= CheckWin;
    }
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
    private void OnDestroy()
    {
        tweener.Kill();
    }
    private void Fade()
    {
        tweener = canvasGroupDialogue.DOFade(0, 1);
        tweener.OnComplete(() => GameManager.instance.StartGame());
    }
    private void Explain()
    {
        loadSceneManager.LoadScene(scene);
    }
    private void CheckWin(bool value)
    {
        if (value)
        {
            tweener = canvasGroupDialogue.DOFade(1, 0);
            win.GoDialogue();
        }
        else
        {
            tweener = canvasGroupDialogue.DOFade(1, 0);
            fail.GoDialogue();
        }
    }
}
