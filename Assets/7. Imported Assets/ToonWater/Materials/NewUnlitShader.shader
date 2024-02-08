Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Tint ("Tint", Color) = (1,1,1,1)
        _UVWaveFrequency("Frequency", Float) = 10
        _UVWaveSpeed("Speed", Float) = 0.5
        _UVWaveAmplitude("Amplitude", Float) = 0.034
        _Transparency("Transparency", Float) = 0.65
        _Contrast("Contrast", Float) = 1.5
        _Brightness ("Brightness", Float) = 1.0
        _UVScale ("Scale", Float) = 0.2
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }

        ZWrite Off

        Blend One SrcAlpha
        
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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
                float2 sinAnim : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _UVWaveFrequency;
            float _UVWaveSpeed;
            float _UVWaveAmplitude;
            float _Transparency;
            float _Contrast;

            float4 _Tint;

            float _Brightness;

            float _UVScale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(float2(v.vertex.x * _UVScale, v.vertex.z * _UVScale), _MainTex);
                o.sinAnim = ((v.vertex.xy+v.vertex.yz) * _UVWaveFrequency) + (_Time.yy * _UVWaveSpeed);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half2 uvDistort = ((sin(0.9*i.sinAnim.xy) + sin(1.33*i.sinAnim.xy+3.14) + sin(2.4*i.sinAnim.xy+5.3))/3) * _UVWaveAmplitude;
                fixed4 col = tex2D(_MainTex, i.uv + uvDistort);
                col.rgb = pow(col.rgb * _Tint.rgb, _Contrast);
                col.rgb *= _Brightness;
                col.a *= _Transparency;
                return col;
            }
            ENDCG
        }
    }
}
