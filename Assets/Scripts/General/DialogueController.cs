using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Net.NetworkInformation;
using UnityEditor;
using System.Runtime.CompilerServices;

public class DialogueController : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private float typingSpeed = 0.08f;
    [SerializeField] private float waitAfterLine = 1.2f;
    [SerializeField] private TMP_Text text;

    [Header("Audios Dialogues")]
    [SerializeField] private AudioSource[] audiosDialogues;

    [Header("Localization")]
    [SerializeField] private string tablename;

    private StringTable dialogueTable;
   [SerializeField]  private List<string> value = new List<string>();

    private Coroutine activeCoroutine;

    

    public void GoDialogue(Action OnComplete = null)
    {
        activeCoroutine = StartCoroutine(LoadAndStartDialogue(OnComplete));
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
            if (audio != null && audio.isPlaying)
                audio.Stop();
        }
    }

    private IEnumerator LoadAndStartDialogue(Action OnComplete)
    {

        var idiomas = LocalizationSettings.StringDatabase.GetTableAsync(tablename);
        yield return idiomas;
        dialogueTable = idiomas.Result as StringTable;

        foreach (var entry in dialogueTable.SharedData.Entries)
        {
            value.Add(dialogueTable.GetEntry(entry.Key).Value);
        }
        activeCoroutine = StartCoroutine(DialoguesCoroutine(OnComplete));
    }

    private IEnumerator DialoguesCoroutine(Action OnComplete)
    {
        for (int i = 0; i < value.Count; ++i)
        {
            text.text = "";

            if (i < audiosDialogues.Length && audiosDialogues[i] != null)
                audiosDialogues[i].Play();

            foreach (char c in value[i])
            {
                text.text += c;
                yield return new WaitForSecondsRealtime(typingSpeed);
            }

            yield return new WaitForSecondsRealtime(waitAfterLine); 
        }

        activeCoroutine = null;
        OnComplete?.Invoke();
    }
}
