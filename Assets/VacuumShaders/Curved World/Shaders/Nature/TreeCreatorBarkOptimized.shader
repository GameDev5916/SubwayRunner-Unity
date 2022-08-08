Shader "VacuumShaders/Curved World/Nature/Tree Creator Bark Optimized" 
{
	Properties 
	{
		[CurvedWorldGearMenu] V_CW_Label_Tag("", float) = 0
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Default Visual Options", float) = 0


		//Albedo
		[CurvedWorldLargeLabel] V_CW_Label_Albedo("Albedo", float) = 0	
		_Color("  Color", color) = (1, 1, 1, 1)		
		_MainTex ("  Map", 2D) = "white" {}

		

		//Bump
		[CurvedWorldLargeLabel] V_CW_Label_Bump("Specular & Bump", float) = 0	
		_SpecColor ("  Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_BumpSpecMap ("  Normalmap (GA) Spec (R)", 2D) = "bump" {}

		//Translucency
		[CurvedWorldLargeLabel] V_CW_Label_Translucency("Translucency", float) = 0	
		_TranslucencyMap ("  Trans (RGB) Gloss(A)", 2D) = "white" {}
	
		// These are here only to provide default values		
		[HideInInspector] _TreeInstanceColor ("TreeInstanceColor", Vector) = (1,1,1,1)
		[HideInInspector] _TreeInstanceScale ("TreeInstanceScale", Vector) = (1,1,1,1)
		[HideInInspector] _SquashAmount ("Squash", Float) = 1


		//Curved World
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Curved World Optionals", float) = 0
	}

	SubShader 
	{ 
		Tags { "RenderType"="CurvedWorld_TreeBark"  
		       "IgnoreProjector"="True"
			   "CurvedWorldTag"="Nature/Tree Creator Bark Optimized" 
			   "CurvedWorldNoneRemoveableKeywords"=""  
			   "CurvedWorldAvailableOptions"="" 
			} 
		LOD 200
	
		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:TreeVertBark addshadow nolightmap
		


		#include "../cginc/CurvedWorld_UnityBuiltin3xTreeLibrary.cginc"

		sampler2D _MainTex;
		sampler2D _BumpSpecMap;
		sampler2D _TranslucencyMap;

		struct Input 
		{
			float2 uv_MainTex;
			fixed4 color : COLOR;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * IN.color.rgb * IN.color.a;
	
			fixed4 trngls = tex2D (_TranslucencyMap, IN.uv_MainTex);
			o.Gloss = trngls.a * _Color.r;
			o.Alpha = c.a;
	
			half4 norspc = tex2D (_BumpSpecMap, IN.uv_MainTex);
			o.Specular = norspc.r;
			o.Normal = UnpackNormalDXT5nm(norspc);
		}
		ENDCG
	}

	Dependency "BillboardShader" = "Hidden/VacuumShaders/Curved World/Nature/Tree Creator Bark Rendertex"

	CustomEditor "CurvedWorld_Material_Editor"
}
