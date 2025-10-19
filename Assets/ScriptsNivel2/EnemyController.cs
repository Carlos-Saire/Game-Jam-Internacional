using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class EnemyController : MonoBehaviour
{
    Rigidbody2D _compRigidbody2D;
    [SerializeField] float TimeFreeze;
    [SerializeField] float MaxTimeFreeze;
    [SerializeField] TMP_Text textTimeFreeze;
    Vector2 PositionToMove;
    [SerializeField] Vector3 startPostition;
    [SerializeField] float speedMove;
    public static event Action OnTimeisOver;
    public static event Action OnCreateTrush;
    bool isTimeZero;
    [SerializeField] GameObject trashPrefab;
    int numberMagic;
    private void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transform.position = startPostition;
        SetTextTimeFreeze(TimeFreeze);
        ActivateEnemys();
    }

    private void OnEnable()
    {
        InputReader.OnClickLeft += ScaryGhost;
    }

    private void OnDisable()
    {
        InputReader.OnClickLeft += ScaryGhost;
    }
    private void Update()
    {
        if (TimeFreeze > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPostition, speedMove * Time.deltaTime);
            if (transform.position == startPostition)
            {
                transform.GetComponent<BoxCollider2D>().enabled = false;
                TimeFreeze -= Time.deltaTime;
                SetTextTimeFreeze(Mathf.FloorToInt(TimeFreeze));
            }

        }
        if (TimeFreeze < 0)
        {
            transform.GetComponent<BoxCollider2D>().enabled = true;
            isTimeZero = true;
            SetTextTimeFreeze(0);
            transform.position = Vector2.MoveTowards(transform.position, PositionToMove, speedMove * Time.deltaTime);
        }



    }

    void ActivateEnemys()
    {
        if (isTimeZero)
        {
            OnTimeisOver?.Invoke();
        }
    }

    void SetTextTimeFreeze(float time)
    {
        textTimeFreeze.text = time.ToString();
    }

    public void SetNewPosition(Vector2 newPosition)
    {
        PositionToMove = newPosition;
    }



    void ScaryGhost()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Ghost"))
        {
           
                TimeFreeze = MaxTimeFreeze;   
        }
    }

   

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Node")
        {

            numberMagic = UnityEngine.Random.Range(0, 101);
            if (numberMagic <= 17)
            {
                OnCreateTrush?.Invoke();
                Instantiate(trashPrefab, transform.position, Quaternion.identity);
            }
            SetNewPosition(collision.GetComponent<NodeControll>().GetAdjacentNode().transform.position);

        }
    }

   

}