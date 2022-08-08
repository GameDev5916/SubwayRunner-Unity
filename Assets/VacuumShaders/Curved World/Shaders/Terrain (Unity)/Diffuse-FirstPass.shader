Shader "VacuumShaders/Curved World/Terrain/Diffuse" 
{
	Properties 
	{
		[HideInInspector] _Control ("Control (RGBA)", 2D) = "red" {}
		[HideInInspector] _Splat3 ("Layer 3 (A)", 2D) = "white" {}
		[HideInInspector] _Splat2 ("Layer 2 (B)", 2D) = "white" {}
		[HideInInspector] _Splat1 ("Layer 1 (G)", 2D) = "white" {}
		[HideInInspector] _Splat0 ("Layer 0 (R)", 2D) = "white" {}
		[HideInInspector] _Normal3 ("Normal 3 (A)", 2D) = "bump" {}
		[HideInInspector] _Normal2 ("Normal 2 (B)", 2D) = "bump" {}
		[HideInInspector] _Normal1 ("Normal 1 (G)", 2D) = "bump" {}
		[HideInInspector] _Normal0 ("Normal 0 (R)", 2D) = "bump" {} 
		// used in fallback on old cards & base map
		[HideInInspector] _MainTex ("BaseMap (RGB)", 2D) = "white" {}
		[HideInInspector] _Color ("Main Color", Color) = (1,1,1,1) 
	}
	 
	CGINCLUDE
		#pragma surface surf Lambert vertex:SplatmapVert finalcolor:myfinal exclude_path:prepass exclude_path:deferred
		#pragma multi_compile_fog
	

		#include "../cginc/CurvedWorld_TerrainSplatmapCommon.cginc"

		void surf(Input IN, inout SurfaceOutput o)
		{
			half4 splat_control;
			half weight;
			fixed4 mixedDiffuse;
			SplatmapMix(IN, splat_control, weight, mixedDiffuse, o.Normal);
			o.Albedo = mixedDiffuse.rgb;
			o.Alpha = weight;
		}

		void myfinal(Input IN, SurfaceOutput o, inout fixed4 color)
		{
			SplatmapApplyWeight(color, o.Alpha);
			SplatmapApplyFog(color, IN);
		}

	ENDCG

	Category 
	{
		Tags { "SplatCount" = "4"
			   "Queue" = "Geometry-99"
			   "RenderType" = "CurvedWorld_Opaque"
		     }
		// TODO: Seems like "#pragma target 3.0 _TERRAIN_NORMAL_MAP" can't fallback correctly on less capable devices?
		// Use two sub-shaders to simulate different features for different targets and still fallback correctly.
		SubShader 
		{  
			// for sm3.0+ targets
			CGPROGRAM
				#pragma target 3.0
				#pragma multi_compile __ _TERRAIN_NORMAL_MAP
			ENDCG
		}
		SubShader 
		{   
			// for sm2.0 targets   
			CGPROGRAM
			ENDCG
		}
	} 
	 
	Dependency "AddPassShader" = "Hidden/VacuumShaders/Curved World/Terrain/Diffuse-AddPass" 
	Dependency "BaseMapShader" = "Hidden/VacuumShaders/Curved World/Diffuse/Diffuse-BaseMap"
	Dependency "Details0"      = "Hidden/TerrainEngine/Details/Vertexlit"
	Dependency "Details1"      = "Hidden/TerrainEngine/Details/WavingDoublePass"
	Dependency "Details2"      = "Hidden/TerrainEngine/Details/BillboardWavingDoublePass"
	Dependency "Tree0"         = "Hidden/TerrainEngine/BillboardTree"

	Fallback "Hidden/VacuumShaders/Curved World/Diffuse/Diffuse-BaseMap"
} 
