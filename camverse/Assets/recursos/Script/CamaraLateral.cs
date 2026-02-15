using UnityEngine;

public class CamaraLateral : MonoBehaviour
{
    public Transform objetivo;
    public Vector3 offset = new Vector3(4f, 2f, 0f); // Lado derecho fijo
    public float suavizado = 5f;

    private Quaternion rotacionFija;

    void Start()
    {
        // Guardamos la rotación inicial de la cámara
        rotacionFija = transform.rotation;
    }

    void LateUpdate()
    {
        if (objetivo == null) return;

        // Solo sigue la posición (NO usa objetivo.right)
        Vector3 posicionDeseada = objetivo.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            posicionDeseada,
            suavizado * Time.deltaTime
        );

        // Mantiene rotación fija
        transform.rotation = rotacionFija;
    }
}

