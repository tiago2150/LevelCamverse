using UnityEngine;

public class Movil : MonoBehaviour
{
    private Vector3 start;
    public Vector3 end;
    public float velocidad = 1f;

    void Start()
    {
        start = transform.position;
    }

    void FixedUpdate()
    {
        float t = (Mathf.Sin(velocidad * Time.time) + 1f) / 2f;
        transform.position = Vector3.Lerp(start, start + end, t);
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


