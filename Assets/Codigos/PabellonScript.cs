using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
public class PabellonScript : MonoBehaviour
{
    
    void Start()
    {
       
    }

    public RawImage PublicidadVert1;
    public RawImage PublicidadVert2;
    public RawImage PublicidadHori1;
    public RawImage PublicidadHori2;
    public RawImage BannerExpoHorz;


    // Update is called once per frame
    void Update()
    {
        
    }
     public void FocusCanvas (string p_focus) {
    #if !UNITY_EDITOR && UNITY_WEBGL
    if (p_focus == "0") {
        WebGLInput.captureAllKeyboardInput = false;
    } else {
        WebGLInput.captureAllKeyboardInput = true;
    }
    #endif
    }
    void ImagenBanner(string imagenRuta, RawImage imagen) {
               if (!string.IsNullOrWhiteSpace(imagenRuta)) {
                    StartCoroutine(ImagenRequest(imagenRuta, (UnityWebRequest req1) => {
                            if ( req1.result == UnityWebRequest.Result.Success ) {
                                imagen.texture = DownloadHandlerTexture.GetContent(req1);
                            }
                    }));     
                }


    }
    void ImagenDb(string url){

         StartCoroutine(JsonRequest(url, (UnityWebRequest req) => {
              var json_text = req.downloadHandler.text;
              Pabellon pabellon = JsonUtility.FromJson<Pabellon>(json_text);                     
              ImagenBanner(pabellon.PublicidadVert1, this.PublicidadVert1 );
              ImagenBanner(pabellon.PublicidadVert2, this.PublicidadVert2 );
              ImagenBanner(pabellon.PublicidadHori1, this.PublicidadHori1 );
              ImagenBanner(pabellon.PublicidadHori2, this.PublicidadHori2 );
              ImagenBanner(pabellon.BannerExpoHorz, this.BannerExpoHorz );


        }));

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
}

[System.Serializable]
    public class Pabellon
    {
    public string PublicidadVert1;
    public string PublicidadVert2;
    public string PublicidadHori1;
    public string PublicidadHori2;
    public string BannerExpoHorz;

    }
