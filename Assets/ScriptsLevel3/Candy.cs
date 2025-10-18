using UnityEngine;
using UnityEngine.VFX;
namespace Game3
{
    public class Candy : ClickableItem
    {
        protected override void Start()
        {
            base.Start();
            moveSpeed = 3.5f; 
        }

        public override void OnClick()
        {
            Debug.Log("Click caramelo");
            GameManagerLevel3.Instance.AddScore(1);
            // SE PUEDE PONER EL SONIDO MALO ACA O SUS EFECTOS VISUALES si es q hay xd
            Destroy(gameObject);
        }
    }
}
