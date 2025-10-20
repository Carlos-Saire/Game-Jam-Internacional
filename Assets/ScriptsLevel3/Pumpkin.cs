using Game3;
using UnityEngine;
namespace Game3
{
    public class Pumpkin : ClickableItem
    {
        public AudioSource audioSource;
        public GameObject effectprefab;
        private float lifeTime = 7f;

        protected override void Start()
        {
            audioSource = GetComponent<AudioSource>();

            base.Start();
            moveSpeed = 8f;

            if (audioSource != null)
            {
                audioSource.pitch = Random.Range(0.7f, 1.4f);
                audioSource.Play();
            }

            Destroy(gameObject, lifeTime); 
        }

        public override void OnClick()
        {
            GameManagerLevel3.Instance.ReduceLife(1);

            GameObject trick = Instantiate(effectprefab, transform.position, Quaternion.identity);
            Destroy(trick,0.5f);

            Destroy(gameObject);
        }
    }
}
