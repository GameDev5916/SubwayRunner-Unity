// VacuumShaders 2015
// https://www.facebook.com/VacuumShaders

Shader "Hidden/VacuumShaders/Curved World/Outline/One Directional Light/Opaque/Decal"
{
	Properties  
	{  
		[CurvedWorldGearMenu] V_CW_Label_Tag("", float) = 0
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Default Visual Options", float) = 0

		 
		//Modes  
		[CurvedWorldLargeLabel] V_CW_Label_Modes("Modes", float) = 0	
		[CurvedWorldRenderingMode] V_CW_Rendering_Mode("  Rendering", float) = 0	
		[CurvedWorldTextureMixMode] V_CW_Texture_Mix_Mode("  Texture Mix", float) = 0	

		//Cartoon
		[CurvedWorldLargeLabel] V_CW_Label_Cartoon("Outline", float) = 0	
		_V_CW_OutlineColor ("  Color", Color) = (0,0,0,1)
		_V_CW_OutlineWidth ("  Width", Float) = 1

		//Albedo
		[CurvedWorldLargeLabel] V_CW_Label_Albedo("Albedo", float) = 0	
		_Color("  Color", color) = (1, 1, 1, 1)
		_MainTex ("  Map (RGB) RefStr & Gloss (A)", 2D) = "white" {}
		[CurvedWorldUVScroll] _V_CW_MainTex_Scroll("    ", vector) = (0, 0, 0, 0)
		_V_CW_SecondaryTex ("  Decal (RGB) Blend (A)", 2D) = "black" {}
		[CurvedWorldUVScroll] _V_CW_SecondaryTex_Scroll("    ", vector) = (0, 0, 0, 0)
		 
		  

		//Curved World
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Curved World Optionals", float) = 0

		[HideInInspector] _V_CW_Rim_Color("", color) = (1, 1, 1, 1)
		[HideInInspector] _V_CW_Rim_Bias("", Range(-1, 1)) = 0.2
		[HideInInspector] _V_CW_Rim_Power("", Range(0.5, 8.0)) = 3
		
		[HideInInspector] _EmissionMap("", 2D) = "white"{}
		[HideInInspector] _EmissionColor("", color) = (1, 1, 1, 1)	

		[HideInInspector] _V_CW_IBL_Intensity("", float) = 1
		[HideInInspector] _V_CW_IBL_Contrast("", float) = 1 
		[HideInInspector] _V_CW_IBL_Cube("", cube ) = ""{}  

		[HideInInspector] _V_CW_ReflectColor("", color) = (1, 1, 1, 1)
		[HideInInspector] _V_CW_ReflectStrengthAlphaOffset("", Range(-1, 1)) = 0
		[HideInInspector] _V_CW_Cube("", Cube) = "_Skybox"{}	
		[HideInInspector] _V_CW_Fresnel_Bias("", Range(-1, 1)) = 0

		[HideInInspector] _V_CW_Specular_Intensity("", Range(0, 5)) = 1		
		[HideInInspector] _V_CW_SpecularOffset("", Range(-0.25, 0.25)) = 0
		[HideInInspector] _V_CW_Specular_Lookup("", 2D) = "black"{}
		
		[HideInInspector] _V_CW_NormalMapStrength("", float) = 1
		[HideInInspector] _V_CW_NormalMap("", 2D) = "bump" {}
		[HideInInspector] _V_CW_NormalMap_UV_Scale ("", float) = 1

		[HideInInspector] _V_CW_SecondaryNormalMap("", 2D) = ""{}
		[HideInInspector] _V_CW_SecondaryNormalMap_UV_Scale("", float) = 1

		[HideInInspector] _V_CW_LightRampTex("", 2D) = "grey"{}
	}


	SubShader 
	{
		Tags { "RenderType"="CurvedWorld_Opaque" 
		       "CurvedWorldTag"="Outline/One Directional Light/Opaque/Decal" 
			   "CurvedWorldNoneRemoveableKeywords"="" 
			   "CurvedWorldAvailableOptions"="V_CW_USE_LIGHT_RAMP_TEXTURE;V_CW_OUTLINE;V_CW_REFLECTIVE;V_CW_VERTEX_COLOR;_EMISSION;V_CW_RIM;V_CW_FOG;_NORMALMAP;V_CW_SPECULAR_LOOKUP;" 
			 } 
		LOD 200		
		     

		//Base 
		UsePass "Hidden/VacuumShaders/Curved World/One Directional Light/Opaque/Decal/FORWARD"

		//Outline
		UsePass "Hidden/VacuumShaders/Curved World/Outline/OUTLINE"		
		  		
	} //SubShader


	Fallback "Hidden/VacuumShaders/Curved World/VertexLit/Diffuse" 
	CustomEditor "CurvedWorld_Material_Editor"
} //Shader
