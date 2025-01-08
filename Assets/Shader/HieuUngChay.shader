Shader "Custom/BlendTwoMaterialsURP"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _BurnTex ("Burn Texture", 2D) = "black" {}
        _TouchPoint ("Touch Point", Vector) = (0, 0, 0, 0)
        _BurnRadius ("Burn Radius", float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            sampler2D _BurnTex;
            float4 _TouchPoint;
            float _BurnRadius;
            float4 _MainTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float distance = length(i.uv - _TouchPoint.xy);
                if (distance < _BurnRadius)
                {
                    half4 burnColor = tex2D(_BurnTex, i.uv);
                    return burnColor;
                }
                else
                {
                    return tex2D(_MainTex, i.uv);
                }
            }
            ENDCG
        }
    }
}