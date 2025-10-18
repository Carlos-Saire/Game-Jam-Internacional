using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [SerializeField] private Slider barraCaramelos;


    public static event Action OnBarFull;
    public static event Action OnBarEmpty;



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
        barraCaramelos.value++;
        if (barraCaramelos.value == barraCaramelos.maxValue)
        {
            OnBarFull?.Invoke();
        }
    }

    void DeleteTrushAtSlider()
    {
        barraCaramelos.value--;
        if (barraCaramelos.value == barraCaramelos.minValue)
        {
            OnBarEmpty?.Invoke();
        }
    }

    void SetValueBar(int value)
    {
        barraCaramelos.value = value;
    }
}
