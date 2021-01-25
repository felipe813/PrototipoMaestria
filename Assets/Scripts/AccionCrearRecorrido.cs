using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;



/// <summary>
/// Acción para calificar una imagen
/// </summary>
public class AccionCrearRecorrido : IComando
{
    private string _prefijoComponenteImagen ="ImagenRecorrido_";
    private string _prefijoComponenteImagenFinal ="ImagenRecorrido";
    private string _ftp = "ftp://RepositorioImagenes:repositorio@files.000webhost.com/Imagenes/";
    private string _nombreAccion = "CrearRecorrido";
    private int _tamanoStandarImagen = 900;
    private List<String> _mensajesProgreso;
    public MonoBehaviour _componente;
    
    public AccionCrearRecorrido(MonoBehaviour componente){
        this._componente = componente;

        this._mensajesProgreso = new List<string>();
        this._mensajesProgreso.Add("Verá imágenes del posconflicto Colombiano");
        this._mensajesProgreso.Add("Verá imágenes del posconflicto Colombiano");
        this._mensajesProgreso.Add("Verá imágenes del posconflicto Colombiano");
        this._mensajesProgreso.Add("Puede ver las imágenes las veces que desee");
        this._mensajesProgreso.Add("Puede ver las imágenes las veces que desee");
        this._mensajesProgreso.Add("Use los botones laterales para cambiar las imágenes");
        this._mensajesProgreso.Add("Use los botones laterales para cambiar las imágenes");
        this._mensajesProgreso.Add("No olvide calificar las imágenes.");
        this._mensajesProgreso.Add("No olvide calificar las imágenes.");
        this._mensajesProgreso.Add("No olvide calificar las imágenes.");
    }

    public void EjecutarAccion(){
        
        DAORecorrido daoRecorrido = new DAORecorrido();
        if(daoRecorrido.CrearNuevoRecorrido()){
            this._componente.StartCoroutine(CargarImagenes(Recorrido.listaImagenes));
        }else{
            InterfazProgreso.ActualizarAccion(_nombreAccion,100,"No se pudo crear el recorrido, consulte con el administrador");
        }
    }
    
    public void DeshacerAccion(){
        
    }

    private IEnumerator CargarImagenes(List<Imagen> imagenes){
        InterfazProgreso.ActualizarAccion(_nombreAccion,10,"Iniciando creación del recorrido");
        int index = 1;
        int total = imagenes.Count;
        int delta = 90/total;
        foreach (Imagen imagen in imagenes)
        {
            string nombreComponente = this._prefijoComponenteImagen+index;
            GameObject ContenedorImagen = GameObject.Find(nombreComponente);
            ContenedorImagen.SetActive(false);

            string url = this._ftp+imagen.direccionImagen;
            var loaded = UnityWebRequestTexture.GetTexture(url);
            //loaded.timeout = 0;  
            yield return loaded.SendWebRequest();
            if(loaded.isNetworkError || loaded.isHttpError){
                Debug.Log("Error cargando imagen "+url+". "+loaded.error);                
            }else{
                try{                                    
                    ContenedorImagen.name = _prefijoComponenteImagenFinal+imagen.idImagen;

                    Texture2D texture= ((DownloadHandlerTexture)loaded.downloadHandler).texture as Texture2D;

                    RectTransform rt = (RectTransform)ContenedorImagen.transform;
                    float proporcion = Convert.ToSingle(texture.width)/Convert.ToSingle(texture.height);
                    if(proporcion>1){
                        //Imagen mas ancha que larga
                        rt.sizeDelta = new Vector2((_tamanoStandarImagen*proporcion),_tamanoStandarImagen);
                    }else{
                        rt.sizeDelta = new Vector2(_tamanoStandarImagen,(_tamanoStandarImagen*proporcion));
                    }              
                    
                    Sprite sprite = Sprite.Create(texture,new Rect(0,0, texture.width, texture.height),Vector2.one/2);                                    
                    ContenedorImagen.GetComponent<Image>().sprite = sprite; 

                    Recorrido.AgregarGameObjectImagen(ContenedorImagen,imagen.idImagen);
                    

                }catch (Exception e){
                    Debug.Log("Error cargando imagen "+url+" ."+e.Message);
                    Recorrido.RemoverImagenById(imagen.idImagen);                   
                    MonoBehaviour.Destroy(ContenedorImagen);        
                }      
            }
            InterfazProgreso.ActualizarAccion(_nombreAccion,10+index*delta,_mensajesProgreso[index%_mensajesProgreso.Count]);
            index++;  
        }
        if(Recorrido.listaImagenes.Count>0){
            Recorrido.imagenActual= Recorrido.listaImagenes[0].idImagen;
            Recorrido.MostrarImagen(Recorrido.imagenActual);
        }
        InterfazProgreso.ActualizarAccion(_nombreAccion,100,"Recorrido creado con éxito");                             
    }

    private string URLEncode(string url){
        //  Á              %C1
        //  É              %C9
        //  Í              %CD
        //  Ó              %D3
        //  Ú              %DA
        //  á              %E1
        //  é              %E9
        //  í              %ED
        //  ó              %F3
        //  ú              %FA
        url = url.Replace(" ","%20");
        url = url.Replace("Á","%C1");
        url = url.Replace("É","%C9");
        url = url.Replace("Í","%CD");
        url = url.Replace("Ó","%D3");
        url = url.Replace("Ú","%DA");
        url = url.Replace("á","%E1");
        url = url.Replace("é","%E9");
        url = url.Replace("í","%ED");
        url = url.Replace("ó","%F3");
        url = url.Replace("ú","%FA");
        return url;
    }

}
