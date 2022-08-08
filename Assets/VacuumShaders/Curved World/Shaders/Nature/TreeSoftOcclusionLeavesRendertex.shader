Shader "Hidden/VacuumShaders/Curved World/Nature/Tree Soft Occlusion Leaves Rendertex" 
{
	Properties 
	{
		[CurvedWorldGearMenu] V_CW_Label_Tag("", float) = 0
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Default Visual Options", float) = 0


		//Albedo
		[CurvedWorldLargeLabel] V_CW_Label_Albedo("Albedo", float) = 0	
		_Color ("  Color", Color) = (1,1,1,0)
		_MainTex ("  Map (RGB) Trans (A)", 2D) = "white" {}
		
		//Light
		[CurvedWorldLargeLabel] V_CW_Label_Light("Light", float) = 0
		_BaseLight ("  Base Light", Range(0, 1)) = 0.35
		_AO ("  Amb. Occlusion", Range(0, 10)) = 2.4
		_Occlusion ("  Dir Occlusion", Range(0, 20)) = 7.5
		
		//Cutoff
		[CurvedWorldLargeLabel] V_CW_Label_Cutoff("Cutout", float) = 0	
		_Cutoff ("  Alpha cutoff", Range(0,1)) = 0.5
		_HalfOverCutoff ("  0.5 / Alpha cutoff", Range(0,1)) = 1.0

		// These are here only to provide default values
		[HideInInspector] _TreeInstanceColor ("TreeInstanceColor", Vector) = (1,1,1,1)
		[HideInInspector] _TreeInstanceScale ("TreeInstanceScale", Vector) = (1,1,1,1)
		[HideInInspector] _SquashAmount ("Squash", Float) = 1



		//Curved World
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Curved World Optionals", float) = 0
	}
	
	SubShader 
	{

		Tags { "Queue" = "Transparent-99" 
		       "CurvedWorldTag"="Nature/Tree Soft Occlusion Leaves Rendertex" 
			   "CurvedWorldNoneRemoveableKeywords"=""  
			   "CurvedWorldAvailableOptions"="" 
			} 
		Cull Off
		
		Pass 
		{
			Lighting On
			ZWrite On

			CGPROGRAM
			#pragma vertex leaves
			#pragma fragment frag
			#define USE_CUSTOM_LIGHT_DIR 1
			


			#include "../cginc/CurvedWorld_UnityBuiltin2xTreeLibrary.cginc"
			
			sampler2D _MainTex;
			fixed _Cutoff;
			
			fixed4 frag(v2f input) : SV_Target
			{
				fixed4 col = tex2D( _MainTex, input.uv.xy);
				col.rgb *= input.color.rgb;
				clip(col.a - _Cutoff);
				col.a = 1;
				return col;
			}
			ENDCG
		}
	}
	
	Fallback Off

	CustomEditor "CurvedWorld_Material_Editor"
}
