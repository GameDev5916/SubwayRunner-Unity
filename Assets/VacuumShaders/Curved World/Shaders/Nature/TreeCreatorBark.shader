Shader "VacuumShaders/Curved World/Nature/Tree Creator Bark" 
{
	Properties 
	{
		[CurvedWorldGearMenu] V_CW_Label_Tag("", float) = 0
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Default Visual Options", float) = 0


		//Albedo
		[CurvedWorldLargeLabel] V_CW_Label_Albedo("Albedo", float) = 0	
		_Color("  Color", color) = (1, 1, 1, 1)		
		_MainTex ("  Map", 2D) = "white" {}
		
		//Specular
		[CurvedWorldLargeLabel] V_CW_Label_Specular("Specular", float) = 0	
		_SpecColor ("  Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("  Shininess", Range (0.01, 1)) = 0.078125
		_GlossMap ("  Gloss (A)", 2D) = "black" {} 

		//Bump
		[CurvedWorldLargeLabel] V_CW_Label_Bump("Bump", float) = 0	
		_BumpMap ("  Normalmap", 2D) = "bump" {}
		
	
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
			   "CurvedWorldTag"="Nature/Tree Creator Bark" 
			   "CurvedWorldNoneRemoveableKeywords"=""  
			   "CurvedWorldAvailableOptions"="" 
			} 
		LOD 200
		
		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:TreeVertBark addshadow nolightmap



		#include "../cginc/CurvedWorld_UnityBuiltin3xTreeLibrary.cginc"

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _GlossMap;
		half _Shininess;

		struct Input 
		{
			float2 uv_MainTex;
			fixed4 color : COLOR;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * IN.color.rgb * IN.color.a;
			o.Gloss = tex2D(_GlossMap, IN.uv_MainTex).a;
			o.Alpha = c.a;
			o.Specular = _Shininess;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));

		}
		ENDCG
	}

	Dependency "OptimizedShader" = "VacuumShaders/Curved World/Nature/Tree Creator Bark Optimized"
	
	FallBack "Hidden/VacuumShaders/Curved World/VertexLit/Diffuse" 

	CustomEditor "CurvedWorld_Material_Editor"
}
