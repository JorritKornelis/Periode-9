// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Firefly"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.6
		_FireFlyAlpha("FireFlyAlpha", 2D) = "white" {}
		[HDR]_Color("Color", Color) = (0,0,0,0)
		_FireFlyGradient("FireFlyGradient", 2D) = "white" {}
		_He("He", Float) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _tex4coord2( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow vertex:vertexDataFunc 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
			float4 uv2_tex4coord2;
		};

		uniform float _He;
		uniform sampler2D _FireFlyGradient;
		uniform float4 _Color;
		uniform sampler2D _TextureSample0;
		uniform sampler2D _FireFlyAlpha;
		uniform float _Cutoff = 0.6;


		float3x3 CotangentFrame( float3 normal , float3 position , float2 uv )
		{
			float3 dp1 = ddx ( position );
			float3 dp2 = ddy ( position );
			float2 duv1 = ddx ( uv );
			float2 duv2 = ddy ( uv );
			float3 dp2perp = cross ( dp2, normal );
			float3 dp1perp = cross ( normal, dp1 );
			float3 tangent = dp2perp * duv1.x + dp1perp * duv2.x;
			float3 bitangent = dp2perp * duv1.y + dp1perp * duv2.y;
			float invmax = rsqrt ( max ( dot ( tangent, tangent ), dot ( bitangent, bitangent ) ) );
			tangent *= invmax;
			bitangent *= invmax;
			return float3x3 (	tangent.x, bitangent.x, normal.x,
								tangent.y, bitangent.y, normal.y,
								tangent.z, bitangent.z, normal.z );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldNormal = UnityObjectToWorldNormal( v.normal );
			float3 ase_normWorldNormal = normalize( ase_worldNormal );
			float3 normal3_g7 = ase_normWorldNormal;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 position3_g7 = ase_worldViewDir;
			float2 uv3_g7 = v.texcoord.xy;
			float3x3 localCotangentFrame3_g7 = CotangentFrame( normal3_g7 , position3_g7 , uv3_g7 );
			float3 temp_output_6_0_g1 = float3( 0,0,0 );
			float3 temp_output_24_0_g1 = mul( localCotangentFrame3_g7, temp_output_6_0_g1 );
			float3 break27_g1 = temp_output_24_0_g1;
			float mulTime22 = _Time.y * v.texcoord.w;
			float4 appendResult11 = (float4(0.0 , (( break27_g1.y + ( (( _He * -1.0 ) + (sin( ( mulTime22 + v.texcoord.z ) ) - -1.0) * (_He - ( _He * -1.0 )) / (1.0 - -1.0)) * tex2Dlod( _FireFlyGradient, float4( v.texcoord.xy, 0, 0.0) ) ) )).r , 0.0 , 0.0));
			v.vertex.xyz += appendResult11.xyz;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Emission = _Color.rgb;
			o.Alpha = 1;
			float4 temp_cast_1 = (i.uv2_tex4coord2.x).xxxx;
			clip( ( step( tex2D( _TextureSample0, i.uv_texcoord ) , temp_cast_1 ) * tex2D( _FireFlyAlpha, i.uv_texcoord ) ).r - _Cutoff );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17101
0;73.6;626;419;1411.569;753.7974;1.606001;True;False
Node;AmplifyShaderEditor.TexCoordVertexDataNode;33;-1794.02,615.3588;Inherit;False;0;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;22;-1537.58,818.6208;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-1342.851,660.24;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-1437.623,1064.524;Float;False;Property;_He;He;4;0;Create;True;0;0;False;0;0;0.48;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;23;-1309.169,839.3854;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-1278.335,965.1379;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;30;-1334.553,1169.244;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-1063.935,1180.496;Inherit;True;Property;_FireFlyGradient;FireFlyGradient;3;0;Create;True;0;0;False;0;c427ab5e61b9f814399dc56deb8e21b5;c427ab5e61b9f814399dc56deb8e21b5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;18;-1122.242,904.2717;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-852.8591,1028.031;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;47;-1401.648,-510.1337;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;25;-963.9263,449.9738;Inherit;False;PerturbNormal;-1;;1;c8b64dd82fb09f542943a895dffb6c06;1,26,0;1;6;FLOAT3;0,0,0;False;4;FLOAT3;9;FLOAT;28;FLOAT;29;FLOAT;30
Node;AmplifyShaderEditor.SamplerNode;46;-1099.227,-558.84;Inherit;True;Property;_TextureSample0;Texture Sample 0;6;0;Create;True;0;0;False;0;78d846492899ac947a144c85c0c94788;523ca5674cbaa4248bc692293d461c7a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-1110.006,-49.04335;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-811.4739,839.8461;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;43;-1056.377,-298.6763;Inherit;True;1;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-865.7088,-75.94305;Inherit;True;Property;_FireFlyAlpha;FireFlyAlpha;1;0;Create;True;0;0;False;0;7839f2733db18514d8313bffa2fcce57;21411490d53d8ed4999f102fa3be5dae;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;48;-693.991,-513.4409;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SwizzleNode;28;-279.2068,457.0585;Inherit;True;FLOAT;0;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;3;-212.4906,41.78521;Float;False;Property;_Color;Color;2;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0.05719685,0.0154624,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-1733.853,843.8599;Float;False;Property;_TimeSpeed;TimeSpeed;5;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;11;-102.382,484.5681;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-464.8295,-203.9034;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;ASEMaterialInspector;0;0;Standard;Firefly;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.6;True;False;0;True;TransparentCutout;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;22;0;33;4
WireConnection;31;0;22;0
WireConnection;31;1;33;3
WireConnection;23;0;31;0
WireConnection;19;0;10;0
WireConnection;4;1;30;0
WireConnection;18;0;23;0
WireConnection;18;3;19;0
WireConnection;18;4;10;0
WireConnection;12;0;18;0
WireConnection;12;1;4;0
WireConnection;46;1;47;0
WireConnection;8;0;25;29
WireConnection;8;1;12;0
WireConnection;1;1;2;0
WireConnection;48;0;46;0
WireConnection;48;1;43;1
WireConnection;28;0;8;0
WireConnection;11;1;28;0
WireConnection;45;0;48;0
WireConnection;45;1;1;0
WireConnection;0;2;3;0
WireConnection;0;10;45;0
WireConnection;0;11;11;0
ASEEND*/
//CHKSM=54DD3CAA528787B5A083946DA6BC33B7F26589A1