Shader "Custom/Fisheye"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Fov ("Field of View", Range(180, 360)) = 180
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _Fov;

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // 視野角に基づいてUVを歪ませる
                float2 uv = i.uv * 2.0 - 1.0;
                float r = length(uv);
                float theta = atan2(uv.y, uv.x);

                float fovRatio = _Fov / 360.0;
                float distortedR = pow(r, fovRatio); // 視野角による歪み調整

                float2 distortedUV = float2(cos(theta), sin(theta)) * distortedR;
                distortedUV = (distortedUV + 1.0) * 0.5;

                // テクスチャサンプリング
                float4 color = tex2D(_MainTex, distortedUV);
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
