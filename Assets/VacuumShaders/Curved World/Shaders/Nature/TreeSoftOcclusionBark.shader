Shader "VacuumShaders/Curved World/Nature/Tree Soft Occlusion Bark" 
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
		Tags { "IgnoreProjector"="True"
			   "RenderType" = "CurvedWorld_TreeOpaque"
			   "DisableBatching"="True"
		       "CurvedWorldTag"="Nature/Tree Soft Occlusion Bark" 
			   "CurvedWorldNoneRemoveableKeywords"=""  
			   "CurvedWorldAvailableOptions"="" 
			} 
		Pass 
		{
			Lighting On
		
			CGPROGRAM  
			#pragma vertex bark
			#pragma fragment frag
			#pragma multi_compile_fog



			#include "../cginc/CurvedWorld_UnityBuiltin2xTreeLibrary.cginc"
			
			sampler2D _MainTex;
			
			fixed4 frag(v2f input) : SV_Target
			{
				fixed4 col = input.color;
				col.rgb *= tex2D( _MainTex, input.uv.xy).rgb;
				UNITY_APPLY_FOG(input.fogCoord, col);
				UNITY_OPAQUE_ALPHA(col.a);
				return col;
			}
			ENDCG
		}
		
		Pass 
		{
			Name "ShadowCaster"
			Tags { "LightMode" = "ShadowCaster" }
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_shadowcaster
			#include "UnityCG.cginc"
			#include "TerrainEngine.cginc"


			#include "../cginc/CurvedWorld_Base.cginc"
			
			struct v2f 
			{ 
				V2F_SHADOW_CASTER;
			};
			
			struct appdata 
			{
			    float4 vertex : POSITION;
				float3 normal : NORMAL;
			    fixed4 color : COLOR;
				float4 tangent : TANGENT;
			};
			v2f vert( appdata v )
			{
				v2f o;

				V_CW_TransformPointAndNormal(v.vertex, v.normal, v.tangent);

				TerrainAnimateTree(v.vertex, v.color.w);
				TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
				return o;
			}
			
			float4 frag( v2f i ) : SV_Target
			{
				SHADOW_CASTER_FRAGMENT(i)
			}
			ENDCG	
		}
	}
	
	Dependency "BillboardShader" = "Hidden/VacuumShaders/Curved World/Nature/Tree Soft Occlusion Bark Rendertex"
	Fallback Off

	CustomEditor "CurvedWorld_Material_Editor"
}
