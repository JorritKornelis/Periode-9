// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Slash"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_slashtexture("slashtexture", 2D) = "white" {}
		[HDR]_ColorHigh("ColorHigh", Color) = (0,0,0,0)
		[HDR]_ColorLow("ColorLow", Color) = (0,0,0,0)
		_Lifter("Lifter", Float) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Corrision("Corrision", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _ColorLow;
		uniform float4 _ColorHigh;
		uniform sampler2D _slashtexture;
		uniform float4 _slashtexture_ST;
		uniform float _Lifter;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _Corrision;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv0_slashtexture = i.uv_texcoord * _slashtexture_ST.xy + _slashtexture_ST.zw;
			float4 tex2DNode1 = tex2D( _slashtexture, uv0_slashtexture, float2( 0,0 ), float2( 0,0 ) );
			float4 clampResult17 = clamp( ( tex2DNode1 + _Lifter ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 lerpResult5 = lerp( _ColorLow , _ColorHigh , clampResult17);
			o.Emission = lerpResult5.rgb;
			o.Alpha = 1;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			clip( ( tex2DNode1 - ( tex2D( _TextureSample0, uv_TextureSample0 ) * _Corrision ) ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16900
0;73.6;688;394;963.793;345.4778;1.792303;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-651.953,205.4017;Float;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-327.0127,121.1452;Float;False;Property;_Lifter;Lifter;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-338.5113,201.7637;Float;True;Property;_slashtexture;slashtexture;1;0;Create;True;0;0;False;0;65bfea28da76e434a866ae1babecdc12;65bfea28da76e434a866ae1babecdc12;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;13;-901.8346,-190.3109;Float;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;False;0;523ca5674cbaa4248bc692293d461c7a;523ca5674cbaa4248bc692293d461c7a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-803.0346,117.4891;Float;False;Property;_Corrision;Corrision;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-178.8277,100.9213;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;4;-414.5861,-63.88089;Float;False;Property;_ColorHigh;ColorHigh;2;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;3;-409.2068,-257.5374;Float;False;Property;_ColorLow;ColorLow;3;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-592.1346,26.28913;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;17;-175.1795,50.62122;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;16;-292.0592,438.6884;Float;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;5;-146.4615,-145.511;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Slash;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1;1;12;0
WireConnection;6;0;1;0
WireConnection;6;1;7;0
WireConnection;14;0;13;0
WireConnection;14;1;15;0
WireConnection;17;0;6;0
WireConnection;16;0;1;0
WireConnection;16;1;14;0
WireConnection;5;0;3;0
WireConnection;5;1;4;0
WireConnection;5;2;17;0
WireConnection;0;2;5;0
WireConnection;0;10;16;0
ASEEND*/
//CHKSM=82D071D6FF8ECB19A23B448AA37632C8BF3FEB7B