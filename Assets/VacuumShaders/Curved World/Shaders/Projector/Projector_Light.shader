Shader "VacuumShaders/Curved World/Projector/Light" 
{ 
	Properties 
	{
		[CurvedWorldGearMenu] V_CW_TAG("", float) = 0
		[CurvedWorldLabel] V_CW_DEFAULTOPTIONS("Default Options", float) = 0


		//Default Options
		_Color ("Main Color", Color) = (1,1,1,1)
		[CurvedWorldClampTextureDrawer] _ShadowTex("Cookie", 2D) = ""{}
		[CurvedWorldClampTextureDrawer] _FalloffTex ("FallOff", 2D) = "" {}		


		//CurvedWorld Options
		[CurvedWorldLabel] V_CW_CW_OPTIONS("Curved World Options", float) = 0
		
		[HideInInspector] _V_CW_Y_Bend_Size("", float) = 0
		[HideInInspector] _V_CW_X_Bend_Size("", float) = 0
		[HideInInspector] _V_CW_Z_Bend_Size("", float) = 0
		[HideInInspector] _V_CW_X_Bend_Bias("", float) = 0
		[HideInInspector] _V_CW_Y_Bend_Bias("", float) = 0
		[HideInInspector] _V_CW_Z_Bend_Bias("", float) = 0 

		[HideInInspector] _V_CW_Rim_Color("", color) = (1, 1, 1, 1)
		[HideInInspector] _V_CW_Rim_Bias("", Range(-1, 1)) = 0.2
		   
		[HideInInspector] _V_CW_Fog_Color("", color) = (1, 1, 1, 1)
		[HideInInspector] _V_CW_Fog_Density("", Range(0.0, 1.0)) = 1 
		[HideInInspector] _V_CW_Fog_Start("", float) = 0
		[HideInInspector] _V_CW_Fog_End("", float) = 100  
		
		[HideInInspector] _V_CW_IBL_Intensity("", float) = 1
		[HideInInspector] _V_CW_IBL_Contrast("", float) = 1 
		[HideInInspector] _V_CW_IBL_Cube("", cube ) = ""{}  

		[HideInInspector] _V_CW_Emission_Color("", color) = (1, 1, 1, 1)
		[HideInInspector] _V_CW_Emission_Strength("", float) = 1
	}
	 

	Subshader 
	{
		Tags { "Queue"="Transparent"
			   "CurvedWorldTag"="Projector/Light" 
			   "CurvedWorldNoneRemoveableKeywords"=""
			   "CurvedWorldAvailableOptions"=""
			 }

		Pass 
		{
			ZWrite Off
			ColorMask RGB 
			Blend DstColor One
			Offset -1, -1
	 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			 

			#define V_CW_PROJECTOR_LIGHT

			#include "../cginc/CurvedWorld_Projector.cginc" 
			 			

			ENDCG

		} //Pass

	} //SubShader


	CustomEditor "CurvedWorld_Material_Editor"
} //Shader
