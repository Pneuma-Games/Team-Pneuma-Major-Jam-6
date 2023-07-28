Shader "PostProcessing/SobelOutline"
{
    HLSLINCLUDE
#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
        float4 FragMain(VaryingsDefault i) : SV_Target
    {
        return float4(0.0, 1.0, 0.0, 1.0);
    }
        ENDHLSL

        SubShader
    {
        Cull Off ZWrite Off ZTest Always

            Pass
        {
            HLSLPROGRAM
                #pragma vertex VertDefault
                #pragma fragment FragMain
            ENDHLSL
        }
    }
}