using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CalabazaController : MonoBehaviour
{
    public static event Action OnGetCandys;

    private bool existsCandys =false;

    private void OnEnable()
    {
        InputReader.OnClickRight += GetCandys;
        CandyController.OnExistCandys += SetValueExistsCandies;
    }

    private void OnDisable()
    {
        InputReader.OnClickRight -= GetCandys;
        CandyController.OnExistCandys -= SetValueExistsCandies;
    }
    void GetCandys()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Calabaza"))
            {
               if(existsCandys)
                OnGetCandys?.Invoke();
            }
    }

    
    void SetValueExistsCandies(bool value)
    {
        existsCandys = value;
    }
}
