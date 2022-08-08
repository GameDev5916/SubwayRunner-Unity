// VacuumShaders 2015
// https://www.facebook.com/VacuumShaders

Shader "Hidden/VacuumShaders/Curved World/Terrain/T4M/Unlit/3Tex"
{
	Properties    
	{                       
		[CurvedWorldGearMenu] V_CW_Label_Tag("", float) = 0		
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Default Visual Options", float) = 0
		

		//Default Options		
		_Color("Tint Color", color) = (1, 1, 1, 1)  
		[HideInInspector] _MainTex ("Base (RGB)", 2D) = "white" {}


		//Blend Mode
		[CurvedWorldLargeLabel] V_CW_Label_Blend("Control", float) = 0	
		[CurvedWorldMobileTextureCount] V_CW_Label_MobileTextureCount("3", float) = 0
		[KeywordEnum(Texture, VertexColor)] V_CW_TERRAINBLEND ("  Blend By", Float) = 0
		[CanBeHidden] _V_CW_Control ("  Control (RGBA)", 2D) = "gray" {}

		//Layers
		[CurvedWorldLargeLabel] V_CW_Label_Layers("Layers", float) = 0		
		_V_CW_Splat1_uvScale("  Layer 1 UV Scale", float) = 1
		[CurvedWorldTexture2D16] _V_CW_Splat1 ("  Layer 1 (R)", 2D) = "black" {}
		_V_CW_Splat2_uvScale("  Layer 2 UV Scale", float) = 1
		[CurvedWorldTexture2D16] _V_CW_Splat2 ("  Layer 2 (G)", 2D) = "black" {}
		_V_CW_Splat3_uvScale("  Layer 3 UV Scale", float) = 1
		[CurvedWorldTexture2D16] _V_CW_Splat3 ("  Layer 3 (B)", 2D) = "black" {}		


		//Curved World
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Curved World Optionals", float) = 0
	

		[HideInInspector] _V_CW_IBL_Intensity("", float) = 1
		[HideInInspector] _V_CW_IBL_Contrast("", float) = 1 
		[HideInInspector] _V_CW_IBL_Cube("", cube ) = ""{}  

		[HideInInspector] _V_CW_IBL_Matcap ("", 2D) = "Gray" {}	

		[HideInInspector] _V_CW_ReflectColor("", color) = (1, 1, 1, 1)
		[HideInInspector] _V_CW_ReflectStrengthAlphaOffset("", Range(-1, 1)) = 0
		[HideInInspector] _V_CW_Cube("", Cube) = "_Skybox"{}	
		[HideInInspector] _V_CW_Fresnel_Bias("", Range(-1, 1)) = 0

		[HideInInspector] _V_CW_NormalMapStrength("", float) = 1
		[HideInInspector] _V_CW_NormalMap("", 2D) = "bump" {}
		[HideInInspector] _V_CW_NormalMap_UV_Scale ("", float) = 1
	}
	 
	
	SubShader 
	{
		Tags { "RenderType"="CurvedWorld_Opaque" 
		       "CurvedWorldTag"="Terrain/Mobile (T4M)/Unlit" 
			   "CurvedWorldNoneRemoveableKeywords"="V_CW_TERRAINBLEND_TEXTURE"
			   "CurvedWorldAvailableOptions"="V_CW_REFLECTIVE;V_CW_VERTEX_COLOR;V_CW_IBL;V_CW_FOG;" 
			 } 
		LOD 150		
		Fog{Mode Off}   


		Pass
	    {
			CGPROGRAM
			#pragma vertex vert 
	    	#pragma fragment frag


			#pragma shader_feature V_CW_REFLECTIVE_OFF V_CW_REFLECTIVE V_CW_REFLECTIVE_FRESNEL
			#pragma shader_feature V_CW_VERTEX_COLOR_OFF V_CW_VERTEX_COLOR 
			#pragma shader_feature V_CW_IBL_OFF V_CW_IBL_CUBE V_CW_IBL_MATCAP
			#pragma shader_feature V_CW_MATCAP_BLEND_MULTIPLY V_CW_MATCAP_BLEND_ADD

			#pragma shader_feature V_CW_FOG_OFF V_CW_FOG
			#ifdef V_CW_FOG
				#pragma multi_compile_fog
			#endif 

			#pragma shader_feature V_CW_TERRAINBLEND_TEXTURE V_CW_TERRAINBLEND_VERTEXCOLOR

			#define V_CW_MOBILE_TERRAIN
			#define V_CW_TERRAIN_2TEX
			#define V_CW_TERRAIN_3TEX 


			#include "../cginc/CurvedWorld_Unlit.cginc" 


			ENDCG

		} //Pass

	} //SubShader
	 
	
	CustomEditor "CurvedWorld_Material_Editor"
} //Shader
