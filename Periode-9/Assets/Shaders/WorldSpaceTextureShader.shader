// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "WorldSpaceTexture"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Metalllic("Metalllic", 2D) = "white" {}
		_Smoothness("Smoothness", 2D) = "white" {}
		_NormalOffset("Normal Offset", Float) = 0
		_NormalStrength("Normal Strength", Float) = 0
		[Normal]_NormalMap("NormalMap", 2D) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
		};

		uniform sampler2D _NormalMap;
		uniform sampler2D _Sampler028;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _NormalOffset;
		uniform float _NormalStrength;
		uniform sampler2D _Metalllic;
		uniform sampler2D _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 appendResult33 = (float3(ase_worldPos.x , ase_worldPos.z , 1.0));
			float2 temp_output_30_0 = (_Albedo_ST.zw*_Albedo_ST.xy + appendResult33.xy);
			float4 appendResult45 = (float4((temp_output_30_0*_Albedo_ST.xy + 0.0) , temp_output_30_0));
			float2 temp_output_2_0_g1 = appendResult45.xy;
			float2 break6_g1 = temp_output_2_0_g1;
			float temp_output_25_0_g1 = ( pow( _NormalOffset , 3.0 ) * 0.1 );
			float2 appendResult8_g1 = (float2(( break6_g1.x + temp_output_25_0_g1 ) , break6_g1.y));
			float4 tex2DNode14_g1 = tex2D( _NormalMap, temp_output_2_0_g1 );
			float temp_output_4_0_g1 = _NormalStrength;
			float3 appendResult13_g1 = (float3(1.0 , 0.0 , ( ( tex2D( _NormalMap, appendResult8_g1 ).g - tex2DNode14_g1.g ) * temp_output_4_0_g1 )));
			float2 appendResult9_g1 = (float2(break6_g1.x , ( break6_g1.y + temp_output_25_0_g1 )));
			float3 appendResult16_g1 = (float3(0.0 , 1.0 , ( ( tex2D( _NormalMap, appendResult9_g1 ).g - tex2DNode14_g1.g ) * temp_output_4_0_g1 )));
			float3 normalizeResult22_g1 = normalize( cross( appendResult13_g1 , appendResult16_g1 ) );
			o.Normal = normalizeResult22_g1;
			o.Albedo = tex2D( _Albedo, appendResult45.xy ).rgb;
			o.Metallic = tex2D( _Metalllic, appendResult45.xy ).r;
			o.Smoothness = tex2D( _Smoothness, appendResult45.xy ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16900
0;73.6;579;438;1139.898;362.8452;2.726781;False;False
Node;AmplifyShaderEditor.CommentaryNode;47;-1387.345,266.595;Float;False;395.67;223.6645;Position in world;2;33;12;;1,0.8411184,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;46;-1187.938,-152.3658;Float;False;523.1047;319.2514;Get Offset;3;41;30;28;;1,0,0,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;12;-1360.238,313.2599;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureTransformNode;28;-1137.938,-102.3658;Float;False;27;1;0;SAMPLER2D;_Sampler028;False;2;FLOAT2;0;FLOAT2;1
Node;AmplifyShaderEditor.DynamicAppendNode;33;-1157.275,316.595;Float;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;30;-1125.977,3.640033;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;41;-887.6063,-0.4143953;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-292.5842,260.0016;Float;False;Property;_NormalStrength;Normal Strength;6;0;Create;True;0;0;False;0;0;13.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;48;-546.1233,-113.9519;Float;False;366.5484;915.2399;Color;4;52;53;27;54;;0.1751578,1,0,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;57;-808.9206,378.2626;Float;True;Property;_NormalMap;NormalMap;7;1;[Normal];Create;True;0;0;False;0;None;7ddcba51d9fc0894d98b4ba77fbdfbd7;True;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.DynamicAppendNode;45;-743.7996,222.7727;Float;False;FLOAT4;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT2;0,0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-304.0318,140.4526;Float;False;Property;_NormalOffset;Normal Offset;5;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;53;-491.1745,573.8609;Float;True;Property;_Smoothness;Smoothness;4;0;Create;True;0;0;False;0;None;6618005f6bafebf40b3d09f498401fba;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;52;-494.7141,355.5046;Float;True;Property;_Metalllic;Metalllic;3;0;Create;True;0;0;False;0;None;6618005f6bafebf40b3d09f498401fba;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;27;-496.1234,-63.9519;Float;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;84508b93f15f2b64386ec07486afc7a3;00d034bb5072d8043a98b8a4aae5a40d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;54;-523.8335,174.8464;Float;False;NormalCreate;1;;1;e12f7ae19d416b942820e3932b56220f;0;4;1;SAMPLER2D;;False;2;FLOAT2;0,0;False;3;FLOAT;0.5;False;4;FLOAT;2;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;49;97.09161,-72.16449;Float;False;317.2;498.6;lol;1;0;;1,0.5518868,0.9514436,1;0;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;147.0916,-22.16449;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;WorldSpaceTexture;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;33;0;12;1
WireConnection;33;1;12;3
WireConnection;30;0;28;1
WireConnection;30;1;28;0
WireConnection;30;2;33;0
WireConnection;41;0;30;0
WireConnection;41;1;28;0
WireConnection;45;0;41;0
WireConnection;45;2;30;0
WireConnection;53;1;45;0
WireConnection;52;1;45;0
WireConnection;27;1;45;0
WireConnection;54;1;57;0
WireConnection;54;2;45;0
WireConnection;54;3;55;0
WireConnection;54;4;56;0
WireConnection;0;0;27;0
WireConnection;0;1;54;0
WireConnection;0;3;52;0
WireConnection;0;4;53;0
ASEEND*/
//CHKSM=757D34D3BF8ADE0943805686BB60C5945005D20B