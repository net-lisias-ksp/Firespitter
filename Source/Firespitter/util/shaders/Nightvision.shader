/*
Firespitter /L
Copyright 2013-2018, Andreas Aakvik Gogstad (Snjo)
Copyright 2018-2020, LisiasT

    Developers: LisiasT, Snjo

    This file is part of Firespitter.
*/
Shader "Nightvision" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
    }
    SubShader {
//      Tags { "RenderType"="Opaque" }     -- works
        Tags { "RenderType"="Opaque" }
//      Tags { "LightMode"="PixelOrNone" } --  no
        Pass {
            Blend SrcColor DstColor
            SetTexture [_MainTex] {
                constantColor (0.5,0.7,0.5,0.5)" +
                combine constant + texture
            }
        }
        Pass {
            Blend SrcColor DstColor
            SetTexture [_MainTex] {
                combine texture * previous
            }
        }
    }
}
