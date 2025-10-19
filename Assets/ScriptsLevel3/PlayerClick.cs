using UnityEngine;
using UnityEngine.InputSystem;
namespace Game3 
{
    public class PlayerClick : MonoBehaviour
    {
        void Update()
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                DetectClick();
            }
        }

        private void DetectClick()
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                ClickableItem clickable = hit.collider.GetComponent<ClickableItem>();
                if (clickable != null)
                {
                    clickable.OnClick();
                }
            }
        }
    }
}
