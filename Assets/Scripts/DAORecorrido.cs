using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class DAORecorrido
{
    private string cantidadImagenes = "10";
    /// <summary>
    /// Se crea un recorrido nuevo en la base de datos con 
    /// </summary>
    /// <returns></returns>
    public bool CrearNuevoRecorrido()
    {
        
        string respuesta = ServicioREST.EjecutarOperacion(ServicioREST.direccionServicio + "/api/imagenesRandom/" + cantidadImagenes, "GET");
        if (respuesta == null)
        {
            Debug.Log("!!!Error creando el recorrido");
            return false;
        }
        JSONNode listaImagenes = JSON.Parse(respuesta)["Imagenes"];
        string listaImagenesJSON = "[";
        if (listaImagenes != null)
        {
            foreach (var imagen in listaImagenes)
            {
                listaImagenesJSON+="\""+imagen.Value["Id"]+"\""+",";
            }
            listaImagenesJSON = listaImagenesJSON.TrimEnd(',');
            listaImagenesJSON+="]";

            int idUsuario = Usuario.idUsuario;
            string jsonPOST = "{" +
                 "\"IdUsuario\":\"" + idUsuario + "\"," +
                 "\"IdImagenes\": "+listaImagenesJSON+
                 "}";

            respuesta = ServicioREST.EjecutarOperacion(ServicioREST.direccionServicio + "/api/recorridos", "POST",jsonPOST);
            JSONNode recorrido = JSON.Parse(respuesta)["Recorrido"];
            Recorrido.fechaRecorrido = System.DateTime.Parse(recorrido["FechaRecorrido"]);
            Recorrido.id = recorrido["Id"];
            JSONNode imagenes = recorrido["Imagenes"];
            if(Recorrido.listaImagenes == null) Recorrido.listaImagenes = new List<Imagen>();
            foreach (var imagen in imagenes)
            {
                Imagen img = new Imagen{
                    idImagen = imagen.Value["IdImagen"],
                    direccionImagen = imagen.Value["DireccionImagen"],
                    nombreImagen = imagen.Value["NombreImagen"],
                    calificacion = imagen.Value["Calificacion"],
                };
                Recorrido.listaImagenes.Add(img);
            }            
            return true;
        }
        else
        {
            Debug.Log("!!!Error creando el recorrido");
            return false;
        }
    }


    public bool CalificarImagen(int idImagen,int idRecorrido, int calificacion)
    {
        Debug.Log("Se calificará imagen "+idImagen+" del recorrido "+idRecorrido+" con calificacion "+calificacion);
        if(idImagen == 0 || idRecorrido == 0){
            return false;
        }
        string jsonPUT = "{" +
                "\"IdRecorrido\": \"" + idRecorrido + "\"," +
                "\"IdImagen\": \"" + idImagen + "\"," +
                "\"Calificacion\": \"" + calificacion + "\""+
                "}";
        
        string respuesta = ServicioREST.EjecutarOperacion(ServicioREST.direccionServicio+"/api/recorridos", "PUT",jsonPUT);
        if(respuesta == null){
            Debug.Log("!!!No se pudo hacer la calificacion");
            return false;
        }
        //Debug.Log(respuesta);
        return true;
    }

    
}
