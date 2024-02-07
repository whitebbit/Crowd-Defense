#ifndef URP_UNLIT_GBUFFER_PASS_INCLUDED
#define URP_UNLIT_GBUFFER_PASS_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Unlit.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/UnityGBuffer.hlsl"
#if defined(LOD_FADE_CROSSFADE)
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
    // BEGIN GENERATED MESH ANIMATOR CODE
    #include "../MeshAnimator.hlsl"
    // END GENERATED MESH ANIMATOR CODE
#endif

struct Attributes
{
    float4 positionOS : POSITION;
    float2 uv : TEXCOORD0;
    float3 normalOS : NORMAL;

    UNITY_VERTEX_INPUT_INSTANCE_ID
    // BEGIN GENERATED MESH ANIMATOR CODE
    uint vertexId        : SV_VertexID;
    // END GENERATED MESH ANIMATOR CODE
};

struct Varyings
{
    float4 positionCS : SV_POSITION;
    float2 uv : TEXCOORD0;
    float3 normalWS : TEXCOORD1;

    UNITY_VERTEX_INPUT_INSTANCE_ID
    UNITY_VERTEX_OUTPUT_STEREO
};

void InitializeInputData(Varyings input, out InputData inputData)
{
    inputData = (InputData)0;

    inputData.normalWS = NormalizeNormalPerPixel(input.normalWS);

    inputData.positionWS = float3(0, 0, 0);
    inputData.viewDirectionWS = half3(0, 0, 1);
    inputData.shadowCoord = 0;
    inputData.fogCoord = 0;
    inputData.vertexLighting = half3(0, 0, 0);
    inputData.bakedGI = half3(0, 0, 0);
    inputData.normalizedScreenSpaceUV = 0;
    inputData.shadowMask = half4(1, 1, 1, 1);
}

Varyings UnlitPassVertex(Attributes input)
{
    Varyings output = (Varyings)0;

    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input, output);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

    // BEGIN GENERATED MESH ANIMATOR CODE
    float3 animatedPosition;
    float3 animatedNormal;	
    ApplyMeshAnimationValues_float(
        input.positionOS.xyz,
        input.normalOS.xyz,
        UNITY_ACCESS_INSTANCED_PROP(Props, _AnimTimeInfo), 
        _AnimTextures,
        UNITY_ACCESS_INSTANCED_PROP(Props, _AnimInfo),
        UNITY_ACCESS_INSTANCED_PROP(Props, _AnimScalar), 
        UNITY_ACCESS_INSTANCED_PROP(Props, _CrossfadeAnimInfo), 
        UNITY_ACCESS_INSTANCED_PROP(Props, _CrossfadeAnimTimeInfo), 
        UNITY_ACCESS_INSTANCED_PROP(Props, _CrossfadeAnimScalar), 
        UNITY_ACCESS_INSTANCED_PROP(Props, _CrossfadeData),  
        input.vertexId,
        sampler_AnimTextures,
        animatedPosition,
        animatedNormal);
    
    input.positionOS.xyz = animatedPosition;
    
    // END GENERATED MESH ANIMATOR CODE

    VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
    output.positionCS = vertexInput.positionCS;

    output.uv = TRANSFORM_TEX(input.uv, _BaseMap);

    VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS);
    output.normalWS = normalInput.normalWS;

    return output;
}

FragmentOutput UnlitPassFragment(Varyings input)
{
    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

    half2 uv = input.uv;
    half4 texColor = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap));
    half3 color = texColor.rgb * _BaseColor.rgb;
    half alpha = texColor.a * _BaseColor.a;

    alpha = AlphaDiscard(alpha, _Cutoff);
    color = AlphaModulate(color, alpha);

#ifdef LOD_FADE_CROSSFADE
    LODFadeCrossFade(input.positionCS);
#endif

    InputData inputData;
    InitializeInputData(input, inputData);

#ifdef _DBUFFER
    ApplyDecalToBaseColor(input.positionCS, color);
#endif

    SurfaceData surfaceData = (SurfaceData)0;
    surfaceData.albedo = color;
    surfaceData.alpha = alpha;

#if defined(_SCREEN_SPACE_OCCLUSION) // GBuffer never has transparents
    float2 normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);
    AmbientOcclusionFactor aoFactor = GetScreenSpaceAmbientOcclusion(normalizedScreenSpaceUV);
    surfaceData.occlusion = aoFactor.directAmbientOcclusion;
#else
    surfaceData.occlusion = 1;
#endif

    return SurfaceDataToGbuffer(surfaceData, inputData, float3(0,0,0), kLightingInvalid);
}

#endif