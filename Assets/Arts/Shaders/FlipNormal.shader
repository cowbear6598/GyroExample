Shader "Custom/360VideoShaderURP"
{
    Properties
    {
        _MainTex ("360 Video Texture", 2D) = "white" {}
        [Toggle] _FlipHorizontal("Flip Horizontal", Float) = 0
        [Toggle] _FlipVertical("Flip Vertical", Float) = 0
    }

    SubShader
    {
        Tags {"RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" "Queue"="Geometry"}
        LOD 100

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        ENDHLSL

        Pass
        {
            Name "360VideoPass"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 viewDirWS : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float _FlipHorizontal;
            float _FlipVertical;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.viewDirWS = normalize(TransformObjectToWorld(IN.positionOS.xyz));
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float3 dir = normalize(IN.viewDirWS);

                // 將方向向量轉換為球面坐標
                float2 uv;
                uv.x = 0.5 + atan2(dir.z, dir.x) / (2.0 * PI);
                uv.y = 0.5 - asin(dir.y) / PI;

                // 應用翻轉
                uv.x = _FlipHorizontal ? 1 - uv.x : uv.x;
                uv.y = _FlipVertical ? 1 - uv.y : uv.y;

                // 採樣並輸出顏色
                half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                return color;
            }
            ENDHLSL
        }
    }
}