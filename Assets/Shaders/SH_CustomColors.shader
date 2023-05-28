Shader "Custom/M_CustomColors"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Red  ("Red Remap", Color) = (1,1,1,1)
        _Green ("Green Remap", Color) = (.5,.5,.5,.5)
        _Blue  ("Blue Remap", Color) = (0,0,0,0)

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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _Red;
            fixed4 _Green;
            fixed4 _Blue;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 tex = tex2D(_MainTex, i.uv);
                fixed4 col = lerp(0, _Red, tex.r) + lerp(0, _Green, tex.g) + lerp(0, _Blue, tex.b);
                return fixed4(col.rgb, tex.a);
            }
            ENDCG
        }
    }
}
