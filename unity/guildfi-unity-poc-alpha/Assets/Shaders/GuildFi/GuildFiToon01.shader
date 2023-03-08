Shader "GuildFi/Toon01" {
    Properties{
        [Header(Surface options)]
        [MainTexture] _MainTexture("Main Texture", 2D) = "white" {}
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        _ShadowColor("Shadow Color", Color) = (0, 0, 0, 1)
        _LightSmooth("Light Smooth", Float) = 0.1
    }
    SubShader{
        Tags{"RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline"}

        Pass {
            Name "ForwardLit"
            Tags{"LightMode" = "UniversalForward"}

            HLSLPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment
            #include "GuildFiToon01ForwardLitPass.hlsl"
            ENDHLSL
        }

        UsePass "Universal Render Pipeline/Lit/ShadowCaster"

        UsePass "Universal Render Pipeline/Lit/DepthOnly"

    }
}
