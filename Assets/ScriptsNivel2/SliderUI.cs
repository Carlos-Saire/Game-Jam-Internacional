using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{

    [SerializeField] private int currentCandies;
    [SerializeField] private int maxCandies;

    public static event Action OnBarFull;
    public static event Action OnBarEmpty;

    [DllImport("__Internal")]
    private static extern void SetCandys(string text);

    private void OnEnable()
    {
        EnemyController.OnCreateTrush += AddTrushAtSlider;
        CalabazaController.OnGetCandys += DeleteTrushAtSlider;
        TrushGenerator.OnCreatedCandiesInitials += SetValueBar;
    }

    private void OnDisable()
    {
        EnemyController.OnCreateTrush -= AddTrushAtSlider;
        CalabazaController.OnGetCandys -= DeleteTrushAtSlider;
        TrushGenerator.OnCreatedCandiesInitials -= SetValueBar;
    }
  

    void AddTrushAtSlider()
    {
         currentCandies++;
        string gaaa = "Dulces: " + currentCandies + "/"+ maxCandies;
        #if UNITY_WEBGL && !UNITY_EDITOR
                    
                                    SetCandys(gaaa);
        #endif
        if (currentCandies >= maxCandies)
        {
            OnBarFull?.Invoke();
        }
    }

    void DeleteTrushAtSlider()
    {   currentCandies--;
        string gaaa = "Dulces: " + currentCandies + "/" + maxCandies;
        #if UNITY_WEBGL && !UNITY_EDITOR
                    
                                            SetCandys(gaaa);
        #endif
        if (currentCandies <=0)
        {
            OnBarEmpty?.Invoke();
        }
    }

    void SetValueBar(int value)
    {
        currentCandies= value;
        string gaaa = "Dulces: " + currentCandies + "/" + maxCandies;
        #if UNITY_WEBGL && !UNITY_EDITOR
                      SetCandys(gaaa);
        #endif
    }
}
