Shader "Custom/PlanetShader" {
	Properties {
		_Position ("Position", Vector) = (0,0,0,0)
		_WidthDir ("WidthDir", Vector) = (1,0,0,0)
		_HeightDir ("HeightDir", Vector) = (0,1,0,0)
		_Width ("Width", Float) = 1
	}
	SubShader {
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			uniform float4 _Position;
			uniform float4 _WidthDir;
			uniform float4 _HeightDir;
			uniform float _Width;
			
			struct v2f{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};			
			
			v2f vert(appdata_base v){
				v2f o;
				float3 w = float3(0,1,0);
				float3 h = float3(0,0,1);
				v.vertex = float4((_Position.xyz + ( (_WidthDir.xyz * v.vertex.x + _HeightDir.xyz * v.vertex.y) * _Width)).xyz, 1);
//				v.vertex = float4((_Position.xyz + ( (w * v.vertex.x + h * v.vertex.y) * _Width * 40)).xyz, 1);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
	
			half4 frag(v2f i) : COLOR{
				return half4(.1,.1,1,.9);	
			}		
			
			ENDCG	
		}
	} 
}
