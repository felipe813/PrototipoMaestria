using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controlador para las escenas de test
/// </summary>
public class ControladorTest : MonoBehaviour
{
    //Elementos del escenario de la escena test
    public GameObject Introduccion;
    public GameObject Pregunta1;
    public Dropdown Dropdown1;
    public GameObject Pregunta2;
     public Dropdown Dropdown2;
    public GameObject Pregunta3;
     public Dropdown Dropdown3;
    public GameObject Pregunta4;
     public Dropdown Dropdown4;
    public GameObject Pregunta5;
     public Dropdown Dropdown5;
    

    
    /// <summary>
    /// Método para mostrar la pregunta 1
    /// </summary>
    public void MostrarPregunta1()
    {
        DesactivarTodo();
        Pregunta1.SetActive(true);
    }

    /// <summary>
    /// Método para mostrar la pregunta 2
    /// </summary>
    public void MostrarPregunta2()
    {
        int idRecorrido = Recorrido.id;
        int respuesta = 5 - Dropdown1.value;
        AgregarRespuesta(idRecorrido, 1, respuesta);

        DesactivarTodo();       
        Pregunta2.SetActive(true);
       
    }

    /// <summary>
    /// Método para mostrar la pregunta 3
    /// </summary>
    public void MostrarPregunta3()
    {
        int idRecorrido = Recorrido.id;
        int respuesta = 5 - Dropdown2.value;
        AgregarRespuesta(idRecorrido, 2, respuesta);

        DesactivarTodo();
        Pregunta3.SetActive(true);
    }

    /// <summary>
    /// Método para mostrar la pregunta 4
    /// </summary>
    public void MostrarPregunta4()
    {
        int idRecorrido = Recorrido.id;
        int respuesta = 5 - Dropdown3.value;
        AgregarRespuesta(idRecorrido, 3, respuesta);

        DesactivarTodo();
        Pregunta4.SetActive(true);
    }

    /// <summary>
    /// Método para mostrar la pregunta 5
    /// </summary>
    public void MostrarPregunta5()
    {
        int idRecorrido = Recorrido.id;
        int respuesta = 5 - Dropdown4.value;
        AgregarRespuesta(idRecorrido, 4, respuesta);

        DesactivarTodo();
        Pregunta5.SetActive(true);
    }

    /// <summary>
    /// Método para mostrar la pregunta 5
    /// </summary>
    public void Salir()
    {
        int idRecorrido = Recorrido.id;
        int respuesta = 5 - Dropdown5.value;
        AgregarRespuesta(idRecorrido, 5, respuesta);

        Application.Quit();
    }


    private void DesactivarTodo(){
        Introduccion.SetActive(false);
        Pregunta1.SetActive(false);
        Pregunta2.SetActive(false);
        Pregunta3.SetActive(false);
        Pregunta4.SetActive(false);
        Pregunta5.SetActive(false);
    }

    private void AgregarRespuesta(int idRecorrido, int idPegunta, int respuestaPregunta){
        string jsonPOST = "{" +
                 "\"IdRecorrido\":\"" + idRecorrido + "\"," +
                 "\"IdPregunta\":\"" + idPegunta + "\"," +
                 "\"Respuesta\": "+respuestaPregunta+
                 "}";

        string respuesta = ServicioREST.EjecutarOperacion(ServicioREST.direccionServicio + "/api/RespuestasTest", "POST",jsonPOST);
        if (respuesta == null)
        {
            Debug.Log("!!!Error creando respuesta");
        }
    }
}
