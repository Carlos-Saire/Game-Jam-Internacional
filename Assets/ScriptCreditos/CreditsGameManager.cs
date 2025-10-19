using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeAndMove : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public RectTransform canvasObject;

    public float moveSpeed = 3f;
    public float fadeInDuration = 2f;
    public float waitTime = 2f;

    private float currentFadeTime = 0f;
    private float currentWaitTime = 0f;
    private bool isFadingIn = true;
    private bool hasWaited = false;

    void Update()
    {
        if (isFadingIn)
        {
            currentFadeTime += Time.deltaTime;
            float alpha = Mathf.Clamp(currentFadeTime / fadeInDuration, 0f, 1f);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);

            if (currentFadeTime >= fadeInDuration)
            {
                isFadingIn = false;
                currentWaitTime = 0f;
            }
        }

        if (!isFadingIn && !hasWaited)
        {
            currentWaitTime += Time.deltaTime;
            if (currentWaitTime >= waitTime)
            {
                hasWaited = true;
            }
        }

        if (hasWaited)
        {
            Vector3 moveDirection = Vector3.up * moveSpeed * Time.deltaTime;
            spriteRenderer.transform.position += moveDirection;

            canvasObject.localPosition += new Vector3(0f, moveDirection.y * 60, 0f);
        }

        if (canvasObject.localPosition.y >= 6270f)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
