// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ToonShaderTest"
{
    Properties {
        _Color ("Lit Color", Color) = (1,1,1,1)
        _UnlitColor ("Shaded Color", Color) = (0.5,0.5,0.5,1)
        _SpecColor ("Specular Color", Color) = (1,1,1,1)
        _RimColor ("Fresnel Color", Color) = (1.0,1.0,1.0,1.0)
        _DiffuseThreshold ("Lighting Threshold", Range(-1.1,1)) = 0.1
        _Diffusion ("Diffusion", Range(0,0.99)) = 0.0  
        _Shininess ("Specular Polish", Range(0.1,10)) = 1
        _RimPower ("Fresnel Power", range (0.1,10.0)) = 3.0
        _MainTex ("Diffuse Map", 2D) = "white" {}
        _Bump ("Normal Map", 2D) = "white" {}
    }
    SubShader {
        Pass {
            Tags {"LightMode" = "ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma target 3.0
           
            //User defined variables
            uniform fixed4 _Color;
            uniform fixed4 _UnlitColor;
            uniform fixed _DiffuseThreshold;
            uniform fixed _Diffusion;
            uniform fixed4 _SpecColor;
            uniform fixed _Shininess;
            uniform half _SpecDiffusion;
            uniform sampler _MainTex;
            uniform float4 _MainTex_ST;
            uniform sampler2D _Bump;
            uniform sampler2D _Bump_ST;
            uniform float _RimPower;
            uniform float3 _RimColor;
           
            //Unity defined variables
            uniform half4 _LightColor0;
           
            //Structs
            struct vertexInput{
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
           
            };
            struct vertexOutput{
                half4 pos : POSITION;
                float2 dif : TEXCOORD0;
                fixed3 normalDir : TEXCOORD1;
                fixed4 lightDir : TEXCOORD2;
                fixed3 viewDir : TEXCOORD3;
                float3 posWorld : TEXCOORD4;
                float2 nor : TEXCOORD5;
               
            };
           
            //Vertex function
            vertexOutput vert(vertexInput v){
                vertexOutput o;
               
                //Normal direction
                o.normalDir = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);
               
                //Unity transform position
                o.pos = UnityObjectToClipPos(v.vertex);
               
                //World position
                half4 posWorld = mul(unity_ObjectToWorld, v.vertex);
               
                //Textures
                o.dif = v.texcoord;  
                o.nor = v.texcoord;
               
                //View direction
                o.viewDir = normalize(_WorldSpaceCameraPos.xyz - posWorld.xyz);
               
                //Light direction
                half3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - posWorld.xyz;
                o.lightDir = fixed4(
                    normalize(lerp(_WorldSpaceLightPos0.xyz, fragmentToLightSource, _WorldSpaceLightPos0.w)),
                    lerp(1.0,1.0/length(fragmentToLightSource), _WorldSpaceLightPos0.w)
                );
               
                return o;  
            }
           
            //Fragment function
            fixed4 frag(vertexOutput i) : COLOR {
               
                //Math
                fixed3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                fixed3 normalDirection = i.normalDir;
                fixed nDotL = saturate(dot(i.normalDir, i.lightDir.xyz));
               
                //Don't know why this needs to be here, but apparently it does
                float3 lightDirection;
               
                //Diffuse texture
                float4 colTex = _Color * tex2D(_MainTex, i.dif);
                float3 norTex = UnpackNormal(tex2D(_Bump, i.nor));
               
                //Final light direction
                lightDirection = normalize(_WorldSpaceLightPos0.xyz);
               
                //Shading
                fixed diffuseCutoff = saturate((max(_DiffuseThreshold, nDotL) - _DiffuseThreshold) * pow((2 - _Diffusion), 10));
                fixed specularCutoff = _LightColor0.xyz * _SpecColor.rgb * max(0.0, dot(norTex, lightDirection)) * pow(max(0.0, dot(reflect(-lightDirection, norTex), viewDirection)), _Shininess);
                fixed3 ambientShading = (1 - diffuseCutoff) * _UnlitColor.xyz;
                fixed3 diffuseShading = _Color.xyz * diffuseCutoff;
                fixed3 specularShading = _SpecColor.xyz * specularCutoff;
               
                //Rim lighting
                float rim = 1 - saturate(dot(normalize(i.viewDir), norTex));
                float3 rimLighting =  _RimColor * saturate(dot(norTex, norTex)) * pow(rim, _RimPower);
               
                //Final shading
                fixed3 finalShading = ambientShading + diffuseShading + specularShading + rimLighting;
               
                //Output
                return fixed4(colTex.rgb * finalShading, 1.0);
            }
            ENDCG
        }
    }   
}
