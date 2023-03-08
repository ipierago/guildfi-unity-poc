#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"

TEXTURE2D(_MainTexture);
SAMPLER(sampler_MainTexture);

float4 _MainTexture_ST;
float4 _BaseColor;
float4 _ShadowColor;
float _LightSmooth;

struct Attributes
{
  float3 positionOS : POSITION;
  float3 normalOS : NORMAL;
  float2 uv : TEXCOORD0;
};

struct Interpolators
{
  float4 positionCS : SV_POSITION;
  float2 uv : TEXCOORD0;
  float3 normalWS : TEXCOORD1;
};

Interpolators Vertex(Attributes input)
{
  Interpolators output;

  VertexPositionInputs posnInputs = GetVertexPositionInputs(input.positionOS);
  output.positionCS = posnInputs.positionCS;

  VertexNormalInputs normInputs = GetVertexNormalInputs(input.normalOS);
  output.normalWS = normInputs.normalWS;

  output.uv = TRANSFORM_TEX(input.uv, _MainTexture);

  return output;
}

float4 Fragment(Interpolators input) : SV_TARGET
{
  float3 normalWS = normalize(input.normalWS);

  Light mainLight = GetMainLight();
  float diff = dot(normalWS, mainLight.direction);
  diff = clamp(diff, 0, 1);
  diff = smoothstep(_LightSmooth, 1, diff);
  half3 diffColor = lerp(_ShadowColor, _BaseColor, diff);

  float4 mainTextureTexel = SAMPLE_TEXTURE2D(_MainTexture, sampler_MainTexture, input.uv);
  diffColor *= mainTextureTexel.rgb;

  return float4(diffColor, 1);
}
