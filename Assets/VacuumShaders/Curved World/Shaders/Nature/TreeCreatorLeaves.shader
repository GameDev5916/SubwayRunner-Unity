Shader "VacuumShaders/Curved World/Nature/Tree Creator Leaves" 
{
	Properties 
	{
		[CurvedWorldGearMenu] V_CW_Label_Tag("", float) = 0
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Default Visual Options", float) = 0


		//Albedo
		[CurvedWorldLargeLabel] V_CW_Label_Albedo("Albedo", float) = 0	
		_Color ("  Color", Color) = (1,1,1,1)
		_MainTex ("  Map (RGB) Alpha (A)", 2D) = "white" {}

		//Specular
		[CurvedWorldLargeLabel] V_CW_Label_Specular("Specular", float) = 0	
		_Shininess ("  Shininess", Range (0.01, 1)) = 0.078125
		_GlossMap ("  Gloss (A)", 2D) = "black" {}

		//Bump
		[CurvedWorldLargeLabel] V_CW_Label_Bump("Bump", float) = 0	
		_BumpMap ("  Normalmap", 2D) = "bump" {}
		
		//Translucency
		[CurvedWorldLargeLabel] V_CW_Label_Translucency("Translucency", float) = 0
		_TranslucencyMap ("  Translucency (A)", 2D) = "white" {}

		//Shadow
		[CurvedWorldLargeLabel] V_CW_Label_Shadow("Shadow", float) = 0
		_ShadowOffset ("  Shadow Offset (A)", 2D) = "black" {}
	
		//Cutoff
		[CurvedWorldLargeLabel] V_CW_Label_Cutoff("Cutout", float) = 0	
		_Cutoff ("  Alpha cutoff", Range(0,1)) = 0.3

		// These are here only to provide default values
		[HideInInspector] _TreeInstanceColor ("TreeInstanceColor", Vector) = (1,1,1,1)
		[HideInInspector] _TreeInstanceScale ("TreeInstanceScale", Vector) = (1,1,1,1)
		[HideInInspector] _SquashAmount ("Squash", Float) = 1



		//Curved World
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Curved World Optionals", float) = 0
	}

	SubShader 
	{ 
		Tags { "IgnoreProjector"="True" 
		       "RenderType"="CurvedWorld_TreeLeaf" 
			   "CurvedWorldTag"="Nature/Tree Creator Leaves" 
			   "CurvedWorldNoneRemoveableKeywords"=""  
			   "CurvedWorldAvailableOptions"="" 
			} 
		LOD 200
		
		CGPROGRAM
		#pragma surface surf TreeLeaf alphatest:_Cutoff vertex:TreeVertLeaf addshadow nolightmap noforwardadd
		


		#include "../cginc/CurvedWorld_UnityBuiltin3xTreeLibrary.cginc"

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _GlossMap;
		sampler2D _TranslucencyMap;
		half _Shininess;

		struct Input 
		{
			float2 uv_MainTex;
			fixed4 color : COLOR; // color.a = AO
		};

		void surf (Input IN, inout LeafSurfaceOutput o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * IN.color.rgb * IN.color.a;
			o.Translucency = tex2D(_TranslucencyMap, IN.uv_MainTex).rgb;
			o.Gloss = tex2D(_GlossMap, IN.uv_MainTex).a;
			o.Alpha = c.a;
			o.Specular = _Shininess;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
		}
		ENDCG
	}
	 
	Dependency "OptimizedShader" = "VacuumShaders/Curved World/Nature/Tree Creator Leaves Optimized"
	
	FallBack "Hidden/VacuumShaders/Curved World/VertexLit/Diffuse"

	CustomEditor "CurvedWorld_Material_Editor"
}
