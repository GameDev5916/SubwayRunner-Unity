Shader "Hidden/TerrainEngine/Details/WavingDoublePass" 
{
	Properties 
	{
		_WavingTint ("Fade Color", Color) = (.7,.6,.5, 0)
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
		_WaveAndDistance ("Wave and distance", Vector) = (12, 3.6, 1, 1)
		_Cutoff ("Cutoff", float) = 0.5
	}

	SubShader 
	{
		Tags { "Queue" = "Geometry+200"
			   "IgnoreProjector"="True"
			   "RenderType"="CurvedWorld_Grass"
			   "DisableBatching"="True"
		     }
		Cull Off
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert addshadow
		

		#include "../cginc/CurvedWorld_Base.cginc"
		#include "TerrainEngine.cginc"

		sampler2D _MainTex; 
		fixed _Cutoff;

		struct Input 
		{
			float2 uv_MainTex;
			fixed4 color : COLOR;
		};

		void vert (inout appdata_full v, out Input o) 
		{
			UNITY_INITIALIZE_OUTPUT(Input,o); 

			V_CW_TransformPoint(v.vertex);

			// MeshGrass v.color.a: 1 on top vertices, 0 on bottom vertices
			// _WaveAndDistance.z == 0 for MeshLit
			v.color = TerrainWaveGrass (v.vertex, v.color.a * _WaveAndDistance.z, v.color);
		} 

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			clip (o.Alpha - _Cutoff);
			o.Alpha *= IN.color.a;
		}
		ENDCG
	}
	
			
	Fallback "Hidden/VacuumShaders/Curved World/VertexLit/Diffuse"
}
