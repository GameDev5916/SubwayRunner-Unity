Shader "VacuumShaders/Curved World/FX/Glass/Simple" 
{
	Properties 
	{	
		[CurvedWorldGearMenu] V_CW_Label_Tag("", float) = 0		
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Default Visual Options", float) = 0


		//Albedo
		[CurvedWorldLargeLabel] V_CW_Label_Albedo("Albedo", float) = 0	
		_Color("  Tint Color", color) = (1, 1, 1, 1)		
		_MainTex ("  Tint Color (RGB)", 2D) = "white" {}
		[CurvedWorldUVScroll] _V_CW_MainTex_Scroll("    ", vector) = (0, 0, 0, 0)

		//Curved World
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Curved World Optionals", float) = 0
	}

	SubShader 
	{
		Tags { "Queue"="Transparent" 
			   "IgnoreProjector"="True" 
			   "RenderType"="CurvedWorld_Opaque" 
			   "CurvedWorldTag"="FX/Glass/Simple" 
			   "CurvedWorldNoneRemoveableKeywords"="" 
			   "CurvedWorldAvailableOptions"="" 
			 } 
		LOD 100
		Blend DstColor Zero
		

		//PassName "BASE" 
		Pass 
		{ 
			Name "BASE"  

			 
			CGPROGRAM
			#pragma vertex vert
   			#pragma fragment frag  			         
			#pragma multi_compile_fog	                          
			           


			#define V_CW_FOG
			
			#include "../cginc/CurvedWorld_Unlit.cginc" 


			ENDCG

		} //Pass

	} //SubShader

	CustomEditor "CurvedWorld_Material_Editor"

} //Shader
