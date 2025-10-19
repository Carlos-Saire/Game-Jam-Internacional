using UnityEngine;
using UnityEngine.VFX;
namespace Game3
{
    public class Candy : ClickableItem
    {
        [Header("Candy Sprites")]
        public Sprite[] candySprites;

        protected override void Start()
        {
            base.Start();
            moveSpeed = 4f;

            if (candySprites != null && candySprites.Length > 0)
            {
                int index = Random.Range(0, candySprites.Length);
                spriteRenderer.sprite = candySprites[index];
            }
        }

        public override void OnClick()
        {
            Debug.Log("Click caramelo");
            GameManagerLevel3.Instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}
