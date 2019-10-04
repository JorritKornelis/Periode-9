// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DistDis"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Location("Location", Vector) = (0,0,0,0)
		_Radius("Radius", Float) = 0
		_SmallRadius("SmallRadius", Float) = 0
		_Hardness("Hardness", Float) = 0
		_SmallHardness("SmallHardness", Float) = 0
		_Speed("Speed", Vector) = (0,0,0,0)
		_Color0("Color 0", Color) = (1,1,1,0)
		_Width("Width", Float) = 0.1
		[HDR]_Color1("Color 1", Color) = (1,0,0,0)
		_MainTex("MainTex", 2D) = "white" {}
		_OpacityInverter("OpacityInverter", Range( 0 , 0.01)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float4 _Color0;
		uniform sampler2D _TextureSample0;
		uniform float2 _Speed;
		uniform float4 _TextureSample0_ST;
		uniform float3 _Location;
		uniform float _Radius;
		uniform float _Width;
		uniform half _OpacityInverter;
		uniform float _Hardness;
		uniform float4 _Color1;
		uniform float _SmallRadius;
		uniform float _SmallHardness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv0_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			o.Albedo = ( tex2D( _MainTex, uv0_MainTex ) * _Color0 ).rgb;
			float2 uv0_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float2 panner11 = ( 1.0 * _Time.y * _Speed + uv0_TextureSample0);
			float4 tex2DNode2 = tex2D( _TextureSample0, panner11 );
			float3 ase_worldPos = i.worldPos;
			float temp_output_62_0 = sign( _OpacityInverter );
			float lerpResult65 = lerp( ( 1.0 - _Width ) , _Width , temp_output_62_0);
			float3 temp_output_5_0_g3 = ( ( ase_worldPos - _Location ) / ( _Radius + lerpResult65 ) );
			float dotResult8_g3 = dot( temp_output_5_0_g3 , temp_output_5_0_g3 );
			float clampResult10_g3 = clamp( dotResult8_g3 , 0.0 , 1.0 );
			float lerpResult47 = lerp( 1.0 , 0.0 , pow( clampResult10_g3 , _Hardness ));
			float4 temp_cast_1 = (lerpResult47).xxxx;
			float4 temp_output_49_0 = step( tex2DNode2 , temp_cast_1 );
			float4 temp_cast_2 = (lerpResult47).xxxx;
			float4 lerpResult63 = lerp( temp_output_49_0 , ( 1.0 - temp_output_49_0 ) , temp_output_62_0);
			o.Emission = ( lerpResult63 * _Color1 ).rgb;
			float3 temp_output_5_0_g1 = ( ( ase_worldPos - _Location ) / _Radius );
			float dotResult8_g1 = dot( temp_output_5_0_g1 , temp_output_5_0_g1 );
			float clampResult10_g1 = clamp( dotResult8_g1 , 0.0 , 1.0 );
			float lerpResult9 = lerp( 1.0 , 0.0 , pow( clampResult10_g1 , _Hardness ));
			float4 temp_cast_4 = (lerpResult9).xxxx;
			float3 temp_output_5_0_g4 = ( ( ase_worldPos - _Location ) / _SmallRadius );
			float dotResult8_g4 = dot( temp_output_5_0_g4 , temp_output_5_0_g4 );
			float clampResult10_g4 = clamp( dotResult8_g4 , 0.0 , 1.0 );
			float4 clampResult19 = clamp( ( step( tex2DNode2 , temp_cast_4 ) + ( 1.0 - pow( clampResult10_g4 , _SmallHardness ) ) ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 lerpResult59 = lerp( ( 1.0 - clampResult19 ) , clampResult19 , temp_output_62_0);
			o.Alpha = lerpResult59.r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows 

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
0;73.6;634;440;1696.446;-88.25711;1.749307;True;False
Node;AmplifyShaderEditor.RangedFloatNode;46;-1438.015,1555.267;Float;False;Property;_Width;Width;8;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-283.8164,337.8726;Half;False;Property;_OpacityInverter;OpacityInverter;11;0;Create;True;0;0;False;0;0;0;0;0.01;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;66;-1521.639,1705.378;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SignOpNode;62;5.961243,361.6657;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;65;-1194.639,1684.378;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1249.45,1055.621;Float;False;Property;_Radius;Radius;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-1088.808,54.34315;Float;False;0;2;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;6;-1262.45,900.6205;Float;False;Property;_Location;Location;1;0;Create;True;0;0;False;0;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector2Node;12;-1157.227,300.1258;Float;False;Property;_Speed;Speed;6;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;8;-1248.45,1135.621;Float;False;Property;_Hardness;Hardness;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1;-784.4725,905.6958;Float;False;SphereMask;-1;;1;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT3;0,0,0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1295.692,1286.963;Float;False;Property;_SmallRadius;SmallRadius;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1294.692,1366.963;Float;False;Property;_SmallHardness;SmallHardness;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;45;-1104.486,1514.597;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;11;-963.5322,248.0474;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;17;-830.7145,1137.038;Float;True;SphereMask;-1;;4;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT3;0,0,0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;44;-765.4039,1466.057;Float;False;SphereMask;-1;;3;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT3;0,0,0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;9;-444.0653,780.6406;Float;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-640.6746,371.9757;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;16d574e53541bba44a84052fa38778df;16d574e53541bba44a84052fa38778df;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;47;-303.1978,1389.806;Float;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;22;-435.7718,1052.164;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;10;-273.2522,470.846;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;49;-73.396,1341.998;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;-54.97094,868.1116;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;64;-195.0231,1623.202;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;19;112.5012,1056.018;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;54;-747.7334,-73.84517;Float;False;0;55;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;63;23.5502,1508.927;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;51;16.37313,1708.477;Float;False;Property;_Color1;Color 1;9;1;[HDR];Create;True;0;0;False;0;1,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;23;31.6659,572.4919;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;55;-494.5036,-99.38097;Float;True;Property;_MainTex;MainTex;10;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;20;-493.5649,159.1711;Float;False;Property;_Color0;Color 0;7;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-179.1519,112.1092;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;59;214.4097,328.8736;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;212.8109,1314.229;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;565.4998,27.29999;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;DistDis;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;66;0;46;0
WireConnection;62;0;60;0
WireConnection;65;0;66;0
WireConnection;65;1;46;0
WireConnection;65;2;62;0
WireConnection;1;15;6;0
WireConnection;1;14;7;0
WireConnection;1;12;8;0
WireConnection;45;0;7;0
WireConnection;45;1;65;0
WireConnection;11;0;3;0
WireConnection;11;2;12;0
WireConnection;17;15;6;0
WireConnection;17;14;15;0
WireConnection;17;12;16;0
WireConnection;44;15;6;0
WireConnection;44;14;45;0
WireConnection;44;12;8;0
WireConnection;9;2;1;0
WireConnection;2;1;11;0
WireConnection;47;2;44;0
WireConnection;22;0;17;0
WireConnection;10;0;2;0
WireConnection;10;1;9;0
WireConnection;49;0;2;0
WireConnection;49;1;47;0
WireConnection;18;0;10;0
WireConnection;18;1;22;0
WireConnection;64;0;49;0
WireConnection;19;0;18;0
WireConnection;63;0;49;0
WireConnection;63;1;64;0
WireConnection;63;2;62;0
WireConnection;23;0;19;0
WireConnection;55;1;54;0
WireConnection;57;0;55;0
WireConnection;57;1;20;0
WireConnection;59;0;23;0
WireConnection;59;1;19;0
WireConnection;59;2;62;0
WireConnection;50;0;63;0
WireConnection;50;1;51;0
WireConnection;0;0;57;0
WireConnection;0;2;50;0
WireConnection;0;9;59;0
ASEEND*/
//CHKSM=6FC4C4BDA44ED7DDDF4799F21B956E29277CBE53