
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
        GameManager.instance.Win();
    }

    void Lose()
    {
        GameManager.instance.Fail();
    }
}

