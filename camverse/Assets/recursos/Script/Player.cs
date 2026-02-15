using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float multiplicadorBoost = 5f;
    public float alturaSalto = 1.5f;

    [HideInInspector]
    public Transform camaraActiva;

    private Rigidbody rb;
    private Vector3 movimiento;

    private int saltosMaximos = 2;
    private int saltosRestantes;

    private bool puedeCambiarEscena = false;
    private bool boostActivo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // 🔥 Importante para evitar atravesar objetos
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        saltosRestantes = saltosMaximos;
    }

    void Update()
    {
        float moverX = 0f;
        float moverZ = 0f;

        if (Input.GetKey(KeyCode.D)) moverX = 1f;
        if (Input.GetKey(KeyCode.A)) moverX = -1f;
        if (Input.GetKey(KeyCode.W)) moverZ = 1f;
        if (Input.GetKey(KeyCode.S)) moverZ = -1f;

        if (camaraActiva != null)
        {
            Vector3 forward = camaraActiva.forward;
            Vector3 right = camaraActiva.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            movimiento = (forward * moverZ + right * moverX).normalized;
        }

        // Salto doble
        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            Saltar();
            saltosRestantes--;
        }

        // Boost mientras se mantiene F
        boostActivo = Input.GetKey(KeyCode.F);

        // Cambio de escena
        if (Input.GetKeyDown(KeyCode.R) && puedeCambiarEscena)
        {
            SceneManager.LoadScene("final");
        }
    }

    void FixedUpdate()
    {
        float velocidadActual = boostActivo ? velocidad * multiplicadorBoost : velocidad;

        Vector3 direccion = movimiento * velocidadActual;

        // 🔥 Movimiento físico estable (no atraviesa paredes)
        rb.linearVelocity = new Vector3(direccion.x, rb.linearVelocity.y, direccion.z);
    }

    void Saltar()
    {
        float fuerzaSalto = Mathf.Sqrt(alturaSalto * -2f * Physics.gravity.y);

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detectar si tocó el suelo para resetear saltos
        foreach (ContactPoint contacto in collision.contacts)
        {
            if (contacto.normal.y > 0.5f)
            {
                saltosRestantes = saltosMaximos;
                break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZonaCambio"))
        {
            puedeCambiarEscena = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ZonaCambio"))
        {
            puedeCambiarEscena = false;
        }
    }
}
