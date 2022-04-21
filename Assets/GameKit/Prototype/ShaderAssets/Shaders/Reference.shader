Shader "Reference/Reference"
{
    // Doc: https://docs.unity3d.com/Manual/SL-Properties.html
    Properties
    {
        _Int ("Int Name", Int) = 2
        _Float ("Float Name", Float) = 1.5
        _Range ("_Range Name", Range(0.5,1.0)) = 0.7
        _Color ("Color Name", Color) = (1,1,1,1)
        _Vector ("Vector Name", Vector) = (1,2,3,4)
        _Texture2D ("Texturen2D Name", 2D) = "white" {}
        _Texture3D ("Texturen3D Name", 3D) = "black" {}
        _TextureCube ("TexturenCube", Cube) = "" {}
        _CubeMapArray ("_CubeMapArray", CubeArray) = "" {}
        _Texture2DArray ("_Texture2DArray", 2DArray) = "" {}

        /*
            Attributes
            [Gamma], [HDR], [HideInInspector], [MainTexture], [MainColor], [NoScaleOffset], [Normal],[PerRendererData]
        */
    }

    SubShader
    {
        // Doc: https://docs.unity3d.com/Manual/SL-SubShaderTags.html
        Tags
        {
            // "RenderPipline" = "UniversalRenderPipline" // "HighDefinitionRenderPipline"
            "Queue" = "Transparent" // Background, Geometry, AlphaTest, Transparent, Overlay, [offset]integer
            "RenderType" = "Transparent" // Reference: https://docs.unity3d.com/Manual/SL-ShaderReplacement.html
            "PreviewType" = "Plane" // "Plane", "Sphere", "Skybox"
            // "ForceNoShadowCasting" = True
            // "DisableBatching" = True // False, LODFading
            // "IgnoreProjector" = True 
            // "CanUseSpriteAtlas" = True
        }
        // Command
        // Doc: https://docs.unity3d.com/Manual/shader-shaderlab-commands.html
        Blend One OneMinusSrcAlpha

        /*
            AlphaToMask: sets the alpha-to-coverage mode.
            Blend: enables and configures alpha blending.
            BlendOp: sets the operation used by the Blend command.
            ColorMask: sets the color channel writing mask.
            Conservative: enables and disables conservative rasterization.
            Cull: sets the polygon culling mode.
            Offset: sets the polygon depth offset.
            Stencil: configures the stencil test, and what to write to the stencil buffer.
            ZClip: sets the depth clip mode.
            ZTest: sets the depth testing mode.
            ZWrite: sets the depth buffer writing mode.
        */
        Pass
        {
            Name "MyPass"
            Tags
            {
                "LightMode" = "ForwardBase" // Doc: https://docs.unity3d.com/Manual/shader-predefined-pass-tags-built-in.html
                "RequireOptions" = "SoftVegetation"
            }
            CGPROGRAM
            // CG Syntax
            #pragma vertFunc
            #pragma fragmentFunc
            #include "UnityCG.cginc"
            
            struct appdata
            {

            }

            struct v2f
            {

            }

            v2f vertFunc(appdata vert)
            {

            }

            fixed4 fragmentFunc(v2f)
            {

            }

            ENDCG
        }
    }
}