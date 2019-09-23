// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "A"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Waves1("Waves1", 2D) = "white" {}
		_Waves2("Waves2", 2D) = "white" {}
		_Color0("Color 0", Color) = (0.6981132,0.5437266,0.2272161,0)
		_sped("sped", Vector) = (0,0,0,0)
		_spedder("spedder", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color0;
		uniform sampler2D _Waves1;
		uniform float2 _sped;
		uniform float4 _Waves1_ST;
		uniform sampler2D _Waves2;
		uniform float2 _spedder;
		uniform float4 _Waves2_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 temp_output_45_0 = _Color0;
			o.Albedo = temp_output_45_0.rgb;
			o.Emission = temp_output_45_0.rgb;
			o.Alpha = 1;
			float2 uv0_Waves1 = i.uv_texcoord * _Waves1_ST.xy + _Waves1_ST.zw;
			float2 panner32 = ( 1.0 * _Time.y * _sped + uv0_Waves1);
			float2 uv0_Waves2 = i.uv_texcoord * _Waves2_ST.xy + _Waves2_ST.zw;
			float2 panner33 = ( 1.0 * _Time.y * _spedder + uv0_Waves2);
			clip( ( tex2D( _Waves1, panner32, float2( 0,0 ), float2( 0,0 ) ) * tex2D( _Waves2, panner33, float2( 0,0 ), float2( 0,0 ) ) ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16900
0;73.6;688;394;997.4328;-66.39443;1.920554;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;34;-1006.219,277.0678;Float;False;0;29;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;46;-912.5574,419.7803;Float;False;Property;_sped;sped;4;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;47;-805.5924,685.2117;Float;False;Property;_spedder;spedder;5;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;35;-965.5366,526.7111;Float;False;0;30;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;32;-708.4964,351.0362;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;33;-721.4408,558.1477;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;29;-510.0145,304.4455;Float;True;Property;_Waves1;Waves1;1;0;Create;True;0;0;False;0;f9889b9ce1ae6e74fa49cabae10ad1f5;f9889b9ce1ae6e74fa49cabae10ad1f5;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;30;-503.8691,527.7255;Float;True;Property;_Waves2;Waves2;2;0;Create;True;0;0;False;0;947524cafbe50c34a893e2799e0f3eb3;947524cafbe50c34a893e2799e0f3eb3;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-142.6391,506.3696;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;45;-216.6543,-197.5313;Float;False;Property;_Color0;Color 0;3;0;Create;True;0;0;False;0;0.6981132,0.5437266,0.2272161,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;A;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;False;TransparentCutout;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;32;0;34;0
WireConnection;32;2;46;0
WireConnection;33;0;35;0
WireConnection;33;2;47;0
WireConnection;29;1;32;0
WireConnection;30;1;33;0
WireConnection;31;0;29;0
WireConnection;31;1;30;0
WireConnection;0;0;45;0
WireConnection;0;2;45;0
WireConnection;0;10;31;0
ASEEND*/
//CHKSM=60A6DC8E3C139C52FEB47F0CC0F334856C5F155C