Shader "ForestVision/MaskTexture" {
Properties{
    _MainTex ("Main Texture (RGB)", 2D) = ""
    _PathTex ("Path Texture (RGB)", 2D) = ""
    _PathMask ("Path Mask (A)", 2D) = ""
}
SubShader {
    Lighting On
    Material {
        Ambient [_Color]
        Diffuse [_Color]
    }
    Pass{  
        SetTexture [_MainTex]
 
        SetTexture [_PathMask]{
            combine previous, texture
        }
               
        SetTexture [_PathTex]{
            combine texture lerp(previous) previous
        }
    }
}
Fallback "Diffuse"
}