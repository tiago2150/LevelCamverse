using UnityEngine;
using UnityEngine.InputSystem;

public class rotacion : MonoBehaviour
{
    public float suavizado = 10f;
    private float anguloObjetivo = 0f;

    public GameObject panelX;
    public GameObject panel90;
    public GameObject panel180;
    public GameObject panel270;

    public bool rotacionBloqueada = false;

    private Keyboard teclado;

    void Start()
    {
        teclado = Keyboard.current;

        anguloObjetivo = 0f;
        transform.rotation = Quaternion.Euler(0f, anguloObjetivo, 0f);

        ActivarPanel(panelX);
    }

    void Update()
    {
        if (teclado == null) return;
        if (rotacionBloqueada) return;

        if (teclado.pKey.wasPressedThisFrame)
        {
            anguloObjetivo = 0f;
            ActivarPanel(panelX);
        }

        if (teclado.iKey.wasPressedThisFrame)
        {
            anguloObjetivo = 90f;
            ActivarPanel(panel90);
        }

        if (teclado.uKey.wasPressedThisFrame)
        {
            anguloObjetivo = 180f;
            ActivarPanel(panel180);
        }

        if (teclado.oKey.wasPressedThisFrame)
        {
            anguloObjetivo = 270f;
            ActivarPanel(panel270);
        }
    }

    void LateUpdate()
    {
        Quaternion rotacionDeseada = Quaternion.Euler(0f, anguloObjetivo, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotacionDeseada, suavizado * Time.deltaTime);
    }

    void ActivarPanel(GameObject panelActivo)
    {
        panelX.SetActive(false);
        panel90.SetActive(false);
        panel180.SetActive(false);
        panel270.SetActive(false);

        panelActivo.SetActive(true);
    }
}