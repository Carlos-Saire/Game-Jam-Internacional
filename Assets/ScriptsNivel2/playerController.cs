using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class playerController : MonoBehaviour
{
    Rigidbody2D _compRigidbody2D;
    [SerializeField] float speed;
    float vertical;
    float horizontal;
    [SerializeField] float xMin, xMax, yMin, yMax;
    [SerializeField] float currentX, currentY;
    public static event Action OnYellEnemy1;
    public static event Action OnYellEnemy2;
    public static event Action OnDestroyTrush;
    SimpleLinkList<GameObject> ListTrush = new SimpleLinkList<GameObject>();
    [SerializeField] int MaxnumberGetTrush;
    [SerializeField] float timeDurationDialogue;
    int currentNumberGetTrush;
    GameObject[] lastTrushGet;
    [SerializeField] GameObject[] lastTrushGetUI;




    bool isYell;
    bool isTakeTrush;
    bool pressX = false;
    bool takeCan = false;



    private void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        lastTrushGet = new GameObject[MaxnumberGetTrush];

    }


    private void Update()
    {
        currentX = Mathf.Clamp(transform.position.x, xMin, xMax);
        currentY = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector2(currentX, currentY);

        if (takeCan && pressX)
        {
            if (currentNumberGetTrush > 0)
            {
                ListTrush.DeleteAtStart();
                Destroy(lastTrushGet[currentNumberGetTrush - 1]);
                lastTrushGetUI[currentNumberGetTrush - 1].gameObject.SetActive(false);
                currentNumberGetTrush--;
                OnDestroyTrush?.Invoke();
            }
            pressX = false;
        }

    }

    public void AxisY(InputAction.CallbackContext context)
    {
        vertical = context.ReadValue<float>();
    }

    public void AxisX(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<float>();
    }

    public void ButtonZ(InputAction.CallbackContext context)
    {
        isTakeTrush = context.performed;
    }

    public void ButtonX(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            pressX = true;
        }
        else
        {
            pressX = false;
        }

    }

    public void ButtonC(InputAction.CallbackContext context)
    {
        isYell = context.performed;
    }


    private void FixedUpdate()
    {
        _compRigidbody2D.linearVelocity = new Vector2(speed * horizontal, speed * vertical);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Enemy1" && isYell == true)
        {
            OnYellEnemy1?.Invoke();
            StartCoroutine(Dialogue());

            isYell = false;
        }
        if (collision != null && collision.gameObject.tag == "Enemy2" && isYell == true)
        {
            OnYellEnemy2?.Invoke();
            StartCoroutine(Dialogue());
            isYell = false;
        }

        if (collision != null && collision.gameObject.tag == "trush" && isTakeTrush == true)
        {

            isTakeTrush = false;
            if (currentNumberGetTrush < MaxnumberGetTrush)
            {
                lastTrushGet[currentNumberGetTrush] = collision.gameObject;
                lastTrushGetUI[currentNumberGetTrush].gameObject.SetActive(true);
                currentNumberGetTrush++;
                ListTrush.AddAtStart(collision.gameObject);

                collision.gameObject.SetActive(false);
            }

        }
        IEnumerator Dialogue()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(timeDurationDialogue);
            transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "trashCan")
        {
            takeCan = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "trashCan")
        {
            takeCan = false;
        }
    }
}