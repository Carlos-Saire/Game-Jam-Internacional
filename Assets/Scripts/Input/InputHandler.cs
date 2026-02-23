using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    public static event Action<float> OnMoveHorizontal;

    private MainInput inputs;
    private InputAction moveAction;
    private InputAction touchAction;

    private float _width;

    private void Reset()
    {
        gameObject.name = "InputHandler";
        PlayerInput playerInput = GetComponent<PlayerInput>();
        playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
    }
    private void Awake()
    {
        inputs = new MainInput();
        moveAction = inputs.Player.Move;
        touchAction = inputs.Player.Touch;  
    }
    private void Start()
    {
        _width = Screen.width/2;
    }
    private void OnEnable()
    {
        inputs.Enable();

        moveAction.performed += InputMove;
        moveAction.started += InputMove;
        moveAction.canceled += InputMove;

        touchAction.performed += InputTouch;
        touchAction.started += InputTouch;
        touchAction.canceled += InputTouch;
    }

    private void OnDisable()
    {
        moveAction.performed -= InputMove;
        moveAction.started -= InputMove;
        moveAction.canceled -= InputMove;

        touchAction.performed -= InputTouch;
        touchAction.started -= InputTouch;
        touchAction.canceled -= InputTouch;

        inputs.Disable();
    }
    public void InputMove(InputAction.CallbackContext context)
    {
        OnMoveHorizontal?.Invoke(context.ReadValue<Vector2>().x);
    }
    public void InputTouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 touchPosition = Touchscreen.current.position.value;
            if (touchPosition.x < _width)
            {
                OnMoveHorizontal?.Invoke(-1f);
            }
            else
            {
                OnMoveHorizontal?.Invoke(1f);
            }
        }
        else if (context.canceled)
        {
            OnMoveHorizontal?.Invoke(0f);
        }
       
        
    }
}
