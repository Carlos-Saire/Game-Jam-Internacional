using System;
using UnityEngine;
namespace Game1
{
    [RequireComponent (typeof (Rigidbody2D))]
    [RequireComponent (typeof (BoxCollider2D))]
    public abstract class Items : MonoBehaviour
    {
        public static event Action<int> OnScore;

        private Rigidbody2D rb;
        [SerializeField] private float speed;
        [Header("Audio")]
        [SerializeField] private AudioClipSO audioClipSO;
        [Header("Score")]
        [SerializeField] private int incrementScore;
        private void Reset()
        {
            rb = GetComponent<Rigidbody2D>();
            GetComponent<Collider2D>().isTrigger = true;
            rb.gravityScale = 0;
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        protected virtual void Update()
        {
            if (transform.position.y <= -6.1f)
            {
                Destroy(gameObject);
            }
        }
        protected virtual void FixedUpdate()
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -speed);
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CollisionPlayer();
            }
        }
        protected virtual void CollisionPlayer()
        {
            audioClipSO.PlayOneShoot();
            Destroy(gameObject);
        }
        protected void ActiveEventIncrmentScore()
        {
            OnScore?.Invoke(incrementScore);
        }
    }
}

