using UnityEngine;
using System.Collections.Generic;

public class Camaras : MonoBehaviour
{
    public Camera cameraU;
    public Camera cameraI;
    public Camera cameraO;
    public Camera cameraP;

    public Player player;

    private Camera currentCamera;

    // Guardamos posición ORIGINAL de cada cámara
    private Dictionary<Camera, Vector3> posicionesOriginales = new Dictionary<Camera, Vector3>();

    // Posición actual forzada (si estamos en un piso)
    private Vector3? posicionForzada = null;

    void Start()
    {
        posicionesOriginales[cameraU] = cameraU.transform.position;
        posicionesOriginales[cameraI] = cameraI.transform.position;
        posicionesOriginales[cameraO] = cameraO.transform.position;
        posicionesOriginales[cameraP] = cameraP.transform.position;

        ActivateCamera(cameraU);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            ActivateCamera(cameraU);

        else if (Input.GetKeyDown(KeyCode.I))
            ActivateCamera(cameraI);

        else if (Input.GetKeyDown(KeyCode.O))
            ActivateCamera(cameraO);

        else if (Input.GetKeyDown(KeyCode.P))
            ActivateCamera(cameraP);
    }

    void ActivateCamera(Camera cam)
    {
        cameraU.gameObject.SetActive(false);
        cameraI.gameObject.SetActive(false);
        cameraO.gameObject.SetActive(false);
        cameraP.gameObject.SetActive(false);

        cam.gameObject.SetActive(true);

        currentCamera = cam;

        // Si estamos en un piso, usamos la posición forzada
        if (posicionForzada.HasValue)
            cam.transform.position = posicionForzada.Value;
        else
            cam.transform.position = posicionesOriginales[cam];

        if (player != null)
            player.camaraActiva = cam.transform;
    }

    // 👇 MOVER A POSICIÓN EXACTA
    public void SetPosicion(Vector3 nuevaPosicion)
    {
        posicionForzada = nuevaPosicion;

        if (currentCamera != null)
            currentCamera.transform.position = nuevaPosicion;
    }

    // 👇 VOLVER A PISO 1
    public void ResetPosicion()
    {
        posicionForzada = null;

        if (currentCamera != null)
            currentCamera.transform.position = posicionesOriginales[currentCamera];
    }
}
