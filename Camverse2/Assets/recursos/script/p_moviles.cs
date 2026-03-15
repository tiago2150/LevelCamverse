using UnityEngine;

public class p_moviles : MonoBehaviour
{
    Vector3 start;
    public Vector3 end;
    public float Velocidad;

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
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
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