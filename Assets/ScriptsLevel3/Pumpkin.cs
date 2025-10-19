using Game3;
using UnityEngine;
namespace Game3
{
    public class Pumpkin : ClickableItem
    {
        private float lifeTime = 5f;

        protected override void Start()
        {
            base.Start();
            moveSpeed = 1.5f; 
            Destroy(gameObject, lifeTime); 
        }

        public override void OnClick()
        {
            Debug.Log("Click calabaza");
            GameManagerLevel3.Instance.ReduceLife(1);
            // SE PUEDE PONER EL SONIDO MALO ACA O SUS EFECTOS VISUALES si es q hay xd
            Destroy(gameObject);
        }
    }
}
