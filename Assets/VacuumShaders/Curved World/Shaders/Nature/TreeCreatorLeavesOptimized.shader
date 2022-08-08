Shader "VacuumShaders/Curved World/Nature/Tree Creator Leaves Optimized" 
{
	Properties 
	{
		[CurvedWorldGearMenu] V_CW_Label_Tag("", float) = 0
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Default Visual Options", float) = 0


		//Albedo
		[CurvedWorldLargeLabel] V_CW_Label_Albedo("Albedo", float) = 0	
		_Color ("  Color", Color) = (1,1,1,1)
		_MainTex ("  Map (RGB) Alpha (A)", 2D) = "white" {}

		//Bump
		[CurvedWorldLargeLabel] V_CW_Label_Bump("Specular, Bump & Shadow", float) = 0	
		_BumpSpecMap ("  Normalmap (GA) Spec (R) Shadow Offset (B)", 2D) = "bump" {}

		//Translucency
		[CurvedWorldLargeLabel] V_CW_Label_Translucency("Translucency", float) = 0
		_TranslucencyColor ("  Translucency Color", Color) = (0.73,0.85,0.41,1) // (187,219,106,255)
		_TranslucencyMap ("  Trans (B) Gloss(A)", 2D) = "white" {}
		_TranslucencyViewDependency ("  View dependency", Range(0,1)) = 0.7
		
		//Shadow
		[CurvedWorldLargeLabel] V_CW_Label_Shadow("Shadow", float) = 0
		_ShadowTex ("  Shadow (RGB)", 2D) = "white" {}
		_ShadowStrength("  Shadow Strength", Range(0,1)) = 0.8
		_ShadowOffsetScale ("  Shadow Offset Scale", Float) = 1		
		
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
		       "CurvedWorldTag"="Nature/Tree Creator Leaves Optimized" 
			   "CurvedWorldNoneRemoveableKeywords"=""  
			   "CurvedWorldAvailableOptions"="" 
			} 
		LOD 200
	
		CGPROGRAM
		#pragma surface surf TreeLeaf alphatest:_Cutoff vertex:TreeVertLeaf nolightmap noforwardadd



		#include "../cginc/CurvedWorld_UnityBuiltin3xTreeLibrary.cginc"

		sampler2D _MainTex;
		sampler2D _BumpSpecMap;
		sampler2D _TranslucencyMap;

		struct Input 
		{
			float2 uv_MainTex;
			fixed4 color : COLOR; // color.a = AO
		};

		void surf (Input IN, inout LeafSurfaceOutput o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * IN.color.rgb * IN.color.a;
	
			fixed4 trngls = tex2D (_TranslucencyMap, IN.uv_MainTex);
			o.Translucency = trngls.b;
			o.Gloss = trngls.a * _Color.r;
			o.Alpha = c.a;
	
			half4 norspc = tex2D (_BumpSpecMap, IN.uv_MainTex);
			o.Specular = norspc.r;
			o.Normal = UnpackNormalDXT5nm(norspc);
		}
		ENDCG

		// Pass to render object as a shadow caster
		Pass 
		{
			Name "ShadowCaster"
			Tags { "LightMode" = "ShadowCaster" }
		
			CGPROGRAM
			#pragma vertex vert_surf
			#pragma fragment frag_surf
			#pragma multi_compile_shadowcaster
			#include "HLSLSupport.cginc"
			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			#define INTERNAL_DATA
			#define WorldReflectionVector(data,normal) data.worldRefl

		

			#include "../cginc/CurvedWorld_UnityBuiltin3xTreeLibrary.cginc"


			sampler2D _MainTex;

			struct Input 
			{
				float2 uv_MainTex;
			};

			struct v2f_surf 
			{
				V2F_SHADOW_CASTER;
				float2 hip_pack0 : TEXCOORD1;
			};

			float4 _MainTex_ST;
			v2f_surf vert_surf (appdata_full v) 
			{
				v2f_surf o;
				
				TreeVertLeaf (v);
				o.hip_pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
				TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
				return o;
			}
			fixed _Cutoff;
			float4 frag_surf (v2f_surf IN) : SV_Target 
			{
				half alpha = tex2D(_MainTex, IN.hip_pack0.xy).a;
				clip (alpha - _Cutoff);
				SHADOW_CASTER_FRAGMENT(IN)
			}
			ENDCG
		}
	
	}

	Dependency "BillboardShader" = "Hidden/VacuumShaders/Curved World/Nature/Tree Creator Leaves Rendertex"

	CustomEditor "CurvedWorld_Material_Editor"
}
