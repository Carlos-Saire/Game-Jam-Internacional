using Unity.Mathematics;
using UnityEngine;
namespace Game1
{
    [RequireComponent (typeof (Rigidbody2D))]
    public class PlayerController : StartableEntity
    {
        [Header("Characteristics")]
        private Rigidbody2D rb;
        private float horizontal;
        [SerializeField] private float speed;
        private ParticleSystem particleSyste;

        [Header("Limits")]
        [SerializeField] private Vector2 xLimit;

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
            rb = GetComponent<Rigidbody2D>();
            particleSyste = gameObject.GetComponent<ParticleSystem>();
        }
        private void FixedUpdate()
        {
            if (!isStartGame) return;

            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
        private void Update()
        {
            if (!isStartGame) return;

            transform.position=new Vector2(Mathf.Clamp(transform.position.x, xLimit.x, xLimit.y),transform.position.y);
        }
        private void Horizontal(float horizontal)
        {
            this.horizontal = horizontal;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Good"))
            {
                particleSyste.Play();
            }
        }
        private void UpdateSpeed(float speed)
        {
            this.speed += speed;
        }

    }
}
