// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Spheremask"
{
	Properties
	{
		_Location("Location", Vector) = (0,0,0,0)
		_Radius("Radius", Float) = 1
		_Hardness("Hardness", Float) = 1
		_LineWidth("LineWidth", Float) = 1
		[HDR]_Color("Color", Color) = (1,0,0,1)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Min("Min", Float) = 0
		_Max("Max", Float) = 0
		[HDR]_Brighty("Brighty", Color) = (0,0,0,0)
		_DisintegrationTexture("DisintegrationTexture", 2D) = "white" {}
		_Erosion("Erosion", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float3 _Location;
		uniform float _Min;
		uniform float _Max;
		uniform float _Radius;
		uniform float _Hardness;
		uniform float _LineWidth;
		uniform float4 _Brighty;
		uniform float4 _Color;
		uniform sampler2D _TextureSample0;
		uniform sampler2D _DisintegrationTexture;
		uniform float4 _DisintegrationTexture_ST;
		uniform float _Erosion;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 Location10 = _Location;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float3 temp_output_5_0_g3 = ( ( ase_worldPos - Location10 ) / _Radius );
			float dotResult8_g3 = dot( temp_output_5_0_g3 , temp_output_5_0_g3 );
			float clampResult10_g3 = clamp( dotResult8_g3 , 0.0 , 1.0 );
			float temp_output_1_0 = pow( clampResult10_g3 , _Hardness );
			float3 temp_output_5_0_g2 = ( ( ase_worldPos - Location10 ) / ( _Radius + _LineWidth ) );
			float dotResult8_g2 = dot( temp_output_5_0_g2 , temp_output_5_0_g2 );
			float clampResult10_g2 = clamp( dotResult8_g2 , 0.0 , 1.0 );
			float clampResult45 = clamp( ( temp_output_1_0 + saturate( ( pow( clampResult10_g2 , _Hardness ) - temp_output_1_0 ) ) ) , 0.0 , 1.0 );
			float smoothstepResult56 = smoothstep( _Min , _Max , clampResult45);
			float3 lerpResult70 = lerp( ( Location10 - ase_worldPos ) , float3( 0,0,0 ) , smoothstepResult56);
			v.vertex.xyz += lerpResult70;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 Location10 = _Location;
			float3 temp_output_5_0_g3 = ( ( ase_worldPos - Location10 ) / _Radius );
			float dotResult8_g3 = dot( temp_output_5_0_g3 , temp_output_5_0_g3 );
			float clampResult10_g3 = clamp( dotResult8_g3 , 0.0 , 1.0 );
			float temp_output_1_0 = pow( clampResult10_g3 , _Hardness );
			float3 temp_output_5_0_g2 = ( ( ase_worldPos - Location10 ) / ( _Radius + _LineWidth ) );
			float dotResult8_g2 = dot( temp_output_5_0_g2 , temp_output_5_0_g2 );
			float clampResult10_g2 = clamp( dotResult8_g2 , 0.0 , 1.0 );
			float clampResult45 = clamp( ( temp_output_1_0 + saturate( ( pow( clampResult10_g2 , _Hardness ) - temp_output_1_0 ) ) ) , 0.0 , 1.0 );
			float smoothstepResult56 = smoothstep( _Min , _Max , clampResult45);
			float4 lerpResult81 = lerp( _Brighty , ( _Color * tex2D( _TextureSample0, i.uv_texcoord ) ) , smoothstepResult56);
			o.Emission = lerpResult81.rgb;
			float2 uv0_DisintegrationTexture = i.uv_texcoord * _DisintegrationTexture_ST.xy + _DisintegrationTexture_ST.zw;
			float Opacity90 = step( tex2D( _DisintegrationTexture, uv0_DisintegrationTexture ).r , _Erosion );
			o.Alpha = Opacity90;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16900
0;73.6;702;440;1407.963;-1060.035;1.811856;True;False
Node;AmplifyShaderEditor.Vector3Node;2;-1842.1,329.3632;Float;False;Property;_Location;Location;0;0;Create;True;0;0;False;0;0,0,0;0.56,0,-0.75;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;10;-1632.657,244.5152;Float;False;Location;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-1515.735,90.42358;Float;False;Property;_Radius;Radius;1;0;Create;True;0;0;False;0;1;2.72;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1534.044,431.6351;Float;False;Property;_LineWidth;LineWidth;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-1147.466,118.8748;Float;False;Property;_Hardness;Hardness;2;0;Create;True;0;0;False;0;1;12.84;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;11;-1334.848,109.2728;Float;False;10;Location;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-1345.8,249.755;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1;-957.1387,-85.40065;Float;True;SphereMask;-1;;3;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT3;0,0,0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;18;-990.125,218.8918;Float;True;SphereMask;-1;;2;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT3;0,0,0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;92;-1343.499,1173.336;Float;False;1162.417;523.7651;;5;85;89;84;88;90;Erosion;1,0.585702,0,1;0;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;42;-423.7545,149.625;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;85;-1293.499,1398.117;Float;False;0;84;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;43;-231.511,198.417;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;84;-1031.742,1409.198;Float;True;Property;_DisintegrationTexture;DisintegrationTexture;9;0;Create;True;0;0;False;0;9789d23040cb1fb45ad60392430c3c15;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;89;-718.0772,1223.336;Float;False;Property;_Erosion;Erosion;10;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;44;-1092.604,694.9301;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;55;-613.0073,-9.056992;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;78;-518.5367,623.9271;Float;False;10;Location;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;45;-931.4037,681.9398;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;80;-575.8348,450.1502;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;58;-974.5218,587.0926;Float;False;Property;_Max;Max;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;54;-493.1091,-459.4212;Float;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;False;0;e28dc97a9541e3642a48c0e3886688c5;e28dc97a9541e3642a48c0e3886688c5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;47;-478.7875,-215.9076;Float;False;Property;_Color;Color;4;1;[HDR];Create;True;0;0;False;0;1,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;88;-670.0713,1444.501;Float;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-975.971,471.1454;Float;False;Property;_Min;Min;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;56;-770.1646,516.075;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-155.4635,-40.28862;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;90;-421.0819,1481.07;Float;False;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;79;-297.0699,481.8659;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;82;-77.13631,742.8124;Float;False;Property;_Brighty;Brighty;8;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotateAboutAxisNode;83;-309.4594,697.8163;Float;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;70;-126.9883,572.0444;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;81;197.7947,596.0046;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;91;-238.9227,102.0638;Float;False;90;Opacity;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Spheremask;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;2;0
WireConnection;16;0;3;0
WireConnection;16;1;15;0
WireConnection;1;15;11;0
WireConnection;1;14;3;0
WireConnection;1;12;4;0
WireConnection;18;15;11;0
WireConnection;18;14;16;0
WireConnection;18;12;4;0
WireConnection;42;0;18;0
WireConnection;42;1;1;0
WireConnection;43;0;42;0
WireConnection;84;1;85;0
WireConnection;44;0;1;0
WireConnection;44;1;43;0
WireConnection;45;0;44;0
WireConnection;54;1;55;0
WireConnection;88;0;84;1
WireConnection;88;1;89;0
WireConnection;56;0;45;0
WireConnection;56;1;57;0
WireConnection;56;2;58;0
WireConnection;53;0;47;0
WireConnection;53;1;54;0
WireConnection;90;0;88;0
WireConnection;79;0;78;0
WireConnection;79;1;80;0
WireConnection;70;0;79;0
WireConnection;70;2;56;0
WireConnection;81;0;82;0
WireConnection;81;1;53;0
WireConnection;81;2;56;0
WireConnection;0;2;81;0
WireConnection;0;9;91;0
WireConnection;0;11;70;0
ASEEND*/
//CHKSM=B21A58F6C8D9021608A033EDFB0756A958F8AFD2