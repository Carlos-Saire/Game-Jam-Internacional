using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerController : MonoBehaviour
{
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;



    private void OnEnable()
    {
        TrushGenerator.OnTopContamination += Lose;
        TrushGenerator.OnBottomContamination += Win;
    }

    private void OnDisable()
    {
        TrushGenerator.OnTopContamination -= Lose;
        TrushGenerator.OnBottomContamination -= Win;
    }

    void Win()
    {
        Time.timeScale = 0.0f;
        panelWin.SetActive(true);
    }

    void Lose()
    {
        Time.timeScale = 0.0f;
        panelLose.SetActive(true);
    }
}

