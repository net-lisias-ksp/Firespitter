/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018-2020, LisiasT

    Developers: LisiasT, Snjo

    This file is part of Firespitter.
*/
Shader "Unlit/Texture" {
   Properties {
       _MainTex ("MainTex", 2D) = "black" {}
   }
   SubShader {
       Tags { "RenderType"="Opaque" }
       LOD 100
       Pass {
           Lighting Off
           SetTexture [_MainTex] { combine texture }
       }
   }
}
