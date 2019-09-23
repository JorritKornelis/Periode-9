// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "A"
{
	Properties
	{
		[HDR]_Color0("Color 0", Color) = (1.951708,0.966648,1.925618,0)
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_sped2("sped 2", Vector) = (0,0,0,0)
		_IntersectIntensity("Intersect Intensity", Range( 0 , 1)) = 0.2
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityCG.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float4 screenPosition21;
			float2 uv_texcoord;
		};

		uniform float4 _Color0;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _IntersectIntensity;
		uniform sampler2D _TextureSample1;
		uniform float4 _TextureSample1_ST;
		uniform sampler2D _TextureSample2;
		uniform float2 _sped2;
		uniform float4 _TextureSample2_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 vertexPos21 = ase_vertex3Pos;
			float4 ase_screenPos21 = ComputeScreenPos( UnityObjectToClipPos( vertexPos21 ) );
			o.screenPosition21 = ase_screenPos21;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color23 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
			float4 ase_screenPos21 = i.screenPosition21;
			float4 ase_screenPosNorm21 = ase_screenPos21 / ase_screenPos21.w;
			ase_screenPosNorm21.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm21.z : ase_screenPosNorm21.z * 0.5 + 0.5;
			float screenDepth21 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture,UNITY_PROJ_COORD( ase_screenPos21 )));
			float distanceDepth21 = abs( ( screenDepth21 - LinearEyeDepth( ase_screenPosNorm21.z ) ) / ( _IntersectIntensity ) );
			float clampResult20 = clamp( distanceDepth21 , 0.0 , 1.0 );
			float4 lerpResult22 = lerp( color23 , _Color0 , clampResult20);
			o.Emission = lerpResult22.rgb;
			float2 uv_TextureSample1 = i.uv_texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			float4 tex2DNode7 = tex2D( _TextureSample1, uv_TextureSample1 );
			float2 uv0_TextureSample2 = i.uv_texcoord * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
			float2 panner16 = ( 1.0 * _Time.y * _sped2 + uv0_TextureSample2);
			float4 tex2DNode15 = tex2D( _TextureSample2, panner16 );
			float4 smoothstepResult26 = smoothstep( float4( 0,0,0,0 ) , float4( 1,1,1,0 ) , ( tex2DNode7 * ( tex2DNode7 + tex2DNode15 ) ));
			o.Alpha = smoothstepResult26.r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16900
0;73.6;717;440;1427.762;164.0947;2.219279;True;False
Node;AmplifyShaderEditor.Vector2Node;17;-946.3904,-457.6076;Float;False;Property;_sped2;sped 2;5;0;Create;True;0;0;False;0;0,0;0,0.45;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;11;-1036.179,-631.7146;Float;False;0;15;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;16;-779.9971,-582.4026;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-575.4741,729.6337;Float;False;Property;_IntersectIntensity;Intersect Intensity;6;0;Create;True;0;0;False;0;0.2;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;24;-470.5354,847.9551;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;15;-539.0199,-700.0671;Float;True;Property;_TextureSample2;Texture Sample 2;4;0;Create;True;0;0;False;0;78d846492899ac947a144c85c0c94788;78d846492899ac947a144c85c0c94788;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-767.1387,-319.0902;Float;True;Property;_TextureSample1;Texture Sample 1;3;0;Create;True;0;0;False;0;6928f67e1a7630b4d98302001c53bb76;6928f67e1a7630b4d98302001c53bb76;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DepthFade;21;-258.7339,742.9266;Float;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;27;-483.2468,-356.6944;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;2;-208.9272,-226.4309;Float;False;Property;_Color0;Color 0;2;1;[HDR];Create;True;0;0;False;0;1.951708,0.966648,1.925618,0;0,5.992157,0.1254902,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;23;-466.2164,511.4128;Float;False;Constant;_Color1;Color 1;7;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;20;6.819092,694.9546;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-299.0381,-434.3356;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-32.46384,-462.6696;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-356.348,303.4976;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-382.9395,-18.52212;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;9789d23040cb1fb45ad60392430c3c15;78d846492899ac947a144c85c0c94788;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-834.0151,-92.05394;Float;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;3;-583.2986,90.56111;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;2,-0.8;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;22;-117.7864,452.8116;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-780.0483,427.0656;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector2Node;5;-806.7744,127.7163;Float;False;Property;_sped;sped;1;0;Create;True;0;0;False;0;0,0;0,0.79;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SmoothstepOpNode;26;-361.7025,409.1106;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;A;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;11;0
WireConnection;16;2;17;0
WireConnection;15;1;16;0
WireConnection;21;1;24;0
WireConnection;21;0;19;0
WireConnection;27;0;7;0
WireConnection;27;1;15;0
WireConnection;20;0;21;0
WireConnection;28;0;7;0
WireConnection;28;1;27;0
WireConnection;14;0;15;0
WireConnection;14;1;7;0
WireConnection;8;0;1;0
WireConnection;8;1;14;0
WireConnection;1;1;3;0
WireConnection;3;0;4;0
WireConnection;3;2;5;0
WireConnection;22;0;23;0
WireConnection;22;1;2;0
WireConnection;22;2;20;0
WireConnection;18;0;8;0
WireConnection;18;1;14;0
WireConnection;26;0;28;0
WireConnection;0;2;22;0
WireConnection;0;9;26;0
ASEEND*/
//CHKSM=2EE9486ABB37D6BA7F79E4E7244913D25166E8B8