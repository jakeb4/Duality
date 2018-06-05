// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "InvisibleShader"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_DistortionMap("DistortionMap", 2D) = "bump" {}
		_DistortionScale("Distortion Scale", Range( 0 , 1)) = 0
		_RippleScale("RippleScale", Range( 0 , 20)) = 0
		_RippleSpeed("RippleSpeed", Range( 0 , 1)) = 0
		_Blending("Blending", Range( 0 , 1)) = 0
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ "_GrabScreen0" }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 screenPos;
		};

		uniform sampler2D _GrabScreen0;
		uniform sampler2D _DistortionMap;
		uniform float _RippleScale;
		uniform float _RippleSpeed;
		uniform float _DistortionScale;
		uniform float _Blending;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = float3(0,0,0);
			float4 screenPos6 = i.screenPos;
			#if UNITY_UV_STARTS_AT_TOP
			float scale6 = -1.0;
			#else
			float scale6 = 1.0;
			#endif
			float halfPosW6 = screenPos6.w * 0.5;
			screenPos6.y = ( screenPos6.y - halfPosW6 ) * _ProjectionParams.x* scale6 + halfPosW6;
			screenPos6.w += 0.00000000001;
			screenPos6.xyzw /= screenPos6.w;
			float4 temp_cast_0 = (( _Time.y * _RippleSpeed )).xxxx;
			float4 temp_cast_2 = (1.0).xxxx;
			o.Emission = lerp( tex2Dproj( _GrabScreen0, UNITY_PROJ_COORD( ( float4( ( UnpackNormal( tex2D( _DistortionMap,( _RippleScale * (( temp_cast_0 + screenPos6 )).xy )) ) * _DistortionScale ) , 0.0 ) + screenPos6 ) ) ) , temp_cast_2 , _Blending ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
0;92;1150;542;1113.15;509.9496;1.6;True;True
Node;AmplifyShaderEditor.TimeNode;9;-1459.2,-264.8;Float;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;16;-1536.85,-87.14998;Float;False;Property;_RippleSpeed;RippleSpeed;2;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.GrabScreenPosition;6;-1377.001,101.4;Float;False;0;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1234.001,-160.3;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-1048.801,-17.19999;Float;False;0;FLOAT;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;FLOAT4
Node;AmplifyShaderEditor.SwizzleNode;10;-1057.4,-122.1;Float;False;FLOAT2;0;1;2;3;0;FLOAT4;0,0,0,0;False;FLOAT2
Node;AmplifyShaderEditor.RangedFloatNode;13;-1332.099,-338.8003;Float;False;Property;_RippleScale;RippleScale;2;0;0;0;20;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-999.1003,-273.9;Float;False;0;FLOAT;0,0;False;1;FLOAT2;0,0;False;FLOAT2
Node;AmplifyShaderEditor.RangedFloatNode;8;-829.2003,-128.9999;Float;False;Property;_DistortionScale;Distortion Scale;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;3;-841.7999,-347.3001;Float;True;Property;_DistortionMap;DistortionMap;0;0;Assets/AmplifyShaderEditor/Examples/Assets/Textures/Misc/SmallWaves.png;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-503.4001,-248.2;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleAddOpNode;4;-438.6001,-66.49992;Float;False;0;FLOAT3;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;20;-329.1503,198.8503;Float;False;Property;_Blending;Blending;4;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;19;-333.9503,104.4503;Float;False;Constant;_White;White;4;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.ScreenColorNode;2;-305.4997,-78.40002;Float;False;Global;_GrabScreen0;Grab Screen 0;0;0;Object;-1;0;FLOAT4;0,0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.Vector3Node;1;-255.5,-273;Float;False;Constant;_Vector0;Vector 0;0;0;0,0,0;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;17;-82.75008,66.05036;Float;False;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;2;FLOAT;0.0;False;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;120.6,-294.4;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;InvisibleShader;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Translucent;0.5;True;True;0;False;Opaque;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;15;0;9;2
WireConnection;15;1;16;0
WireConnection;14;0;15;0
WireConnection;14;1;6;0
WireConnection;10;0;14;0
WireConnection;11;0;13;0
WireConnection;11;1;10;0
WireConnection;3;1;11;0
WireConnection;7;0;3;0
WireConnection;7;1;8;0
WireConnection;4;0;7;0
WireConnection;4;1;6;0
WireConnection;2;0;4;0
WireConnection;17;0;2;0
WireConnection;17;1;19;0
WireConnection;17;2;20;0
WireConnection;0;0;1;0
WireConnection;0;2;17;0
ASEEND*/
//CHKSM=2A87D8F03F2B72DA956652FF189BF485CA676008