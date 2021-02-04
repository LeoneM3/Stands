using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
public class baseMostrarBanners : MonoBehaviour
{
    

    public virtual RawImage Banner1() { return null; }
    public virtual RawImage Banner2() { return null; }
    public virtual RawImage Banner3() { return null; }
    public virtual RawImage Banner4() { return null; }
    public virtual RawImage Logo() { return null; }
    public virtual RawImage Nombre_logo() { return null; }
    public virtual RawImage Nombre_logo1() { return null; }
    public virtual RawImage Nombre_logo2() { return null; }
    public virtual RawImage[] Logos() { return null; }
    public virtual RawImage[] Imagenes() 
    { 
        List<RawImage> imagenes = new List<RawImage>();
        return imagenes.ToArray(); 
    }

    public virtual RawImage[] ProductosEstrellas()
    { 
        List<RawImage> imagenes = new List<RawImage>();
        return imagenes.ToArray(); 
    }

    public virtual RawImage Logoexpomype1() { return null; }
    public virtual RawImage Logoexpomype2() { return null; }
    public virtual RawImage Recepcionista() { return null; }
    public virtual RawImage Banderin1() { return null; }

    public void FocusCanvas (string p_focus) {
    #if !UNITY_EDITOR && UNITY_WEBGL
    if (p_focus == "0") {
       // WebGLInput.captureAllKeyboardInput = false;
    } else {
        //WebGLInput.captureAllKeyboardInput = true;
    }
    #endif
    }

    public RawImage[] GetChildImagenes(string ruta) {
         RawImage[] imagenes = {};
        GameObject child = GameObject.Find(ruta);  // parent.transform.Find("Productos").gameObject;
            if (child != null) {
            int total = child.transform.childCount;
             Debug.Log("total de imagenes child" + total.ToString());
                    if (total > 0) {
                    imagenes= new RawImage[total];
                    for (int i  = 0; i < total; i++) {
                            var imagen = child.transform.GetChild(i).GetComponent<RawImage>(); // imagenes[i].GetComponent<RawImage>();
                            imagenes[i] = imagen;
                        }
                }
            }
            
        return imagenes;
    }
    /** 
        Funcion que agrega los banners y recepnista
    **/
    private void ConstruirCentroFeria(string wscentro_feria) 
    {
        string json_text = "";
        string webserviceUrl = wscentro_feria.Replace("timespan", DateTime.Now.ToString("yyyyMMddhhmmss"));
        StartCoroutine(JsonRequest(webserviceUrl, (UnityWebRequest req) => {
                json_text = req.downloadHandler.text;
                ObjectCentroFerias centroFerias = JsonUtility.FromJson<ObjectCentroFerias>(json_text);
                var edecanes = centroFerias.Edecanes;
                int t = (int)DateTime.Now.Ticks;
                UnityEngine.Random.InitState(t);
                int n = UnityEngine.Random.Range(0, edecanes.Length);
                string url = edecanes[n].url;

                this.cargarImagen( url, Recepcionista() );
                this.cargarImagen( centroFerias.Banner1, Logoexpomype1() );
                this.cargarImagen( centroFerias.Banner2, Logoexpomype2() );
                this.cargarImagen( centroFerias.Banderin1, Banderin1() );
                /* if (!string.IsNullOrWhiteSpace(url) && Recepcionista() != null) {
                    StartCoroutine(ImagenRequest(url, (UnityWebRequest req1) => {
                            var texture = DownloadHandlerTexture.GetContent(req1);
                            Recepcionista().texture = texture;
                           // Recepcionista().sprite = Sprite.Create( texture, new Rect(0,0, texture.width, texture.height ), new Vector2(0.5f, 0.5f)  );
                    }));
                 }
                
                if (!string.IsNullOrWhiteSpace(centroFerias.Banner1) && Logoexpomype1() != null) {
                    StartCoroutine(ImagenRequest(centroFerias.Banner1, (UnityWebRequest req1) => {
                            var texture = DownloadHandlerTexture.GetContent(req1);
                            Logoexpomype1().texture = texture;
                            //Logoexpomype1().sprite = Sprite.Create( texture, new Rect(0,0, texture.width, texture.height ), new Vector2(0.5f, 0.5f)  );
                            
                    }));
                }

                if (!string.IsNullOrWhiteSpace(centroFerias.Banner2) && Logoexpomype2() != null) {
                    StartCoroutine(ImagenRequest(centroFerias.Banner2, (UnityWebRequest req1) => {
                            var texture = DownloadHandlerTexture.GetContent(req1);
                            Logoexpomype2().texture = texture;
                           //Logoexpomype2().sprite = Sprite.Create( texture, new Rect(0,0, texture.width, texture.height ), new Vector2(0.5f, 0.5f)  );
                    }));
                }*/
            
        }));
    }
    public virtual void ImagenDB(string parameters)
    {
      
        string path = Application.dataPath.Split('/')[3];
        string config = "";
         StartCoroutine(JsonRequest("/" + path + "/unity-conf/conf.json?" + DateTime.Now.ToString("yyyyMMddhhmmss"), (UnityWebRequest reqconf) => {
             config = reqconf.downloadHandler.text;
             Debug.Log(config);
        ObjectMyConfig myConfig = JsonUtility.FromJson<ObjectMyConfig>(config);
        string webservice = myConfig.estand.Replace("timespan", DateTime.Now.ToString("yyyyMMddhhmmss"));
        StartCoroutine(JsonRequest(webservice + parameters, (UnityWebRequest req) =>
    {
        if (req.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log($"{req.error}: {req.downloadHandler.text}");
        }
        else
        {
            try
            {
                ConstruirCentroFeria( myConfig.centro_ferias_eventos + parameters );
            }
            catch (System.Exception e)
            {
                
                Debug.Log("Error en carga de centro feria " + e.Message);
            }
            string json = req.downloadHandler.text;
            var list =  JsonUtility.FromJson<ObjectDatos>(json);
           /**listado de productos*/
            ObjectImagen i1 = new ObjectImagen();
            ObjectImagen i2 = new ObjectImagen();
            ObjectImagen i3 = new ObjectImagen();

            var data = new List<ObjectImagen>();
            i1.url = list.Producto1;
            data.Add(i1);
            i2.url = list.Producto2;
            data.Add(i2);
            i3.url = list.Producto3;
            data.Add(i3);

            /**listado de productos estrellas*/
            ObjectImagen pe1 = new ObjectImagen();
            ObjectImagen pe2 = new ObjectImagen();
            ObjectImagen pe3 = new ObjectImagen();
            ObjectImagen pe4 = new ObjectImagen();
            var pestrellas = new List<ObjectImagen>();
            pe1.url = list.ProductoEstrella1;
            pestrellas.Add(pe1);
            pe2.url = list.ProductoEstrella2;
            pestrellas.Add(pe2);
            pe3.url = list.ProductoEstrella3;
            pestrellas.Add(pe3);
            pe4.url = list.ProductoEstrella4;
            pestrellas.Add(pe4);

            

            RawImage[] Imagenes = this.Imagenes();
            RawImage[] ProductosEstrellas = this.ProductosEstrellas();
            Debug.Log("lista de imagenes " + Imagenes.Length);

            /*creacion de los banners**/
            if (!string.IsNullOrWhiteSpace(list.Banner1) && Banner1() != null) {
                    StartCoroutine(ImagenRequest(list.Banner1, (UnityWebRequest req1) => {
                            Banner1().texture = DownloadHandlerTexture.GetContent(req1);
                    }));
            }
           if (!string.IsNullOrWhiteSpace(list.Banner2) && Banner2() != null) {
                    StartCoroutine(ImagenRequest(list.Banner2, (UnityWebRequest req1) => {
                            Banner2().texture = DownloadHandlerTexture.GetContent(req1);
                    }));
            }
            if (!string.IsNullOrWhiteSpace(list.Banner3) && Banner3() != null) {
                    StartCoroutine(ImagenRequest(list.Banner3, (UnityWebRequest req1) => {
                            Banner3().texture = DownloadHandlerTexture.GetContent(req1);
                    }));
            }

             if (!string.IsNullOrWhiteSpace(list.Banner4) && Banner4() != null) {
                    StartCoroutine(ImagenRequest(list.Banner4, (UnityWebRequest req1) => {
                            Banner4().texture = DownloadHandlerTexture.GetContent(req1);
                    }));
            }
            /**creacion del logo*/
             if (!string.IsNullOrWhiteSpace(list.logo) && Logo() != null) {
                    StartCoroutine(ImagenRequest(list.logo, (UnityWebRequest req1) => {
                            Logo().texture = DownloadHandlerTexture.GetContent(req1);
                    }));
            }

              if (!string.IsNullOrWhiteSpace(list.nombre_logo) && Nombre_logo() != null) {
                    StartCoroutine(ImagenRequest(list.nombre_logo, (UnityWebRequest req1) => {
                            var textura = DownloadHandlerTexture.GetContent(req1);
                            Nombre_logo().texture = textura;
                            if (Nombre_logo1() != null) {
                                Nombre_logo1().texture = textura;
                            }
                            
                            if (Nombre_logo2() != null) {
                                Nombre_logo2().texture = textura;
                            }
                    }));
            }

            /** logos **/
            if (this.Logos() != null && !string.IsNullOrWhiteSpace(list.logo) ) {
                RawImage[] logos = Logos();
                for (int i = 0, len = logos.Length; i < len; i++) {
                    RawImage logo = logos[i];
                    StartCoroutine(ImagenRequest(list.logo, (UnityWebRequest req1) => {
                              logo.texture = DownloadHandlerTexture.GetContent(req1);
                    }));
                }
            }
            /**productos estrellas**/
            for (int i  =0, len = ProductosEstrellas.Length; i< len; i++) {
                  RawImage imagen = ProductosEstrellas[i];
                    if (i <= 3) {
                        string url = pestrellas[i].url;
                        if (!string.IsNullOrEmpty(url)) {
                            StartCoroutine(ImagenRequest(url, (UnityWebRequest req1) => {
                            imagen.texture = DownloadHandlerTexture.GetContent(req1);
                            }));
                        }
                    } else {
                        UnityEngine.Random.InitState( i + DateTime.Now.Millisecond );
                        int n = UnityEngine.Random.Range(0, 4);
                        string url = pestrellas[n].url;
                        if (!string.IsNullOrEmpty(url)) {
                            StartCoroutine(ImagenRequest(url, (UnityWebRequest req1) => {
                            imagen.texture = DownloadHandlerTexture.GetContent(req1);
                            }));
                        }
                    }
            }
            /**imagenes de productos**/
            if (data.Count > 0)
            {
                for (int i = 0, len = Imagenes.Length; i < len; i++) {
                    RawImage imagen = Imagenes[i];
                    if (i <= 2) {
                        string url = data[i].url;
                        if (!string.IsNullOrEmpty(url)) {
                            StartCoroutine(ImagenRequest(url, (UnityWebRequest req1) => {
                            imagen.texture = DownloadHandlerTexture.GetContent(req1);
                            }));
                        }
                    } else {
                        UnityEngine.Random.InitState( i + DateTime.Now.Millisecond );
                        int n = UnityEngine.Random.Range(0, 3);
                        string url = data[n].url;
                        if (!string.IsNullOrEmpty(url)) {
                            StartCoroutine(ImagenRequest(url, (UnityWebRequest req1) => {
                            imagen.texture = DownloadHandlerTexture.GetContent(req1);
                            }));
                        }
                    }
                    
                }
     
            } else {
                Debug.Log("No hay datos de stands");
            }
       


            }
        }));
         }));
 
    }

      public void cargarImagen( string imagenUrl, RawImage rawImage ) {
            if (!string.IsNullOrWhiteSpace(imagenUrl) && rawImage != null) {
                    StartCoroutine(ImagenRequest(imagenUrl, (UnityWebRequest req1) => {
                        if (req1.result == UnityWebRequest.Result.Success) {
                                rawImage.texture = DownloadHandlerTexture.GetContent(req1);
                        }
                    }));
            }
    }
     IEnumerator JsonRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();
            callback(req);
        }
    }
    IEnumerator ImagenRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest req = UnityWebRequestTexture.GetTexture(url))
        {
            yield return req.SendWebRequest();
            callback(req);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

[System.Serializable]
public class ObjectImagen
{
    public string Guid;
    public string url;
    public ObjectImagen() {
        this.Guid = System.Guid.NewGuid().ToString(); 
    }
}
[System.Serializable]
public class ObjectDatos
{
    public string Producto1;
    public string Producto2;
    public string Producto3;
    public string ProductoEstrella1;
    public string ProductoEstrella2;
    public string ProductoEstrella3;
    public string ProductoEstrella4;
    public string logo;
    public string Banner1;
    public string Banner2;
    public string Banner3;
    public string Banner4;
    public string nombre_logo;
    public string Nombre_Empresa;
}
[System.Serializable] 
public class ObjectCentroFerias
{
    public string Banner1;
    public string Banner2;
    public string Banderin1;
    public ObjectImagen[] Edecanes; 
}

[System.Serializable]
public class ObjectMyConfig
{
    public string estand;
    public string centro_ferias_eventos;
}
