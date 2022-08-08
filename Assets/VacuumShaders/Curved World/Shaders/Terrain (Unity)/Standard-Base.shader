Shader "Hidden/VacuumShaders/Curved World/Terrain/Standard-Base" 
{
	Properties 
	{
		_MainTex ("Base (RGB) Smoothness (A)", 2D) = "white" {}
		_MetallicTex ("Metallic (R)", 2D) = "white" {}

		// used in fallback on old cards
		_Color ("Main Color", Color) = (1,1,1,1)
	}

	SubShader 
	{
		Tags 
		{
			"RenderType" = "CurvedWorld_Opaque"
			"Queue" = "Geometry-100"
		}
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows vertex:vert
		#pragma target 3.0
		// needs more than 8 texcoords
		#pragma exclude_renderers gles
		#include "UnityPBSLighting.cginc"


		#include "../cginc/CurvedWorld_Base.cginc"

		sampler2D _MainTex;
		sampler2D _MetallicTex;

		struct Input 
		{
			float2 uv_MainTex;
		};

		void vert (inout appdata_full v, out Input o) 
		{
			UNITY_INITIALIZE_OUTPUT(Input,o); 		

			//Terrain system can not use shaders using v.tangent
			float4 tangent = float4(cross(v.normal, float3(0,0,1)), -1);
			V_CW_TransformPointAndNormal(v.vertex, v.normal, tangent);			
		}  

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = 1;
			o.Smoothness = c.a;
			o.Metallic = tex2D (_MetallicTex, IN.uv_MainTex).r;
		}

		ENDCG
	}

	FallBack "Hidden/VacuumShaders/Curved World/Diffuse/Diffuse-BaseMap" 
}
