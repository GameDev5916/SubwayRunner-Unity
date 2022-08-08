// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

#ifndef VACUUM_CURVEDWORLD_UNLIT_CGINC
#define VACUUM_CURVEDWORLD_UNLIT_CGINC

#include "UnityCG.cginc"
#include "../cginc/CurvedWorld_Base.cginc"
#include "../cginc/CurvedWorld_Functions.cginc"


//Variables/////////////////////////////////////////////////////////////
uniform float _V_CW_OutlineWidth;
uniform float4 _V_CW_OutlineColor;

//Structs///////////////////////////////////////////////////////////////
struct vInput
{
	float4 vertex : POSITION;    
	float3 normal : NORMAL;
};

struct vOutput
{
	float4 pos : SV_POSITION;

	#ifdef V_CW_FOG
		UNITY_FOG_COORDS(0)
	#endif

	fixed4 color : COLOR;
};

//Vertex////////////////////////////////////////////////////////////////
vOutput vert(vInput v)
{ 
	vOutput o;
	UNITY_INITIALIZE_OUTPUT(vOutput,o); 
			
	V_CW_TransformPoint(v.vertex);
	
	float2 offset = TransformViewToProjection(mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal).xy);

	#ifndef V_CW_OUTLINE_FIXED_SIZE
		offset /= (distance(_WorldSpaceCameraPos, mul(unity_ObjectToWorld, v.vertex)));
	#endif

	o.pos = UnityObjectToClipPos(v.vertex);		
	o.pos.xy += offset * o.pos.z * _V_CW_OutlineWidth * 0.001;

	o.color = _V_CW_OutlineColor;
	
	#ifdef V_CW_FOG
		UNITY_TRANSFER_FOG(o,o.pos);
	#endif

	return o;
}


//Fragment//////////////////////////////////////////////////////////////
fixed4 frag (vOutput i) : SV_Target
{
	#ifdef V_CW_FOG
		UNITY_APPLY_FOG(i.fogCoord, i.color);
	#endif

	return i.color;
}


#endif