using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase para actualizar el prgoreso en pantalla cuando se están ejecutando varias acciones al tiempo
/// </summary>
public class Progreso
{
    /// <summary>
    /// Nombre del elemento en pantalla donde se mostrará el texto de porcentaje
    /// </summary>
    private static string _nombreTextoPorcentaje = "TextoPorcentaje";
    /// <summary>
    /// Nombre del elemento en pantalla donde se mostrará el texto del mensaje
    /// </summary>
    private static string _nombreTextoMensaje = "TextoMensaje";
    private static string _nombreBarra = "BarraSuperior";

    private static string _nombreLoader = "Loader";

    /// <summary>
    /// Método para actualizar los valores en pantalla de carga
    /// </summary>
    /// <param name="Mensaje">Nuevo mensaje a mostrar</param>
    /// <param name="PorcentajeTotal">Nuevo porcentaje a mostrar</param>
    public static void ActualizarAccion(string Mensaje, double PorcentajeTotal)
    {
        if (Mensaje != null) ActualizarMensaje(Mensaje);
        ActualizarPorcentajeLoader(PorcentajeTotal);
        if(PorcentajeTotal == 100)
        RemoverLoader();
    }

    /// <summary>
    /// Método para actualizar especificamente el porcentaje de accion en pantalla
    /// </summary>
    /// <param name="PorcentajeTotal">Porcentaje actual</param>
    private static void ActualizarPorcentajeLoader(double PorcentajeTotal)
    {
        Debug.Log("El porcentaje TOTAL actual es: " + PorcentajeTotal + " %");
        GameObject porcentajeGameObject = GameObject.Find(_nombreTextoPorcentaje);
        if (porcentajeGameObject != null)
        {
            Text textPorcentaje = porcentajeGameObject.GetComponent<Text>();
            textPorcentaje.text = PorcentajeTotal.ToString("F0") + "%";
        }
        GameObject barraGameObject = GameObject.Find(_nombreBarra);
        if (barraGameObject != null)
        {
            float valorEscale = (float)(0.1 + PorcentajeTotal * 0.009);
            barraGameObject.transform.localScale = new Vector3(valorEscale,1,1);
        }
    }

    /// <summary>
    /// Método para actualizar especificamente el mensaje en pantalla
    /// </summary>
    /// <param name="Mensaje">Texto con el mensaje a mostrar</param>
    private static void ActualizarMensaje(string Mensaje)
    {
        Debug.Log("Mensaje progreso: " + Mensaje);

        GameObject mensajeGameObject = GameObject.Find(_nombreTextoMensaje);
        if (mensajeGameObject != null)
        {
            Text textMensaje = mensajeGameObject.GetComponent<Text>();
            textMensaje.text = Mensaje;
        }

    }

    private static void RemoverLoader()
    {
        Debug.Log("Loader terminado: ");

        GameObject loaderGameObject = GameObject.Find(_nombreLoader);
        if (loaderGameObject != null)
        {
            loaderGameObject.SetActive(false);
        }

    }
}
