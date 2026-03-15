using UnityEngine;

public class SistemaGolpes : MonoBehaviour
{
    [Header("Canvas por daño")]
    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject canvas3;

    private int golpes = 0;
    private int maxGolpes = 3;

    void Start()
    {
        ActualizarCanvas();
    }

    public void RecibirGolpe()
    {
        if (golpes >= maxGolpes) return;

        golpes++;
        ActualizarCanvas();
    }

    void ActualizarCanvas()
    {
        // Desactivamos todos
        canvas1.SetActive(false);
        canvas2.SetActive(false);
        canvas3.SetActive(false);

        // Activamos según cantidad de golpes
        if (golpes == 1)
            canvas1.SetActive(true);

        if (golpes == 2)
            canvas2.SetActive(true);

        if (golpes >= 3)
            canvas3.SetActive(true);
    }
}