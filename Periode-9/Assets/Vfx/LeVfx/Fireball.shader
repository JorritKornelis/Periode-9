// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Fireball"
{
	Properties
	{
		_Noisetexture("Noise texture", 2D) = "white" {}
		_Texture0("Texture 0", 2D) = "white" {}
		[HDR]_Color("Color", Color) = (11.98431,5.270588,0,0)
		_Max("Max", Float) = -0.8
		_Min("Min", Float) = 1.13
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+830" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color;
		uniform float _Min;
		uniform float _Max;
		uniform sampler2D _Texture0;
		uniform float4 _Texture0_ST;
		uniform sampler2D _Noisetexture;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float temp_output_18_0 = 0.0;
			float3 temp_cast_0 = (temp_output_18_0).xxx;
			o.Albedo = temp_cast_0;
			float4 temp_cast_1 = (_Min).xxxx;
			float4 temp_cast_2 = (_Max).xxxx;
			float2 uv_Texture0 = i.uv_texcoord * _Texture0_ST.xy + _Texture0_ST.zw;
			float4 tex2DNode26 = tex2D( _Texture0, uv_Texture0 );
			float4 smoothstepResult27 = smoothstep( temp_cast_1 , temp_cast_2 , tex2DNode26);
			float2 uv_TexCoord6 = i.uv_texcoord * float2( 2.28,1 );
			float2 panner5 = ( 1.0 * _Time.y * float2( 0,-1.32 ) + uv_TexCoord6);
			float4 tex2DNode2 = tex2D( _Noisetexture, panner5 );
			float2 uv_TexCoord10 = i.uv_texcoord * float2( 1.72,0.6 ) + float2( 0,1.38 );
			float2 panner9 = ( 1.0 * _Time.y * float2( 0,-1.27 ) + uv_TexCoord10);
			float4 tex2DNode7 = tex2D( _Noisetexture, panner9 );
			float4 temp_output_25_0 = ( smoothstepResult27 * ( ( tex2DNode2 - tex2DNode7 ) - ( tex2DNode26 * tex2DNode2 * tex2DNode7 ) ) );
			o.Emission = ( _Color * temp_output_25_0 ).rgb;
			o.Metallic = temp_output_18_0;
			o.Smoothness = temp_output_18_0;
			o.Occlusion = temp_output_18_0;
			o.Alpha = temp_output_25_0.r;
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
0;73.6;579;438;1341.871;550.3068;2.064396;True;False
Node;AmplifyShaderEditor.TexturePropertyNode;1;1.071905,-200.2904;Float;True;Property;_Noisetexture;Noise texture;0;0;Create;True;0;0;False;0;78d846492899ac947a144c85c0c94788;78d846492899ac947a144c85c0c94788;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;3;244.0524,-155.0732;Float;False;Noise;-1;True;1;0;SAMPLER2D;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-1625.224,-1.878094;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2.28,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-1633.639,293.4917;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1.72,0.6;False;1;FLOAT2;0,1.38;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;4;-1387.13,-138.9461;Float;False;3;Noise;1;0;OBJECT;0;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.GetLocalVarNode;8;-1395.545,156.4239;Float;False;3;Noise;1;0;OBJECT;0;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TexturePropertyNode;19;-1334.781,-375.1837;Float;True;Property;_Texture0;Texture 0;1;0;Create;True;0;0;False;0;6928f67e1a7630b4d98302001c53bb76;6928f67e1a7630b4d98302001c53bb76;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;5;-1366.524,-3.178143;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1.32;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;9;-1377.538,292.1917;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1.27;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-1163.825,-137.6564;Float;True;Property;_TextureSample0;_TextureSample0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;26;-1130.922,-585.0565;Float;True;Property;_TextureSample2;Texture Sample 2;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-1172.24,157.7135;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-832.0035,232.2771;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-813.9453,-419.3472;Float;False;Property;_Max;Max;3;0;Create;True;0;0;False;0;-0.8;-1.07;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-818.168,-495.8574;Float;False;Property;_Min;Min;4;0;Create;True;0;0;False;0;1.13;0.62;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;30;-805.0425,85.76717;Float;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;27;-632.6639,-578.3374;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0.8490566,0.8490566,0.8490566,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;32;-679.1688,126.1399;Float;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-467.4894,-256.024;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;14;-779.5159,-112.7942;Float;False;Property;_Color;Color;2;1;[HDR];Create;True;0;0;False;0;11.98431,5.270588,0,0;73.51669,8.937323,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-186.688,118.791;Float;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-475.324,71.30649;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Fireball;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;830;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;1;0
WireConnection;5;0;6;0
WireConnection;9;0;10;0
WireConnection;2;0;4;0
WireConnection;2;1;5;0
WireConnection;26;0;19;0
WireConnection;7;0;8;0
WireConnection;7;1;9;0
WireConnection;31;0;26;0
WireConnection;31;1;2;0
WireConnection;31;2;7;0
WireConnection;30;0;2;0
WireConnection;30;1;7;0
WireConnection;27;0;26;0
WireConnection;27;1;28;0
WireConnection;27;2;29;0
WireConnection;32;0;30;0
WireConnection;32;1;31;0
WireConnection;25;0;27;0
WireConnection;25;1;32;0
WireConnection;15;0;14;0
WireConnection;15;1;25;0
WireConnection;0;0;18;0
WireConnection;0;2;15;0
WireConnection;0;3;18;0
WireConnection;0;4;18;0
WireConnection;0;5;18;0
WireConnection;0;9;25;0
ASEEND*/
//CHKSM=36AC7ED319D9C9C25DF7C33CAA82C452A9EC3E13