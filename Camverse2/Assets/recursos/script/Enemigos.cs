using UnityEngine;

public class enemigos : MonoBehaviour
{
    Vector3 start;
    public Vector3 end;
    public float Velocidad;

    [Header("Empuje")]
    public float fuerzaHorizontal = 12f;
    public float fuerzaVertical = 3f;

    void Start()
    {
        start = transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(
            start,
            start + end,
            (Mathf.Sin(Velocidad * Time.time) + 1f) / 2
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rbPlayer = collision.gameObject.GetComponent<Rigidbody>();

            if (rbPlayer != null)
            {
                // Dirección horizontal enemigo → jugador
                Vector3 direccion = collision.transform.position - transform.position;
                direccion.y = 0f;
                direccion.Normalize();

                rbPlayer.linearVelocity = Vector3.zero;

                Vector3 fuerzaFinal =
                    direccion * fuerzaHorizontal +
                    Vector3.up * fuerzaVertical;

                rbPlayer.AddForce(fuerzaFinal, ForceMode.Impulse);

                // 🔥 Avisamos al jugador que recibió golpe
                MovimientoJugador jugador = collision.gameObject.GetComponent<MovimientoJugador>();
                if (jugador != null)
                {
                    jugador.RecibirGolpe();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (Application.isPlaying)
            Gizmos.DrawLine(start, start + end);
        else
            Gizmos.DrawLine(transform.position, transform.position + end);
    }
}