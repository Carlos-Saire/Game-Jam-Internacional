using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public static event Action OnClickLeft;
    public static event Action OnClickRight;
    public static event Action<Vector2> OnPostion;


    public void ClickLeft(InputAction.CallbackContext context)
    {
        if(InputActionPhase.Performed == context.phase)
        {

            OnClickLeft?.Invoke();
            Debug.Log("Click");
        }
       
    }


    public void ClickRight(InputAction.CallbackContext context)
    {
        if(InputActionPhase.Performed == context.phase)
        {
            Debug.Log("Click");
            OnClickRight?.Invoke();
        }
        
    }
    public void Position(InputAction.CallbackContext context)
    {
        OnPostion?.Invoke(context.ReadValue<Vector2>());
    }
}
