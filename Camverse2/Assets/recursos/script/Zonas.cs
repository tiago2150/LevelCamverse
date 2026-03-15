using UnityEngine;

public class Zonas : MonoBehaviour
{
    public GameObject canvasMensaje;

    private void Start()
    {
        if (canvasMensaje != null)
            canvasMensaje.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador entró al trigger");

            if (canvasMensaje != null)
                canvasMensaje.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador entró al trigger");

            if (canvasMensaje != null)
                canvasMensaje.SetActive(false);
        }
    }
}