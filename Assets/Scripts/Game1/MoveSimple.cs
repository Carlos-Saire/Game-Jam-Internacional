using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [Header("Configuración de movimiento")]
    [SerializeField] private float speed = 2f;          // velocidad base
    [SerializeField] private float changeDirTime = 2f;  // tiempo entre cambios de dirección
    [SerializeField] private float rotationSpeed = 5f;  // velocidad de rotación suave

    [Header("Rango del vuelo")]
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;

    private Vector2 direction;
    private float timer;

    void Start()
    {
        CambiarDireccion();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeDirTime)
        {
            CambiarDireccion();
            timer = 0;
        }

        // Movimiento dentro de los límites
        Vector3 pos = transform.position + (Vector3)(direction * speed * Time.deltaTime);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

        // Rotación suave hacia la dirección actual
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void CambiarDireccion()
    {
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
