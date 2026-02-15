using UnityEngine;
using UnityEngine.SceneManagement;

public class cambiosdeescena : MonoBehaviour
{
    public string nombreEscena;      // Nombre exacto de la escena
    public GameObject panelUI;       // Panel que aparece
    public string tagJugador = "Player";

    private bool dentro = false;

    void Start()
    {
        if (panelUI != null)
            panelUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            dentro = true;

            if (panelUI != null)
                panelUI.SetActive(true);

            // Cambiar de escena
            SceneManager.LoadScene(nombreEscena);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            dentro = false;

            if (panelUI != null)
                panelUI.SetActive(false);
        }
    }
}
