// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Sundust"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_fresnelPower("fresnelPower", Float) = 0
		_sped("sped", Vector) = (0,0,0,0)
		_opacity("opacity", Float) = 1
		[HDR]_high("high", Color) = (0,0,0,0)
		[HDR]_low("low", Color) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float4 _low;
		uniform float4 _high;
		uniform sampler2D _TextureSample0;
		uniform float2 _sped;
		uniform sampler2D _Sampler018;
		uniform float4 _TextureSample0_ST;
		uniform float _fresnelPower;
		uniform float _opacity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float2 panner9 = ( 1.0 * _Time.y * _sped + (ase_worldPos*float3( _TextureSample0_ST.xy ,  0.0 ) + float3( _TextureSample0_ST.zw ,  0.0 )).xy);
			float4 tex2DNode1 = tex2D( _TextureSample0, panner9 );
			float4 lerpResult12 = lerp( _low , _high , tex2DNode1);
			o.Emission = lerpResult12.rgb;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV3 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode3 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV3, _fresnelPower ) );
			o.Alpha = ( ( tex2DNode1 * ( 1.0 - fresnelNode3 ) ) * _opacity ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16900
0;73.6;626;440;741.5494;309.9937;1;False;False
Node;AmplifyShaderEditor.TextureTransformNode;18;-890.5091,21.92103;Float;False;1;1;0;SAMPLER2D;_Sampler018;False;2;FLOAT2;0;FLOAT2;1
Node;AmplifyShaderEditor.WorldPosInputsNode;19;-867.8628,-182.1537;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;5;-825.6882,486.6074;Float;False;Property;_fresnelPower;fresnelPower;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;8;-948.1425,154.6165;Float;False;Property;_sped;sped;2;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ScaleAndOffsetNode;15;-623.4963,3.808453;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT4;1,0,0,0;False;2;FLOAT4;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FresnelNode;3;-608.0764,407.2772;Float;False;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;9;-545.7959,166.3721;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;7;-336.7664,465.8836;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-350.2463,-3.766362;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;523ca5674cbaa4248bc692293d461c7a;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-365.4413,355.2493;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-278.0569,426.167;Float;False;Property;_opacity;opacity;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;14;-297.0917,-205.8938;Float;False;Property;_high;high;4;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;13;-301.6253,-366.1564;Float;False;Property;_low;low;5;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-153.6626,331.124;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;12;-63.42683,-243.2155;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;193,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Sundust;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;19;0
WireConnection;15;1;18;0
WireConnection;15;2;18;1
WireConnection;3;3;5;0
WireConnection;9;0;15;0
WireConnection;9;2;8;0
WireConnection;7;0;3;0
WireConnection;1;1;9;0
WireConnection;6;0;1;0
WireConnection;6;1;7;0
WireConnection;10;0;6;0
WireConnection;10;1;11;0
WireConnection;12;0;13;0
WireConnection;12;1;14;0
WireConnection;12;2;1;0
WireConnection;0;2;12;0
WireConnection;0;9;10;0
ASEEND*/
//CHKSM=B0AB8D958F965B55295702DE51F0CDD1E3C28E9C