using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerController : MonoBehaviour
{
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;



    private void OnEnable()
    {
        SliderUI.OnBarFull += Lose;
        SliderUI.OnBarEmpty += Win;
    }

    private void OnDisable()
    {
        SliderUI.OnBarFull -= Lose;
        SliderUI.OnBarEmpty -= Win;
    }

    void Win()
    {
        Time.timeScale = 0.0f;
 
    }

    void Lose()
    {
        Time.timeScale = 0.0f;

    }
}

