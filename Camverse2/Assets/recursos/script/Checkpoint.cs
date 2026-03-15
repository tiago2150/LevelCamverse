using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Jugador")]
    public Transform jugador;

    [Header("Canvas de Checkpoints")]
    public GameObject canvasCheckpoint;

    void Start()
    {
        if (canvasCheckpoint != null)
        {
            canvasCheckpoint.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleCanvas();
        }
    }

    void ToggleCanvas()
    {
        if (canvasCheckpoint != null)
        {
            canvasCheckpoint.SetActive(!canvasCheckpoint.activeSelf);
        }
    }

    public void Teletransportar(Transform destino)
    {
        if (jugador != null && destino != null)
        {
            jugador.position = destino.position;
        }

        if (canvasCheckpoint != null)
        {
            canvasCheckpoint.SetActive(false);
        }
    }
}