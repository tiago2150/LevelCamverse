using UnityEngine;

public class ZonaCenital : MonoBehaviour
{
    public camara camaraPrincipal;
    public rotacion sistemaRotacion;

    private void OnTriggerEnter(Collider other)
    {
        MovimientoJugador jugador = other.GetComponent<MovimientoJugador>();

        if (jugador != null)
        {
            camaraPrincipal.ActivarCenital(true);

            jugador.modoCenital = true;
            jugador.puedeSaltar = false;

            sistemaRotacion.rotacionBloqueada = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MovimientoJugador jugador = other.GetComponent<MovimientoJugador>();

        if (jugador != null)
        {
            camaraPrincipal.ActivarCenital(false);

            jugador.modoCenital = false;
            jugador.puedeSaltar = true;

            sistemaRotacion.rotacionBloqueada = false;
        }
    }
}