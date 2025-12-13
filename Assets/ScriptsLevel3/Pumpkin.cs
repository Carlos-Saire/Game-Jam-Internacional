using Game3;
using UnityEngine;
namespace Game3
{
    public class Pumpkin : ClickableItem,IAuditable
    {
        public GameObject effectprefab;
        public AudioClipSO calabazaSound;
        private float lifeTime = 7f;

        protected override void Start()
        {
            

            base.Start();
            moveSpeed = 8f;

            

            Destroy(gameObject, lifeTime); 
        }

        public override void OnClick()
        {
            GameManagerLevel3.Instance.ReduceLife(1);
            GameObject trick = Instantiate(effectprefab, transform.position, Quaternion.identity);
            PlayMusic(calabazaSound);
            Destroy(trick,0.5f);

            Destroy(gameObject);
        }

        public void PlayMusic(AudioClipSO audio)
        {
            audio.PlayOneShoot();
        }
    }
}
