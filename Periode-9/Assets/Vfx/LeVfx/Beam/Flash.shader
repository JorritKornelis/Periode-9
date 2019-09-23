// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Flash"
{
	Properties
	{
		_Waves1("Waves1", 2D) = "white" {}
		_Waves2("Waves2", 2D) = "white" {}
		[HDR]_Color("Color", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color;
		uniform sampler2D _Waves2;
		uniform float4 _Waves2_ST;
		uniform sampler2D _Waves1;
		uniform float4 _Waves1_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv0_Waves2 = i.uv_texcoord * _Waves2_ST.xy + _Waves2_ST.zw;
			float2 panner5 = ( 1.0 * _Time.y * float2( 0.66,0 ) + uv0_Waves2);
			float2 uv0_Waves1 = i.uv_texcoord * _Waves1_ST.xy + _Waves1_ST.zw;
			float2 panner6 = ( 1.0 * _Time.y * float2( -0.87,0 ) + uv0_Waves1);
			float4 smoothstepResult10 = smoothstep( float4( 0.5377358,0.5377358,0.5377358,0 ) , float4( 1,1,1,0 ) , ( tex2D( _Waves2, panner5, float2( 0,0 ), float2( 0,0 ) ) * tex2D( _Waves1, panner6, float2( 0,0 ), float2( 0,0 ) ) ));
			float4 temp_output_11_0 = ( _Color * smoothstepResult10 );
			o.Albedo = temp_output_11_0.rgb;
			o.Emission = temp_output_11_0.rgb;
			o.Alpha = smoothstepResult10.r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16900
0;73.6;654;440;746.2801;338.6855;1.614489;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-1364.816,345.2517;Float;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-1363.207,59.17006;Float;False;0;2;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;5;-1075.015,99.00809;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.66,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;6;-991.0143,390.208;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.87,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-800.4908,30.66315;Float;True;Property;_Waves2;Waves2;1;0;Create;True;0;0;False;0;947524cafbe50c34a893e2799e0f3eb3;947524cafbe50c34a893e2799e0f3eb3;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-799.0283,338.3813;Float;True;Property;_Waves1;Waves1;0;0;Create;True;0;0;False;0;f9889b9ce1ae6e74fa49cabae10ad1f5;f9889b9ce1ae6e74fa49cabae10ad1f5;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-365.9797,242.2626;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;10;-336.2589,-141.5142;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0.5377358,0.5377358,0.5377358,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;12;-316.7145,-364.2044;Float;False;Property;_Color;Color;2;1;[HDR];Create;True;0;0;False;0;0,0,0,0;42.22425,5.539254,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-40.79059,-188.5228;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Flash;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;7;0
WireConnection;6;0;8;0
WireConnection;2;1;5;0
WireConnection;1;1;6;0
WireConnection;9;0;2;0
WireConnection;9;1;1;0
WireConnection;10;0;9;0
WireConnection;11;0;12;0
WireConnection;11;1;10;0
WireConnection;0;0;11;0
WireConnection;0;2;11;0
WireConnection;0;9;10;0
ASEEND*/
//CHKSM=948209959109CF0AA7BD99BE36C83CEF60103F33