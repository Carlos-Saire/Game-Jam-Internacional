using TMPro;
using System.Collections;
using UnityEngine;
using System;

public class DialogueController : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private string[] dialogues;
    [SerializeField] private float typingSpeed;
    [SerializeField] private float waitAfterLine;
    [SerializeField] private TMP_Text text;

    [Header("AudiosDialogues")]
    [SerializeField] private AudioSource[] audiosDialogues; 
    private void Reset()
    {
        typingSpeed = 0.08f;
        waitAfterLine = 1.2f;
    }
    public void GoDialogue(Action OnComplete=null)
    {
        StartCoroutine(DialoguesCorritune(OnComplete));
    }
    private IEnumerator DialoguesCorritune(Action OnComplete)
    {
        for (int i = 0; i < dialogues.Length; ++i)
        {
            text.text = "";
            if (i < audiosDialogues.Length && audiosDialogues[i] != null)
            {
                audiosDialogues[i].Play();
            }
            for (int j = 0; j < dialogues[i].Length; ++j)
            {
                text.text += dialogues[i][j];
                yield return new WaitForSecondsRealtime(typingSpeed);
            }

            yield return new WaitForSecondsRealtime(waitAfterLine);
        }
        OnComplete?.Invoke();
    }
}
