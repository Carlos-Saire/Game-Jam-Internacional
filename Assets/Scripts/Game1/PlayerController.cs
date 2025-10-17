using UnityEngine;
namespace Game1
{
    [RequireComponent (typeof (Rigidbody2D))]
    public class PlayerController : StartableEntity
    {
        private Rigidbody2D rb;
        private float horizontal;
        [SerializeField] private float speed;
        private ParticleSystem particleSyste;

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
        private void Horizontal(float horizontal)
        {
            this.horizontal = horizontal;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            particleSyste.Play();
        }
        private void UpdateSpeed(float speed)
        {
            this.speed += speed;
        }

    }
}
