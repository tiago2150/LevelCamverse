using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player1 : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float multiplicadorBoost = 5f; // 500%
    public float alturaSalto = 1.5f;

    private Rigidbody rb;
    private Vector3 movimiento;

    private int saltosMaximos = 2;
    private int saltosRestantes;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        saltosRestantes = saltosMaximos;
    }

    void Update()
    {
        float moverX = 0f;
        float moverZ = 0f;

        // Controles personalizados (seg�n tu configuraci�n)
        if (Input.GetKey(KeyCode.W))
            moverX = 1f;

        if (Input.GetKey(KeyCode.S))
            moverX = -1f;

        if (Input.GetKey(KeyCode.D))
            moverZ = -1f;

        if (Input.GetKey(KeyCode.A))
            moverZ = 1f;

        movimiento = new Vector3(moverX, 0f, moverZ).normalized;

        // Rotar hacia donde se mueve
        if (movimiento != Vector3.zero)
        {
            transform.forward = movimiento;
        }

        // Salto y doble salto
        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            Saltar();
            saltosRestantes--;
        }
    }

    void FixedUpdate()
    {
        float velocidadActual = velocidad;

        if (Input.GetKey(KeyCode.F))
        {
            velocidadActual *= multiplicadorBoost;
        }

        Vector3 velocidadFinal = movimiento * velocidadActual;

        // Mantener la velocidad vertical intacta
        rb.linearVelocity = new Vector3(velocidadFinal.x, rb.linearVelocity.y, velocidadFinal.z);
    }

    void Saltar()
    {
        float fuerzaSalto = Mathf.Sqrt(alturaSalto * -2f * Physics.gravity.y);

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contacto in collision.contacts)
        {
            // Si toca suelo
            if (contacto.normal.y > 0.5f)
            {
                saltosRestantes = saltosMaximos;
                break;
            }
        }
    }
}
