Shader "Hidden/TerrainEngine/Details/Vertexlit" 
{
	Properties 
	{
		_MainTex ("Main Texture", 2D) = "white" {  }
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
			fixed4 color : COLOR;
		};

		void vert (inout appdata_full v, out Input o) 
		{
			UNITY_INITIALIZE_OUTPUT(Input,o); 

			V_CW_TransformPoint(v.vertex);	
		}  

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;			 
		}

		ENDCG
	}

	Fallback "Hidden/VacuumShaders/Curved World/VertexLit/Diffuse"
}
