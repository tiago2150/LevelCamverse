using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public Transform referenciaDireccional;

    [Header("Salto")]
    public float alturaSalto = 1.2f;
    public LayerMask sueloLayer;

    [Header("Sistema de Golpes")]
    public GameObject canvas0;
    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject canvas3;

    [Header("Restricciones")]
    public bool puedeSaltar = true;
    public bool modoCenital = false;

    private Rigidbody rb;
    private Keyboard teclado;

    private int saltosMaximos = 2;
    private int saltosRestantes;

    private int golpes = 0;
    private int maxGolpes = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        teclado = Keyboard.current;

        rb.freezeRotation = true;
        saltosRestantes = saltosMaximos;

        ActualizarCanvas();
    }

    void Update()
    {
        if (teclado == null) return;

        if (teclado.spaceKey.wasPressedThisFrame && saltosRestantes > 0 && puedeSaltar)
        {
            Saltar();
        }
    }

    void FixedUpdate()
    {
        if (teclado == null) return;
        if (golpes >= maxGolpes) return;

        Vector3 direccion = Vector3.zero;

        // 🎥 MODO CENITAL
        if (modoCenital)
        {
            if (teclado.dKey.isPressed)
                direccion += Vector3.right;

            if (teclado.aKey.isPressed)
                direccion -= Vector3.right;

            if (teclado.sKey.isPressed)
                direccion -= Vector3.forward;

            if (teclado.wKey.isPressed)
                direccion += Vector3.forward;
        }
        // 🎮 MODO NORMAL
        else
        {
            if (referenciaDireccional == null) return;

            Vector3 forward = referenciaDireccional.forward;
            Vector3 right = referenciaDireccional.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            if (teclado.dKey.isPressed)
                direccion += forward;

            if (teclado.aKey.isPressed)
                direccion -= forward;

            if (teclado.sKey.isPressed)
                direccion += right;

            if (teclado.wKey.isPressed)
                direccion -= right;
        }

        direccion.Normalize();

        Vector3 nuevaVelocidad = new Vector3(
            direccion.x * velocidad,
            rb.linearVelocity.y,
            direccion.z * velocidad
        );

        rb.linearVelocity = nuevaVelocidad;
    }

    void Saltar()
    {
        if (!puedeSaltar) return;

        float fuerzaSalto = Mathf.Sqrt(2f * Mathf.Abs(Physics.gravity.y) * alturaSalto);

        Vector3 velocidadActual = rb.linearVelocity;
        velocidadActual.y = 0f;
        rb.linearVelocity = velocidadActual;

        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.VelocityChange);

        saltosRestantes--;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & sueloLayer) != 0)
        {
            saltosRestantes = saltosMaximos;
        }
    }

    public void RecibirGolpe()
    {
        if (golpes >= maxGolpes) return;

        golpes++;
        ActualizarCanvas();
    }

    public void ReiniciarVidas()
    {
        golpes = 0;
        Time.timeScale = 1f;
        ActualizarCanvas();
    }

    void ActualizarCanvas()
    {
        if (canvas0) canvas0.SetActive(false);
        if (canvas1) canvas1.SetActive(false);
        if (canvas2) canvas2.SetActive(false);
        if (canvas3) canvas3.SetActive(false);

        switch (golpes)
        {
            case 0:
                if (canvas0) canvas0.SetActive(true);
                break;

            case 1:
                if (canvas1) canvas1.SetActive(true);
                break;

            case 2:
                if (canvas2) canvas2.SetActive(true);
                break;

            case 3:
                if (canvas3) canvas3.SetActive(true);
                Time.timeScale = 0f;
                break;
        }
    }
}