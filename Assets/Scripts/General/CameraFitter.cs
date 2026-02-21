using UnityEngine;

[ExecuteAlways]
public class CameraFitter : MonoBehaviour
{
    private Camera cam;

    public float baseOrthographicSize = 5f;   
    private float targetAspect = 16f / 9f;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
    }
    void Start()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        float scaleFactor = targetAspect / currentAspect;
        cam.orthographicSize = baseOrthographicSize * scaleFactor;
    }
}