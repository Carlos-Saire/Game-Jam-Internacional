using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CalabazaController : MonoBehaviour, IAuditable
{
    public static event Action OnGetCandys;

    private bool existsCandys =false;
    [SerializeField] private AudioClipSO audioEffectHave;
    [SerializeField] private AudioClipSO audioEffectHavent;

    private Vector2 mousePosition;
    private void OnEnable()
    {
        InputReader.OnClickRight += GetCandys;
        InputReader.OnPostion += HandlePosition;
        CandyController.OnExistCandys += SetValueExistsCandies;
    }

    private void OnDisable()
    {
        InputReader.OnClickRight -= GetCandys;
        InputReader.OnPostion -= HandlePosition;
        CandyController.OnExistCandys -= SetValueExistsCandies;
    }
    private void HandlePosition(Vector2 vector)
    {
        mousePosition = vector;
    }
    void GetCandys()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Calabaza"))
        {
            if (existsCandys)
            {
                OnGetCandys?.Invoke();
                PlayMusic(audioEffectHave);
            }
            else
            {
                PlayMusic(audioEffectHavent);
            }

        }
    }

    
    void SetValueExistsCandies(bool value)
    {
        existsCandys = value;
    }

    public void PlayMusic(AudioClipSO audio)
    {
        audio.PlayOneShoot();

    }
}
