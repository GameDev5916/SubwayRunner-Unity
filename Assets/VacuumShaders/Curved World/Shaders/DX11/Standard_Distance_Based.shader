Shader "VacuumShaders/Curved World/DX11 (Tessellation)/Standard (Distance Based)" 
{
	Properties 
	{
		[CurvedWorldGearMenu] V_CW_Label_Tag("", float) = 0
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Default Visual Options", float) = 0
		  
		 
		//Albedo
		[CurvedWorldLargeLabel] V_CW_Label_Albedo("Albedo", float) = 0	
		_Color("  Color", color) = (1, 1, 1, 1)
		_MainTex ("  Map (RGB) Smoothness (A)", 2D) = "white" {}
		 
		//Bump 
		[CurvedWorldLargeLabel] V_CW_Label_Bump("Bump", float) = 0	
		_V_CW_NormalMapStrength("  Strength", float) = 1
		_V_CW_NormalMap_UV_Scale("  UV Scale", float) = 1
		[NoScaleOffset] _V_CW_NormalMap ("  Normalmap", 2D) = "bump" {}
		 
		_Glossiness ("Smoothness", Range(0,1)) = 0.5 
		_Metallic ("Metallic", Range(0,1)) = 0.0 

		//Tesselation 
		[CurvedWorldLargeLabel] V_CW_Label_Tesselation("Tesselation", float) = 0	
		_V_CW_Displace_Amount("  Displace Amount", float) = 1
		_V_CW_DisplaceTex_UVScale("  UV Scale", float) = 1
		[NoScaleOffset] _V_CW_DisplaceTex ("  Displace Texture (R)", 2D) = "gray" {}
		_V_CW_Tesselation ("  Tessellation", Range(1,32)) = 4
		_V_CW_Tesselation_Start("  Distance Start", float) = 10
		_V_CW_Tesselation_End("  Distance End", float) = 25		
		
		
		

		//Curved World
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Curved World Optionals", float) = 0
	}

	SubShader  
	{
		Tags { "RenderType"="CurvedWorld_Opaque"
			   "CurvedWorldTag"="DX11 (Tessellation)/Standard (Distance Based)" 
			   "CurvedWorldNoneRemoveableKeywords"=""  
			   "CurvedWorldAvailableOptions"="" 
			 }
		LOD 400     
		   
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard addshadow fullforwardshadows vertex:vertTess tessellate:tessFunc

		#pragma target 5.0
        #include "Tessellation.cginc"  


		
		#define V_CW_STANDARD
		#define V_CW_TESSELATION  
		#define V_CW_TESSELATION_DISTANCE 
		#define _NORMALMAP
		  
		#include "../cginc/CurvedWorld_Surface.cginc" 
		 
       
		ENDCG          
	}    

	FallBack "Hidden/VacuumShaders/Curved World/VertexLit/Diffuse" 
	CustomEditor "CurvedWorld_Material_Editor"
} 
