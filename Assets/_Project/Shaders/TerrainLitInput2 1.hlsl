#ifndef UNIVERSAL_TERRAIN_LIT_INPUT_INCLUDED
#define UNIVERSAL_TERRAIN_LIT_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"

CBUFFER_START(UnityPerMaterial)
    float4 _MainTex_ST;
    half4 _BaseColor;
    half _Cutoff;
CBUFFER_END

#define _Surface 0.0 // Terrain is always opaque

CBUFFER_START(_Terrain)
    half _NormalScale0, _NormalScale1, _NormalScale2, _NormalScale3;
    half _Metallic0, _Metallic1, _Metallic2, _Metallic3;
    half _Smoothness0, _Smoothness1, _Smoothness2, _Smoothness3;
    half4 _DiffuseRemapScale0, _DiffuseRemapScale1, _DiffuseRemapScale2, _DiffuseRemapScale3;
    half4 _MaskMapRemapOffset0, _MaskMapRemapOffset1, _MaskMapRemapOffset2, _MaskMapRemapOffset3;
    half4 _MaskMapRemapScale0, _MaskMapRemapScale1, _MaskMapRemapScale2, _MaskMapRemapScale3;

    float4 _Control_ST;
    float4 _Control_TexelSize;
    half _DiffuseHasAlpha0, _DiffuseHasAlpha1, _DiffuseHasAlpha2, _DiffuseHasAlpha3;
    half _LayerHasMask0, _LayerHasMask1, _LayerHasMask2, _LayerHasMask3;
    half4 _Splat0_ST, _Splat1_ST, _Splat2_ST, _Splat3_ST;
    half _HeightTransition;
    half _NumLayersCount;

    #ifdef UNITY_INSTANCING_ENABLED
    float4 _TerrainHeightmapRecipSize;   // float4(1.0f/width, 1.0f/height, 1.0f/(width-1), 1.0f/(height-1))
    float4 _TerrainHeightmapScale;       // float4(hmScale.x, hmScale.y / (float)(kMaxHeight), hmScale.z, 0.0f)
    #endif
    #ifdef SCENESELECTIONPASS
    int _ObjectId;
    int _PassValue;
    #endif
CBUFFER_END


TEXTURE2D(_Control);    SAMPLER(sampler_Control);
TEXTURE2D(_Splat0);     SAMPLER(sampler_Splat0);
TEXTURE2D(_Splat1);
TEXTURE2D(_Splat2);
TEXTURE2D(_Splat3);

#ifdef _NORMALMAP
TEXTURE2D(_Normal0);     SAMPLER(sampler_Normal0);
TEXTURE2D(_Normal1);
TEXTURE2D(_Normal2);
TEXTURE2D(_Normal3);
#endif

#ifdef _MASKMAP
TEXTURE2D(_Mask0);      SAMPLER(sampler_Mask0);
TEXTURE2D(_Mask1);
TEXTURE2D(_Mask2);
TEXTURE2D(_Mask3);
#endif

TEXTURE2D(_MainTex);       SAMPLER(sampler_MainTex);
TEXTURE2D(_SpecGlossMap);  SAMPLER(sampler_SpecGlossMap);
TEXTURE2D(_MetallicTex);   SAMPLER(sampler_MetallicTex);

#if defined(UNITY_INSTANCING_ENABLED) && defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL)
#define ENABLE_TERRAIN_PERPIXEL_NORMAL
#endif

#ifdef UNITY_INSTANCING_ENABLED
TEXTURE2D(_TerrainHeightmapTexture);
TEXTURE2D(_TerrainNormalmapTexture);
SAMPLER(sampler_TerrainNormalmapTexture);
#endif

UNITY_INSTANCING_BUFFER_START(Terrain)
UNITY_DEFINE_INSTANCED_PROP(float4, _TerrainPatchInstanceData)  // float4(xBase, yBase, skipScale, ~)
UNITY_INSTANCING_BUFFER_END(Terrain)

#ifdef _ALPHATEST_ON
TEXTURE2D(_TerrainHolesTexture);
SAMPLER(sampler_TerrainHolesTexture);

void ClipHoles(float2 uv)
{
    float hole = SAMPLE_TEXTURE2D(_TerrainHolesTexture, sampler_TerrainHolesTexture, uv).r;
    clip(hole == 0.0f ? -1 : 1);
}
#endif

half4 SampleMetallicSpecGloss(float2 uv, half albedoAlpha)
{
    half4 specGloss;
    specGloss = SAMPLE_TEXTURE2D(_MetallicTex, sampler_MetallicTex, uv);
    specGloss.a = albedoAlpha;
    return specGloss;
}

inline void InitializeStandardLitSurfaceData(float2 uv, out SurfaceData outSurfaceData)
{
    outSurfaceData = (SurfaceData)0;
    half4 albedoSmoothness = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
    outSurfaceData.alpha = 1;

    half4 specGloss = SampleMetallicSpecGloss(uv, albedoSmoothness.a);
    outSurfaceData.albedo = albedoSmoothness.rgb;

    outSurfaceData.metallic = specGloss.r;
    outSurfaceData.specular = half3(0.0h, 0.0h, 0.0h);

    outSurfaceData.smoothness = specGloss.a;
    outSurfaceData.normalTS = SampleNormal(uv, TEXTURE2D_ARGS(_BumpMap, sampler_BumpMap));
    outSurfaceData.occlusion = 1;
    outSurfaceData.emission = 0;
}


void TerrainInstancing(inout float4 positionOS, inout float3 normal, inout float2 uv)
{
#ifdef UNITY_INSTANCING_ENABLED
    float2 patchVertex = positionOS.xy;
    float4 instanceData = UNITY_ACCESS_INSTANCED_PROP(Terrain, _TerrainPatchInstanceData);

    float2 sampleCoords = (patchVertex.xy + instanceData.xy) * instanceData.z; // (xy + float2(xBase,yBase)) * skipScale
    float height = UnpackHeightmap(_TerrainHeightmapTexture.Load(int3(sampleCoords, 0)));

    positionOS.xz = sampleCoords * _TerrainHeightmapScale.xz;
    positionOS.y = height * _TerrainHeightmapScale.y;

#ifdef ENABLE_TERRAIN_PERPIXEL_NORMAL
    normal = float3(0, 1, 0);
#else
    normal = _TerrainNormalmapTexture.Load(int3(sampleCoords, 0)).rgb * 2 - 1;
#endif
    uv = sampleCoords * _TerrainHeightmapRecipSize.zw;
#endif
}

void TerrainInstancing(inout float4 positionOS, inout float3 normal)
{
    float2 uv = { 0, 0 };
    TerrainInstancing(positionOS, normal, uv);
}
#endif

//
//
//[PostProcess(typeof(SobelOutlineRenderer), PostProcessEvent.BeforeStack, "SobelOutline")]
//public class SobelOutline : PostProcessEffectSettings
//{
//    public FloatParameter thickness = new FloatParameter{ value = 1.0f };
//    public FloatParameter depthMultiplier = new FloatParameter{ value = 1.0f };
//    public FloatParameter depthBias = new FloatParameter{ value = 1.0f };
//    public FloatParameter normalMultiplier = new FloatParameter{ value = 1.0f };
//    public FloatParameter normalBias = new FloatParameter{ value = 10.0f };
//    public ColorParameter color = new ColorParameter{ value = Color.black };
//}
//
//public sealed class SobelOutlineRenderer : PostProcessEffectRenderer<SobelOutline>
//{
//    public override void Render(PostProcessRenderContext context)
//    {
//        var sheet = context.propertySheets.Get(Shader.Find("PostProcessing/SobelOutline"));
//
//        sheet.properties.SetFloat("_OutlineThickness", settings.thickness);
//        sheet.properties.SetFloat("_OutlineDepthMultiplier", settings.depthMultiplier);
//        sheet.properties.SetFloat("_OutlineDepthBias", settings.depthBias);
//        sheet.properties.SetFloat("_OutlineNormalMultiplier", settings.normalMultiplier);
//        sheet.properties.SetFloat("_OutlineNormalBias", settings.normalBias);
//        sheet.properties.SetColor("_OutlineColor", settings.color);
//
//        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
//    }
//}


//
//
//TEXTURE2D(_CameraColorTexture);
//SAMPLER(sampler_CameraColorTexture);
//float4 _CameraColorTexture_TexelSize;
//
//TEXTURE2D(_CameraDepthTexture);
//SAMPLER(sampler_CameraDepthTexture);
//
//TEXTURE2D(_CameraDepthNormalsTexture);
//SAMPLER(sampler_CameraDepthNormalsTexture);
//
//float3 DecodeNormal(float4 enc)
//{
//    float kScale = 1.7777;
//    float3 nn = enc.xyz * float3(2 * kScale, 2 * kScale, 0) + float3(-kScale, -kScale, 1);
//    float g = 2.0 / dot(nn.xyz, nn.xyz);
//    float3 n;
//    n.xy = g * nn.xy;
//    n.z = g - 1;
//    return n;
//}
//
//void Outline_float(float2 UV, float OutlineThickness, float DepthSensitivity, float NormalsSensitivity, float ColorSensitivity, float4 OutlineColor, out float4 Out)
//{
//    float halfScaleFloor = floor(OutlineThickness * 0.5);
//    float halfScaleCeil = ceil(OutlineThickness * 0.5);
//    float2 Texel = (1.0) / float2(_CameraColorTexture_TexelSize.z, _CameraColorTexture_TexelSize.w);
//
//    float2 uvSamples[4];
//    float depthSamples[4];
//    float3 normalSamples[4], colorSamples[4];
//
//    uvSamples[0] = UV - float2(Texel.x, Texel.y) * halfScaleFloor;
//    uvSamples[1] = UV + float2(Texel.x, Texel.y) * halfScaleCeil;
//    uvSamples[2] = UV + float2(Texel.x * halfScaleCeil, -Texel.y * halfScaleFloor);
//    uvSamples[3] = UV + float2(-Texel.x * halfScaleFloor, Texel.y * halfScaleCeil);
//
//    for (int i = 0; i < 4; i++)
//    {
//        depthSamples[i] = SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, uvSamples[i]).r;
//        normalSamples[i] = DecodeNormal(SAMPLE_TEXTURE2D(_CameraDepthNormalsTexture, sampler_CameraDepthNormalsTexture, uvSamples[i]));
//        colorSamples[i] = SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, uvSamples[i]);
//    }
//
//    // Depth
//    float depthFiniteDifference0 = depthSamples[1] - depthSamples[0];
//    float depthFiniteDifference1 = depthSamples[3] - depthSamples[2];
//    float edgeDepth = sqrt(pow(depthFiniteDifference0, 2) + pow(depthFiniteDifference1, 2)) * 100;
//    float depthThreshold = (1 / DepthSensitivity) * depthSamples[0];
//    edgeDepth = edgeDepth > depthThreshold ? 1 : 0;
//
//    // Normals
//    float3 normalFiniteDifference0 = normalSamples[1] - normalSamples[0];
//    float3 normalFiniteDifference1 = normalSamples[3] - normalSamples[2];
//    float edgeNormal = sqrt(dot(normalFiniteDifference0, normalFiniteDifference0) + dot(normalFiniteDifference1, normalFiniteDifference1));
//    edgeNormal = edgeNormal > (1 / NormalsSensitivity) ? 1 : 0;
//
//    // Color
//    float3 colorFiniteDifference0 = colorSamples[1] - colorSamples[0];
//    float3 colorFiniteDifference1 = colorSamples[3] - colorSamples[2];
//    float edgeColor = sqrt(dot(colorFiniteDifference0, colorFiniteDifference0) + dot(colorFiniteDifference1, colorFiniteDifference1));
//    edgeColor = edgeColor > (1 / ColorSensitivity) ? 1 : 0;
//
//    float edge = max(edgeDepth, max(edgeNormal, edgeColor));
//
//    float4 original = SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, uvSamples[0]);
//    Out = ((1 - edge) * original) + (edge * lerp(original, OutlineColor, OutlineColor.a));
//}