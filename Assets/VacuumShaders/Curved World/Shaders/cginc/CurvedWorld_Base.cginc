// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

#ifndef VACUUM_CURVEDWORLD_BASE_CGINC
#define VACUUM_CURVEDWORLD_BASE_CGINC 


#include "UnityCG.cginc"
////////////////////////////////////////////////////////////////////////////
//																		  //
//Variables 															  //
//																		  //
////////////////////////////////////////////////////////////////////////////

uniform float3 _V_CW_Bend;
uniform float3 _V_CW_Bias;	
uniform float4 _V_CW_PivotPoint_Position;

uniform float4x4 _V_CW_Camera2World;
uniform float4x4 _V_CW_World2Camera;

const float2 _zero2 = float2(0,0);
const float3 _zero3 = float3(0,0,0);
////////////////////////////////////////////////////////////////////////////
//																		  //
//Defines    															  //
//																		  //
////////////////////////////////////////////////////////////////////////////

/*DO NOT DELETE - CURVED WORLD BEND TYPE*/ #define V_CW_BENDTYPE_CLASSIC_RUNNER

#define SIGN(a) (float2(a.x < 0 ? -1.0f : 1.0f, a.y < 0 ? -1.0f : 1.0f))


#define PIVOT _V_CW_PivotPoint_Position.xyz

#if defined(V_CW_PARTICLE_SYSTEM_ON)
	#define Object2World _V_CW_Camera2World
	#define World2Object _V_CW_World2Camera
#else
	#define Object2World unity_ObjectToWorld
	#define World2Object unity_WorldToObject
#endif

////////////////////////////////////////////////////////////////////////////
//																		  //
//Functions    															  //
//																		  //
////////////////////////////////////////////////////////////////////////////

inline void V_CW_TransformPoint(inout float4 vertex)
{	
	#if defined(V_CW_BENDTYPE_CLASSIC_RUNNER)
		
		float4 worldPos = mul( Object2World, vertex ); 
		worldPos.xyz -= PIVOT;

		float2 xyOff = max(_zero2, worldPos.zz - _V_CW_Bias.xy);
		xyOff *= xyOff;
		worldPos = float4(-_V_CW_Bend.y * xyOff.y, _V_CW_Bend.x * xyOff.x, 0.0f, 0.0f) * 0.001; 

		vertex += mul(World2Object, worldPos);

	#elif defined(V_CW_BENDTYPE_LITTLE_PLANET) 
		
		float4 worldPos = mul( Object2World, vertex ); 
		worldPos.xyz -= PIVOT;

		float2 xzOff = max(_zero2, abs(worldPos.zx) - _V_CW_Bias.xz);
		xzOff *= step(_zero2, worldPos.zx) * 2 - 1;
		xzOff *= xzOff;
		worldPos = float4(0, (_V_CW_Bend.x * xzOff.x + _V_CW_Bend.z * xzOff.y) * 0.001, 0, 0); 

		vertex += mul(World2Object, worldPos);

	#elif defined(V_CW_BENDTYPE_UNIVERSAL)
		
		float4 worldPos = mul( Object2World, vertex ); 
		worldPos.xyz -= PIVOT;

		float3 xyzOff = max(_zero3, abs(worldPos.zzx) - _V_CW_Bias.xyz);
		xyzOff *= step(_zero3, worldPos.zzx) * 2 - 1;
		xyzOff *= xyzOff;
		worldPos = float4(-_V_CW_Bend.y * xyzOff.y, _V_CW_Bend.x * xyzOff.x + _V_CW_Bend.z * xyzOff.z, 0.0f, 0.0f) * 0.001; 

		vertex += mul(World2Object, worldPos);

	#elif defined(V_CW_BENDTYPE_PERSPECTIVE_2D)

		float4 modelView = mul(UNITY_MATRIX_MV, vertex); 	

		float2 xyOff = max(_zero2, abs(modelView.yx) - _V_CW_Bias.xy) * sign(modelView.yx);	
		xyOff *= xyOff;
		modelView.z -= (_V_CW_Bend.x * xyOff.x + _V_CW_Bend.y * xyOff.y) * 0.001;
			
		vertex = mul(modelView, UNITY_MATRIX_IT_MV);

	#else
		
		//Do nothing

	#endif
} 

inline void V_CW_TransformPointAndNormal(inout float4 vertex, inout float3 normal, float3 worldPos, float3 worldTangent, float3 worldBinormal)
{

	float3 v0 = worldPos - PIVOT;
	float3 v1 = v0 + worldTangent;
	float3 v2 = v0 + worldBinormal;
	

	#if defined(V_CW_BENDTYPE_CLASSIC_RUNNER)

		float2 xyOff = max(_zero2, v0.zz - _V_CW_Bias.xy);
		xyOff *= xyOff;
		float3 transformedVertex = float3(-_V_CW_Bend.y * xyOff.y, _V_CW_Bend.x * xyOff.x, 0.0f) * 0.001; 
		v0 += transformedVertex;
		

		xyOff = max(_zero2, v1.zz - _V_CW_Bias.xy);
		xyOff *= xyOff;
		v1.xy += float2(-_V_CW_Bend.y * xyOff.y, _V_CW_Bend.x * xyOff.x) * 0.001; 

		
		xyOff = max(_zero2, v2.zz - _V_CW_Bias.xy);
		xyOff *= xyOff;
		v2.xy += float2(-_V_CW_Bend.y * xyOff.y, _V_CW_Bend.x * xyOff.x) * 0.001; 


		vertex.xyz += mul((float3x3)World2Object, transformedVertex);
		normal = normalize(mul((float3x3)World2Object, normalize(cross(v2 - v0, v1 - v0))));

	#elif defined(V_CW_BENDTYPE_LITTLE_PLANET)

		float2 xzOff = max(_zero2, abs(v0.zx) - _V_CW_Bias.xz);
		xzOff *= step(_zero2, v0.zx) * 2 - 1;
		xzOff *= xzOff;
		float3 transformedVertex = float3(0, (_V_CW_Bend.x * xzOff.x + _V_CW_Bend.z * xzOff.y) * 0.001, 0); 
		v0 += transformedVertex;
					  
	  		
		xzOff = max(_zero2, abs(v1.zx) - _V_CW_Bias.xz);
		xzOff *= step(_zero2, v1.zx) * 2 - 1;
		xzOff *= xzOff; 		
		v1.y += (_V_CW_Bend.x * xzOff.x + _V_CW_Bend.z * xzOff.y) * 0.001;
				 
			
		xzOff = max(_zero2, abs(v2.zx) - _V_CW_Bias.xz);
		xzOff *= step(_zero2, v2.zx) * 2 - 1;
		xzOff *= xzOff; 		
		v2.y += (_V_CW_Bend.x * xzOff.x + _V_CW_Bend.z * xzOff.y) * 0.001;	


		vertex.xyz += mul((float3x3)World2Object, transformedVertex);
		normal = normalize(mul((float3x3)World2Object, normalize(cross(v2 - v0, v1 - v0))));

	#elif defined(V_CW_BENDTYPE_UNIVERSAL)

		float3 xyzOff = max(_zero3, abs(v0.zx).xxy - _V_CW_Bias);
		xyzOff *= step(_zero3, v0.xxy) * 2 - 1;
		xyzOff *= xyzOff;
		float3 transformedVertex = float3(-_V_CW_Bend.y * xyzOff.y, _V_CW_Bend.x * xyzOff.x + _V_CW_Bend.z * xyzOff.z, 0.0f) * 0.001; 
		v0 += transformedVertex;
		

		xyzOff = max(_zero3, abs(v1.zx).xxy - _V_CW_Bias);
		xyzOff *= step(_zero3, v1.xxy) * 2 - 1;
		xyzOff *= xyzOff;
		v1.xy += float2(-_V_CW_Bend.y * xyzOff.y, _V_CW_Bend.x * xyzOff.x + _V_CW_Bend.z * xyzOff.z) * 0.001; 


		xyzOff = max(_zero3, abs(v2.zx).xxy - _V_CW_Bias);
		xyzOff *= step(_zero3, v2.xxy) * 2 - 1;
		xyzOff *= xyzOff;
		v2.xy += float2(-_V_CW_Bend.y * xyzOff.y, _V_CW_Bend.x * xyzOff.x + _V_CW_Bend.z * xyzOff.z) * 0.001; 
	
		
		vertex.xyz += mul((float3x3)World2Object, transformedVertex);
		normal = normalize(mul((float3x3)World2Object, normalize(cross(v2 - v0, v1 - v0))));

	#else

		//Do nothing

	#endif
}  

inline void V_CW_TransformPointAndNormal(inout float4 vertex, inout float3 normal, float4 tangent)
{	
	float3 worldPos = mul(Object2World, vertex).xyz; 
	float3 worldNormal = UnityObjectToWorldNormal(normal);
	float3 worldTangent = UnityObjectToWorldDir(tangent.xyz);
	float3 worldBinormal = cross(worldNormal, worldTangent) * -1;// * tangent.w;

	V_CW_TransformPointAndNormal(vertex, normal, worldPos, worldTangent, worldBinormal);
}

#endif 
