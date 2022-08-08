Shader "Hidden/VacuumShaders/Curved World/Nature/Tree Soft Occlusion Bark Rendertex" 
{
	Properties 
	{
		[CurvedWorldGearMenu] V_CW_Label_Tag("", float) = 0
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Default Visual Options", float) = 0


		//Albedo
		[CurvedWorldLargeLabel] V_CW_Label_Albedo("Albedo", float) = 0
		_Color ("  Color", Color) = (1,1,1,0)
		_MainTex ("  Map", 2D) = "white" {}
		
		//Light
		[CurvedWorldLargeLabel] V_CW_Label_Light("Light", float) = 0
		_BaseLight ("  Base Light", Range(0, 1)) = 0.35
		_AO ("  Amb. Occlusion", Range(0, 10)) = 2.4
		
		// These are here only to provide default values
		[HideInInspector] _TreeInstanceColor ("TreeInstanceColor", Vector) = (1,1,1,1)
		[HideInInspector] _TreeInstanceScale ("TreeInstanceScale", Vector) = (1,1,1,1)
		[HideInInspector] _SquashAmount ("Squash", Float) = 1



		//Curved World
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Curved World Optionals", float) = 0
	}
	
	SubShader 
	{
		Tags { "CurvedWorldTag"="Nature/Tree Soft Occlusion Bark Rendertex" 
			   "CurvedWorldNoneRemoveableKeywords"=""  
			   "CurvedWorldAvailableOptions"="" 
			} 

		Pass 
		{
			Lighting On
		
			CGPROGRAM
			#pragma vertex bark
			#pragma fragment frag 
			#define WRITE_ALPHA_1 1
			#define USE_CUSTOM_LIGHT_DIR 1
			


			#include "../cginc/CurvedWorld_UnityBuiltin2xTreeLibrary.cginc"

			sampler2D _MainTex;
			
			fixed4 frag(v2f input) : SV_Target
			{
				fixed4 col = input.color;
				col.rgb *= tex2D( _MainTex, input.uv.xy).rgb;
				UNITY_OPAQUE_ALPHA(col.a);
				return col;
			}
			ENDCG
		}
	}
	
	Fallback Off
	CustomEditor "CurvedWorld_Material_Editor"
}
