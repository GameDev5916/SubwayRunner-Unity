// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "VacuumShaders/Curved World/FX/Flare" 
{
	Properties 
	{
		[CurvedWorldGearMenu] V_CW_Label_Tag("", float) = 0
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Default Visual Options", float) = 0
		  

		//Main
		_Color("  Color", color) = (1, 1, 1, 1)
		_MainTex ("  Map (RGB) Trans (A)", 2D) = "white" {}
		


		//Curved World
		[CurvedWorldLabel] V_CW_Label_UnityDefaults("Curved World Optionals", float) = 0

	}

	SubShader 
	{
		Tags { "Queue"="Transparent"
			   "IgnoreProjector"="True"
			   "RenderType"="Transparent"
			   "PreviewType"="Plane"
			   "CurvedWorldTag"="FX/Flare" 
			   "CurvedWorldNoneRemoveableKeywords"=""  
			   "CurvedWorldAvailableOptions"=""
		     }
		Cull Off 
		Lighting Off 
		ZWrite Off 
		Ztest Always
		Blend One One

		Pass 
		{	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"


			#include "../cginc/CurvedWorld_Base.cginc" 


			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
		
			struct appdata_t 
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f 
			{
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};			
		
			v2f vert (appdata_t v)
			{
				v2f o;

				V_CW_TransformPoint(v.vertex);	

				o.vertex = UnityObjectToClipPos(v.vertex);				
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.color = v.color * _Color;

				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{				
				fixed4 c = tex2D(_MainTex, i.texcoord) * i.color;
				
				return c;
			}
			ENDCG 
		}
	} 	

	CustomEditor "CurvedWorld_Material_Editor"
}
