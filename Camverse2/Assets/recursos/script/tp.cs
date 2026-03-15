using UnityEngine;

public class tp : MonoBehaviour
{
    [Header("Destino del Teletransporte")]
    public Transform destino; // Arrastra aquí el collider destino

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Teletransportar al jugador a la posición del destino
            other.transform.position = destino.position;
        }
    }
}

