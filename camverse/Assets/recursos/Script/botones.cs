using UnityEngine;
using UnityEngine.SceneManagement;

public class botones : MonoBehaviour
{
    [Header("Canvas a activar")]
    public GameObject canvasMenu;

    [Header("Nombres de escenas")]
    public string escena1;
    public string escena2;

    // 🔹 Activar menú
    public void ActivarMenu()
    {
        canvasMenu.SetActive(true);
    }

    // 🔹 Regresar / cerrar menú
    public void CerrarMenu()
    {
        canvasMenu.SetActive(false);
    }

    // 🔹 Cambiar a escena 1
    public void IrAEscena1()
    {
        SceneManager.LoadScene(escena1);
    }

    // 🔹 Cambiar a escena 2
    public void IrAEscena2()
    {
        SceneManager.LoadScene(escena2);
    }

    // 🔹 Salir del juego (para futuro botón)
    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
