using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    public static event Action<float> OnMoveHorizontal;
    public void InputMove(InputAction.CallbackContext context)
    {
        OnMoveHorizontal?.Invoke(context.ReadValue<Vector2>().x);
    }
}
