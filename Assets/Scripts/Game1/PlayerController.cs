using UnityEngine;
namespace Game1
{
    [RequireComponent (typeof (Rigidbody2D))]
    public class PlayerController : StartableEntity
    {
        private Rigidbody2D rb;
        private float horizontal;
        [SerializeField] private float speed;
        private void Reset()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            InputHandler.OnMoveHorizontal += Horizontal;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            InputHandler.OnMoveHorizontal -= Horizontal;
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Horizontal(float horizontal)
        {
            this.horizontal = horizontal;
        }

        private void FixedUpdate()
        {
            if (!isStartGame) return;

            rb.linearVelocity=new Vector2 (horizontal*speed,rb.linearVelocity.y) ;
        }
    }
}
