using UnityEngine;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    [Header("CanvasGroup")]
    [SerializeField] private CanvasGroup canvasGroupDialogue; 

    [Header("DialoguesControllers")] 
    [SerializeField] private DialogueController introduccion;
    [SerializeField] private DialogueController win;
    [SerializeField] private DialogueController fail;
    private void Start()
    {
        introduccion.GoDialogue(Fade);
    }
    private void Fade()
    {
        Tweener fadeTween = canvasGroupDialogue.DOFade(0, 1);
        fadeTween.OnComplete(() => GameManager.instance.StartGame());
    }
}
