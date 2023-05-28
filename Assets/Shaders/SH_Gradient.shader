Shader "Custom/Gradient"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorA  ("Color A", Color) = (1,1,1,1)
        _ColorB ("Color B", Color) = (.5,.5,.5,.5)

    }
    SubShader
    {
		Tags{ 
			"RenderType"="Transparent" 
			"Queue"="Transparent"
		}

        Blend SrcAlpha OneMinusSrcAlpha
		ZWrite off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _ColorA;
            fixed4 _ColorB;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 tex = tex2D(_MainTex, i.uv);
                fixed4 col = lerp(_ColorB, _ColorA, i.uv.y);
                // return col;
                return fixed4(col.rgb, tex.a);
            }
            ENDCG
        }
    }
}
