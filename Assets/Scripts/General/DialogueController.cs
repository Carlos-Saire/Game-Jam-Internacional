using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using System;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private string[] dialogues;

    [SerializeField] private float typingSpeed;
    [SerializeField] private float waitAfterLine;
    [SerializeField] private TMP_Text text;

    [Header("AudiosDialogues")]
    [SerializeField] private AudioSource[] audiosDialogues;

    private StringTable dialogueTable;

    [Header("Localization")]
    [SerializeField] private List<string> value;
    [SerializeField] private string tablename;

    private Coroutine activeCoroutine;

    private void Reset()
    {
        typingSpeed = 0.08f;
        waitAfterLine = 1.2f;
    }

    public void GoDialogue(Action OnComplete = null)
    {
        activeCoroutine = StartCoroutine(DialoguesCorritune(OnComplete));
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

        activeCoroutine = null; 
        OnComplete?.Invoke();
    }

    public void StopDialogue()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        text.text = "";
        foreach (var audio in audiosDialogues)
        {
            if (audio != null)
                audio.Stop();
        }
    }

    public void test()
    {
        StartCoroutine(Idiomas());
    }

    private IEnumerator Idiomas()
    {
        var idiomas = LocalizationSettings.StringDatabase.GetTableAsync(tablename);
        yield return idiomas;
        dialogueTable = idiomas.Result as StringTable;
        Debug.Log(dialogueTable.SharedData.Entries);

        foreach (var entry in dialogueTable.SharedData.Entries)
        {
            value.Add(dialogueTable.GetEntry(entry.Key).Value);
        }
    }
}
