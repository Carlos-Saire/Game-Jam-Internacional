using System.Collections;
using Unity.Mathematics;
using UnityEngine;
namespace Game1
{
    [RequireComponent (typeof (Rigidbody2D))]
    public class PlayerController : StartableEntity
    {
        private SpriteRenderer spriteRenderer;

        [Header("Characteristics")]
        private Rigidbody2D rb;
        private float horizontal;
        [SerializeField] private float speed;
        private ParticleSystem particleSyste;
        private Camera cam;

        [Header("Limits")]
        [SerializeField] private Vector2 xLimit;
        [SerializeField] private float screenOffset = 0.2f;

        [Header("Sprite")]
        [SerializeField]  private Sprite goodSprite;
        [SerializeField]  private Sprite badSprite;
        [SerializeField]  private Sprite neutralSprite;
        [SerializeField]  private float timeWaitChangueSprite;
        private void Reset()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            InputHandler.OnMoveHorizontal += Horizontal;
            RayController.OnSpeed += UpdateSpeed;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            InputHandler.OnMoveHorizontal -= Horizontal;
            RayController.OnSpeed -= UpdateSpeed;
        }
        private void Awake()
        {
            cam = Camera.main;
            rb = GetComponent<Rigidbody2D>();
            particleSyste = gameObject.GetComponent<ParticleSystem>();
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        private void FixedUpdate()
        {
            if (!isStartGame) return;

            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
        private void Update()
        {
            if (!isStartGame) return;

            float halfWidth = cam.orthographicSize * cam.aspect;

            float playerHalfSize = spriteRenderer.bounds.extents.x;

            float leftLimit = -halfWidth + playerHalfSize + screenOffset;
            float rightLimit = halfWidth - playerHalfSize - screenOffset;

            transform.position = new Vector2(
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                transform.position.y
            );
        }
        private void Horizontal(float horizontal)
        {
            this.horizontal = horizontal;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.CompareTag("Good"))
            {
                StopAllCoroutines();
                
                StartCoroutine(ChangueSprite(timeWaitChangueSprite));
                
                particleSyste.Play();
            }

            if (collision.CompareTag("Bad"))
            {
                StopAllCoroutines();
                StartCoroutine(ChangueSpriteBad(timeWaitChangueSprite));
                
            }



        }

        IEnumerator ChangueSprite(float time)
        {
            spriteRenderer.sprite = goodSprite;
            yield return new WaitForSecondsRealtime(time);
            spriteRenderer.sprite = neutralSprite;
        }

        IEnumerator ChangueSpriteBad(float time)
        {
            spriteRenderer.sprite = badSprite;
            yield return new WaitForSecondsRealtime(time);
            spriteRenderer.sprite = neutralSprite;
        }
        private void UpdateSpeed(float speed)
        {
            this.speed += speed;
        }



    }
}
