using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using System;
using System.IO;
public class Stand2Script : baseMostrarBanners
{
    public RawImage banner1;
    public RawImage banner2;
    public RawImage logo1;
    public RawImage nombre_logo;
    public RawImage nombre_logo1;
    public RawImage logoexpomype1;
    public RawImage logoexpomype2;
    public RawImage recepcionista;
    public RawImage banderin1;
    public override RawImage Banner1()
    {
        return banner1;
    }
    public override RawImage Banner2()
    {
        return banner2;
    }

    public override RawImage[] Logos()
    {
        RawImage[] logos = new RawImage[1];
        logos[0] = logo1;
        return logos;
    }
    /* Funcion que retorna las imagenes**/
     public override RawImage[] Imagenes()
    {
      return this.GetChildImagenes("Canvas/RAW img/productos");
    }
    public override RawImage[] ProductosEstrellas()
    {
         return this.GetChildImagenes("Canvas/RAW img/productosEstrellas");
    }
    /* LOGO EXPOMYPE 1**/
    public override RawImage Logoexpomype1()
    {
        return logoexpomype1;
    }

    public override RawImage Banderin1()
    {
        return banderin1;
    }
    public override RawImage Logoexpomype2()
    {
        return logoexpomype2;
    }
    public override RawImage Nombre_logo()
    {
        return nombre_logo;
    }
    
    public override RawImage Recepcionista()
    {
        return recepcionista;
    }

    public override RawImage Nombre_logo1()
    {
        return nombre_logo1;
    }

    void Start()
    {
        
    }
    public override void ImagenDB(string parameters)
    {
        base.ImagenDB(parameters);
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
