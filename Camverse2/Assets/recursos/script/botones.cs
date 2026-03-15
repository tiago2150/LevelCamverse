using UnityEngine;
using UnityEngine.SceneManagement;

public class botones : MonoBehaviour
{
    public GameObject panelPausa;
    public MovimientoJugador jugador;

    private bool estaPausado = false;

    void Start()
    {
        if (panelPausa != null)
            panelPausa.SetActive(false);
    }

    public void PausarJuego()
    {
        if (estaPausado) return;

        estaPausado = true;
        panelPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitarPausa()
    {
        estaPausado = false;
        panelPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.name);
    }

    public void CambiarEscena(string nombreEscena)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscena);
        Debug.Log("cargande escena: " + nombreEscena);
    }

    public void SalirDelJuego()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // 🔥 BOTÓN PARA REINICIAR VIDAS
    public void ReiniciarVidasBoton()
    {
        if (jugador != null)
        {
            jugador.ReiniciarVidas();
        }
    }
}