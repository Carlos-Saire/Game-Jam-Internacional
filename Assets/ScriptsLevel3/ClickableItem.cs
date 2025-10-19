using UnityEngine;
namespace Game3
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class ClickableItem : MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;
        protected Rigidbody2D rb;
        protected float moveSpeed;
        protected Vector2 moveDirection;

        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }

        protected virtual void Start()
        {
            moveDirection = Random.insideUnitCircle.normalized;
            rb.linearVelocity = moveDirection * moveSpeed;
        }

        private void FixedUpdate()
        {
            if (!GameManagerLevel3.Instance.gameStarted || GameManagerLevel3.Instance.gameEnded)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            rb.linearVelocity = moveDirection * moveSpeed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Vertical"))
            {
                moveDirection.x *= -1;
            }

            if (collision.gameObject.CompareTag("Horizontal"))
            {
                moveDirection.y *= -1;
            }

            moveDirection.Normalize(); 
            rb.linearVelocity = moveDirection * moveSpeed;
        }

        public abstract void OnClick();
    }
}

