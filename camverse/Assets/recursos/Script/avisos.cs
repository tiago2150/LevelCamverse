using UnityEngine;

public class avisos : MonoBehaviour
{
    [Header("Objeto del aviso (UI)")]
    public GameObject mensajeUI;

    [Header("Tiempo antes de ocultarse (0 = no usar tiempo)")]
    public float tiempoVisible = 0f;

    private float temporizador;
    private bool contandoTiempo = false;

    private void Start()
    {
        if (mensajeUI != null)
            mensajeUI.SetActive(false);
    }

    private void Update()
    {
        if (contandoTiempo)
        {
            temporizador -= Time.deltaTime;

            if (temporizador <= 0f)
            {
                OcultarMensaje();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MostrarMensaje();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OcultarMensaje();
        }
    }

    void MostrarMensaje()
    {
        if (mensajeUI == null) return;

        mensajeUI.SetActive(true);

        if (tiempoVisible > 0f)
        {
            temporizador = tiempoVisible;
            contandoTiempo = true;
        }
    }

    void OcultarMensaje()
    {
        if (mensajeUI == null) return;

        mensajeUI.SetActive(false);
        contandoTiempo = false;
    }
}
