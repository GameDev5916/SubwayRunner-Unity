Shader "Hidden/TerrainEngine/Details/BillboardWavingDoublePass" 
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
			   "RenderType"="CurvedWorld_GrassBillboard"
			   "DisableBatching"="True"
		     }
		Cull Off
		LOD 200
				
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert addshadow
		#include "UnityCG.cginc"
		#include "TerrainEngine.cginc"


		#include "../cginc/CurvedWorld_Base.cginc"

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


			TerrainBillboardGrass (v.vertex, v.tangent.xy);
			// wave amount defined by the grass height
			float waveAmount = v.tangent.y;
			v.color = TerrainWaveGrass (v.vertex, waveAmount, v.color);
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

	Fallback Off
} 
   
