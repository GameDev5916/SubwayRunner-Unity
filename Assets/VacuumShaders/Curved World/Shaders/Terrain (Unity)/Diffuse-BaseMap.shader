Shader "Hidden/VacuumShaders/Curved World/Diffuse/Diffuse-BaseMap" 
{
	Properties 
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader 
	{
		Tags { "RenderType"="CurvedWorld_Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert addshadow


		#include "../cginc/CurvedWorld_Base.cginc"

		sampler2D _MainTex;

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

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG		
	}

	Fallback "Hidden/VacuumShaders/Curved World/VertexLit/Diffuse"
}
