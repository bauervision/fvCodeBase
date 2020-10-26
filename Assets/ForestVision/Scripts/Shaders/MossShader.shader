Shader "Custom/SnowShader" {
    Properties {
        
        _Color("Main Tint Color", Color) = (1,1,1,1)
        _MainTex ("Base Texture", 2D) = "white" {}
        _Bump ("Bump", 2D) = "bump" {}
        
        _SideLevel ("Side Level", Range(0,1) ) = 0
        _SideColor ("Side Color", Color) = (1.0,1.0,1.0,1.0)
        _SideDirection ("Side Direction", Vector) = (1.5,0,0) 
        
        _TopLevel ("Top Level", Range(0,1) ) = 0
        _TopColor ("Top Color", Color) = (1.0,1.0,1.0,1.0)
        _TopDirection ("Top Direction", Vector) = (0,1.5,0)
        
        //_MossColor ("Top Color", Color) = (1.0,1.0,1.0,1.0)
        //_MossDirection ("Top Direction", Vector) = (0,1,0)
        
       [HideInInspector] _Depth ("Snow Depth", Range(0,0.2)) = 0.1
       [HideInInspector] _Intensity ("Wetness", Range(0, 0.5)) = 0.3
    }
    SubShader {
       Tags { "RenderType"="Opaque" }
       LOD 200
 
       CGPROGRAM
       #pragma surface surf Lambert vertex:vert
 		fixed4 _Color;
       sampler2D _MainTex;
       sampler2D _Bump;
       
       float _SideLevel;
       float4 _SideColor;
       float4 _SideDirection;
       
       float _TopLevel;
       float4 _TopColor;
       float4 _TopDirection;
       
       float _Depth;
       float _Intensity;
 
       struct Input {
           float2 uv_MainTex;
           float2 uv_Bump;
           float3 worldNormal;
           INTERNAL_DATA
       };
 
       void vert (inout appdata_full v) {
            //Convert the normal to world coortinates
            float4 side = mul(UNITY_MATRIX_IT_MV, _SideDirection);
            float4 top = mul(UNITY_MATRIX_IT_MV, _TopDirection);
            //float4 bottom = mul(UNITY_MATRIX_IT_MV, _BottomDirection);
 
            //if(dot(v.normal, sn.xyz) >= lerp(1,-1, (_Snow*2)/3)){
                //v.vertex.xyz += ((side.xyz + v.normal) * _Depth * _SideLevel) + ((top.xyz + v.normal) * _Depth * _TopLevel);
            //}
       }
 
       void surf (Input IN, inout SurfaceOutput o) {
       
            half4 c = tex2D (_MainTex, IN.uv_MainTex); //Normal color of a pixel
            
            o.Normal = UnpackNormal (tex2D (_Bump, IN.uv_Bump)); //get Normal from bump
            
            half difference = dot(WorldNormalVector(IN, o.Normal), _SideDirection.xyz) - lerp(1,-1,_SideLevel);
            half difference2 = dot(WorldNormalVector(IN, o.Normal), _TopDirection.xyz) - lerp(1,-1,_TopLevel);
            
            difference = saturate(difference / _Intensity);
            difference2 = saturate(difference2 / _Intensity);
            
            o.Albedo = ((difference*_SideColor.rgb + (1-difference) ) + (difference2*_TopColor.rgb + (1-difference2))) * c * _Color.rgb;
            o.Alpha = c.a;
       }
       ENDCG
    } 
    FallBack "Diffuse"
}