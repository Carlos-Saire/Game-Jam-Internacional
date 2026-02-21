using UnityEngine;
using UnityEngine.InputSystem;

namespace Game3
{
    public class PlayerClick : MonoBehaviour
    {
        void Update()
        {
            if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
            {
                DetectClick();
            }
        }

        private void DetectClick()
        {
            Vector2 pointerPos = Pointer.current.position.ReadValue();

            Ray ray = Camera.main.ScreenPointToRay(pointerPos);
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