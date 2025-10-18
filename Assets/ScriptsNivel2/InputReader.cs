using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public static event Action OnClickLeft;
    public static event Action OnClickRight;


    public void ClickLeft(InputAction.CallbackContext context)
    {
        if(InputActionPhase.Performed == context.phase)
        {

            OnClickLeft?.Invoke();
        }
       
    }


    public void ClickRight(InputAction.CallbackContext context)
    {
        if(InputActionPhase.Performed == context.phase)
        {

            OnClickRight?.Invoke();
        }
        
    }
}
