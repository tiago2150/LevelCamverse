using UnityEngine;

public class final : MonoBehaviour
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

            Time.timeScale = 0f;
        }
    }
}