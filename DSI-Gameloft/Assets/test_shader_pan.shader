// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.7176471,fgcg:0.4705882,fgcb:0.4196078,fgca:0.7450981,fgde:0.01,fgrn:38.8,fgrf:81.7,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32159,y:32661|emission-6-OUT,alpha-58-R,olcol-5-RGB;n:type:ShaderForge.SFN_Fresnel,id:2,x:32782,y:32808|EXP-32-OUT;n:type:ShaderForge.SFN_Color,id:5,x:32949,y:32554,ptlb:node_5,ptin:_node_5,glob:False,c1:1,c2:0.3979716,c3:0.2205882,c4:1;n:type:ShaderForge.SFN_Lerp,id:6,x:32468,y:32547|A-8-RGB,B-5-RGB,T-2-OUT;n:type:ShaderForge.SFN_Color,id:8,x:32766,y:32506,ptlb:node_5_copy,ptin:_node_5_copy,glob:False,c1:1,c2:0.9533468,c3:0.3235294,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:32,x:32958,y:32895,ptlb:node_32,ptin:_node_32,glob:False,v1:2;n:type:ShaderForge.SFN_Transform,id:46,x:33587,y:32621,tffrom:0,tfto:3|IN-47-OUT;n:type:ShaderForge.SFN_NormalVector,id:47,x:33677,y:32741,pt:False;n:type:ShaderForge.SFN_ComponentMask,id:48,x:33371,y:32689,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-46-XYZ;n:type:ShaderForge.SFN_Add,id:49,x:33191,y:32844|A-48-OUT,B-51-OUT;n:type:ShaderForge.SFN_Vector1,id:50,x:33036,y:32764,v1:2;n:type:ShaderForge.SFN_Vector1,id:51,x:33456,y:32859,v1:1;n:type:ShaderForge.SFN_Divide,id:52,x:32912,y:32697|A-49-OUT,B-50-OUT;n:type:ShaderForge.SFN_Tex2d,id:58,x:32540,y:32716,ptlb:node_58,ptin:_node_58,tex:7c4dc7f60cb47b64f834f9b37a1ab123,ntxv:0,isnm:False|UVIN-52-OUT;proporder:5-8-32-58;pass:END;sub:END;*/

Shader "Custom/test_shader_pan" {
    Properties {
        _node_5 ("node_5", Color) = (1,0.3979716,0.2205882,1)
        _node_5_copy ("node_5_copy", Color) = (1,0.9533468,0.3235294,1)
        _node_32 ("node_32", Float ) = 2
        _node_58 ("node_58", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _node_5;
            uniform float4 _node_5_copy;
            uniform float _node_32;
            uniform sampler2D _node_58; uniform float4 _node_58_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
////// Lighting:
////// Emissive:
                float node_2 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_32);
                float3 emissive = lerp(_node_5_copy.rgb,_node_5.rgb,node_2);
                float3 finalColor = emissive;
                float2 node_52 = ((mul( UNITY_MATRIX_V, float4(i.normalDir,0) ).xyz.rgb.rg+1.0)/2.0);
/// Final Color:
                return fixed4(finalColor,tex2D(_node_58,TRANSFORM_TEX(node_52, _node_58)).r);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
