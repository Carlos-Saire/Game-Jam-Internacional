using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : StartableEntity,IAuditable
{
    Rigidbody2D _compRigidbody2D;
    [SerializeField] float TimeFreeze;
    [SerializeField] float MaxTimeFreeze;
    [SerializeField] Vector2 FirstDestination;
    [SerializeField] Sprite ScarySprite;
    [SerializeField] Sprite NormalSprite;
    [SerializeField] AudioClipSO scarySound;
    [SerializeField] AudioClipSO startMovementSound;
    Vector2 PositionToMove;
    [SerializeField] Vector3 startPostition;
    [SerializeField] float speedMove;
    public static event Action OnTimeisOver;
    public static event Action OnCreateTrush;

    [SerializeField] GameObject trashPrefab;
    [SerializeField] private bool stateFlipInitial;

    private SpriteRenderer sprite;
    private bool isReturning = false;
    private bool hasReachedInitialPosition = false;
    private bool hasPlayedStartSound = false;

    private void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        transform.position = startPostition;
        PositionToMove = FirstDestination;
        hasReachedInitialPosition = false;
        isReturning = false;
        ActivateEnemys();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        InputReader.OnClickLeft += ScaryGhost;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        InputReader.OnClickLeft -= ScaryGhost;
    }

    private void Update()
    {
        if (!isStartGame) return;

        if (TimeFreeze > 0)
        {
            isReturning = true;
            MoveTo(startPostition);

            if (transform.position.x < startPostition.x)
                sprite.flipX = true;
            else if (transform.position.x > startPostition.x)
                sprite.flipX = false;

            if (Vector2.Distance(transform.position, startPostition) < 0.01f)
            {
                TimeFreeze -= Time.deltaTime;
                sprite.flipX = stateFlipInitial;
                sprite.sprite = NormalSprite;
                hasReachedInitialPosition = true;
                hasPlayedStartSound = false; // Se reinicia para poder reproducir el sonido luego
            }
        }
        else if (TimeFreeze <= 0 && hasReachedInitialPosition)
        {
            isReturning = false;
            GetComponent<BoxCollider2D>().enabled = true;

            if (!hasPlayedStartSound)
            {
                PlayMusic(startMovementSound);
                hasPlayedStartSound = true;
            }
        }


        if (!isReturning)
        {
            MoveTo(PositionToMove);
        }
    }

    void MoveTo(Vector2 destination)
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speedMove * Time.deltaTime);
    }

    void ActivateEnemys()
    {
        if (TimeFreeze <= 0 && hasReachedInitialPosition)
        {
            OnTimeisOver?.Invoke();
        }
    }

    public void SetNewPosition(Vector2 newPosition)
    {
        if (transform.position.x < newPosition.x)
            sprite.flipX = true;
        else if (transform.position.x > newPosition.x)
            sprite.flipX = false;

        PositionToMove = newPosition;
    }

    void ScaryGhost()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Ghost"))
        {
            EnemyController ghost = hit.collider.GetComponent<EnemyController>();
            if (ghost != null)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                ghost.TimeFreeze = ghost.MaxTimeFreeze;
                ghost.sprite.sprite = ghost.ScarySprite;
                ghost.hasReachedInitialPosition = false;
                ghost.PlayMusic(scarySound);
            }
        }
    }

    public void PlayMusic(AudioClipSO audio) { 
    
    audio.PlayOneShoot();    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Node") )
        {


            int numberMagic = UnityEngine.Random.Range(0, 101);
            if (numberMagic <= 17)
            {
                OnCreateTrush?.Invoke();
                Instantiate(trashPrefab, transform.position, Quaternion.identity);
            }


            NodeControll currentNode = collision.GetComponent<NodeControll>();
            NodeControll nextNode = currentNode.GetAdjacentNode();

            if (nextNode != null)
            {
                SetNewPosition(nextNode.transform.position);
            }
        }
    }
}
