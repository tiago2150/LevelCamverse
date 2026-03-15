using UnityEngine;

public class camara : MonoBehaviour
{
    public Transform objetivo;

    [Header("Modo lateral")]
    public float distancia = 5f;
    public LayerMask capasColision;

    [Header("Modo cenital")]
    public float alturaCenital = 15f;

    public float suavizado = 10f;

    private bool modoCenital = false;

    public void ActivarCenital(bool estado)
    {
        modoCenital = estado;
    }

    void LateUpdate()
    {
        if (objetivo == null) return;

        if (modoCenital)
        {
            Vector3 posicionDeseada = objetivo.position + new Vector3(0, alturaCenital, 0);

            transform.position = Vector3.Lerp(
                transform.position,
                posicionDeseada,
                suavizado * Time.deltaTime
            );

            transform.rotation = Quaternion.Euler(90f, 0f, 0f);

            return;
        }

        Vector3 direccion = objetivo.right;

        Vector3 posicionDeseada2 = objetivo.position + direccion * distancia;

        RaycastHit hit;

        if (Physics.Raycast(objetivo.position, direccion, out hit, distancia, capasColision))
        {
            posicionDeseada2 = hit.point - direccion * 0.2f;
        }

        transform.position = Vector3.Lerp(
            transform.position,
            posicionDeseada2,
            suavizado * Time.deltaTime
        );

        transform.LookAt(objetivo);
    }
}