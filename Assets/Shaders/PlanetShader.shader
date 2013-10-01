Shader "Custom/PlanetShader" {
	Properties {
		_Position ("Position", Vector) = (0,0,0,0)
		_WidthDir ("WidthDir", Vector) = (1,0,0,0)
		_HeightDir ("HeightDir", Vector) = (0,1,0,0)
		_Width ("Width", Float) = 1
		c ("debug", Float) = 1
	}
	SubShader {
		Pass{
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members grad)
#pragma exclude_renderers d3d11 xbox360
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			uniform float4 _Position;
			uniform float4 _WidthDir;
			uniform float4 _HeightDir;
			uniform float _Width;
			uniform float c;
			
			struct v2f{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};			
			
			v2f vert(appdata_base v){
				v2f o;
				
				//Square
				v.vertex = float4((_Position.xyz + ( (_WidthDir.xyz * v.vertex.x + _HeightDir.xyz * v.vertex.y) * _Width)).xyz, 1);
				
				//Circle
//				v.vertex = float4(normalize(_Position.xyz + (_WidthDir.xyz * v.vertex.x + _HeightDir.xyz * v.vertex.y) * _Width) * _Width, 1);
				
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
	
			half4 frag(v2f i) : COLOR{
				if(c == 1f)
					return half4(1,0,0,1);	
				if(c == 2f)
					return half4(0,1,0,1);	
				if(c == 3f)
					return half4(0,0,1,1);	
				if(c == 4f)
					return half4(0,0,0,1);	
					
			}		
			
			ENDCG	
		}
	} 
}
