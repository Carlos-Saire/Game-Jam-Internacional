using UnityEngine;
using TMPro;
using System.Collections;
using System;

[CreateAssetMenu(fileName = ("DialogueSO"), menuName = ("Scriptable Objects/Dialogues/Dialogue"))]
public class DialogueSO : ScriptableObject
{
    [Header("Dialogue")]
    [SerializeField] private string[] dialogues;
    [SerializeField] private float typingSpeed;
    [SerializeField] private float waitAfterLine;

    private void Reset()
    {
        typingSpeed = 0.08f;
        waitAfterLine = 1.2f;
    }


    private IEnumerator DialoguesCorritune(TMP_Text text, Action onFinish)
    {
        for (int i = 0; i < dialogues.Length; ++i)
        {
            text.text = "";

            for (int j = 0; j < dialogues[i].Length; ++j)
            {
                text.text += dialogues[i][j];
                yield return new WaitForSecondsRealtime(typingSpeed);
            }

            yield return new WaitForSecondsRealtime(waitAfterLine);
        }

        onFinish?.Invoke();
    }
}