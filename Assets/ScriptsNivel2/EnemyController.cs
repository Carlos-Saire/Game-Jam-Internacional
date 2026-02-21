using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : StartableEntity, IAuditable
{
    Rigidbody2D _compRigidbody2D;

    [SerializeField] float TimeFreeze;
    [SerializeField] float MaxTimeFreeze;
    [SerializeField] Vector2 FirstDestination;
    [SerializeField] Sprite ScarySprite;
    [SerializeField] Sprite NormalSprite;
    [SerializeField] AudioClipSO scarySound;
    [SerializeField] AudioClipSO startMovementSound;

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

    private NodeControll targetNode = null;   // nodo objetivo real
    private NodeControll currentNode = null;  // nodo actual

    private Vector2 PositionToMove; // destino en vector2

    private Vector2 mousePosition;


    private void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        transform.position = startPostition;
        PositionToMove = FirstDestination; // Destino inicial
        hasReachedInitialPosition = false;
        isReturning = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        InputReader.OnClickLeft += ScaryGhost;
        InputReader.OnPostion += HandlePosition;

    }

   
    protected override void OnDisable()
    {
        base.OnDisable();
        InputReader.OnClickLeft -= ScaryGhost;
        InputReader.OnPostion -= HandlePosition;
    }
    private void HandlePosition(Vector2 vector)
    {
        mousePosition = vector;
    }

    private void Update()
    {
        if (!isStartGame) return;

        // GHOST VOLVIENDO
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
                hasPlayedStartSound = false;
            }

            return;
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

        // Movimiento normal
        if (!isReturning)
        {
            MoveTo(PositionToMove);

            // Verificación de proximidad para evitar quedarse pegado
            if (targetNode != null)
            {
                float dist = Vector2.Distance(transform.position, targetNode.transform.position);
                if (dist < 0.05f)  // margen pequeño
                {
                    HandleCorrectNode(targetNode);
                }
            }
        }
    }

    void MoveTo(Vector2 destination)
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speedMove * Time.deltaTime);
    }

    public void SetNewNode(NodeControll newNode)
    {
        if (newNode == null) return;

        targetNode = newNode;
        PositionToMove = newNode.transform.position;

        if (transform.position.x < PositionToMove.x)
            sprite.flipX = true;
        else if (transform.position.x > PositionToMove.x)
            sprite.flipX = false;
    }

    void ScaryGhost()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
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

    public void PlayMusic(AudioClipSO audio)
    {
        audio.PlayOneShoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Node"))
            return;

        currentNode = collision.GetComponent<NodeControll>();

        // Primer contacto se convierte en su ruta
        if (targetNode == null)
        {
            HandleCorrectNode(currentNode);
            return;
        }

        // Si no es el nodo objetivo real, ignorar
        if (currentNode != targetNode)
            return;

        HandleCorrectNode(currentNode);
    }

    void HandleCorrectNode(NodeControll currentNode)
    {
        // BASURA RANDOM
        int numberMagic = UnityEngine.Random.Range(0, 101);
        if (numberMagic <= 17)
        {
            OnCreateTrush?.Invoke();
            Instantiate(trashPrefab, transform.position, Quaternion.identity);
        }

        // SIGUIENTE NODO
        NodeControll nextNode = currentNode.GetAdjacentNode();
        if (nextNode != null)
            SetNewNode(nextNode);
    }
}
