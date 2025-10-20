using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CandyController : StartableEntity, IAuditable
{
    public static event Action<int> OnGetCandy;
    public static event Action<int> OnSetCandy;
    public static event Action<bool> OnExistCandys;
   
    public int currentCandys = 0;
    [SerializeField] private int maxCandys;
    [SerializeField] private AudioClipSO audioEffect;
    [SerializeField] private AudioClipSO inventoryFull;
    protected override  void OnEnable()
    {
        base.OnEnable();
        InputReader.OnClickLeft += DropCandy;
        CalabazaController.OnGetCandys += SetCandy;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        InputReader.OnClickLeft -= DropCandy;
        CalabazaController.OnGetCandys -= SetCandy;
    }
    void DropCandy()
    {
        if(!isStartGame) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);


        if (hit.collider != null && hit.collider.CompareTag("Candy"))
        {
            if (currentCandys < maxCandys)
            {
                PlayMusic(audioEffect);
                Destroy(hit.collider.gameObject);
                OnGetCandy?.Invoke(currentCandys);
                currentCandys++;
                OnExistCandys?.Invoke(true);

            }
            else
            {
                PlayMusic(inventoryFull);
            }

        }
                
            
        

    }

    void SetCandy()
    {

        currentCandys--;
        currentCandys = Mathf.Clamp(currentCandys,0, maxCandys);
        OnSetCandy?.Invoke(currentCandys);
        if (currentCandys == 0)
            OnExistCandys?.Invoke(false);
    }
    public void PlayMusic(AudioClipSO audio)
    {
       audio.PlayOneShoot();
       
    }
}
