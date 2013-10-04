Shader "Custom/PlanetShader" {
	Properties {
		Position ("Position", Vector) = (0,0,0,0)
		WidthDir ("WidthDir", Vector) = (1,0,0,0)
		HeightDir ("HeightDir", Vector) = (0,1,0,0)
		Width ("Width", Float) = 1
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
	
			uniform float4 Position;
			uniform float4 WidthDir;
			uniform float4 HeightDir;
			uniform float Width;
			uniform float c;
			
			struct v2f{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float m;
			};			
			
			v2f vert(appdata_base v){
				v2f o;
			
				o.m = v.vertex.x * v.vertex.y + .15;
					
				//Square
				v.vertex = float4((Position.xyz + ( (WidthDir.xyz * v.vertex.x + HeightDir.xyz * v.vertex.y) * Width)).xyz, 1);
				
				//Circle
//				v.vertex = float4(normalize(Position.xyz + (WidthDir.xyz * v.vertex.x + HeightDir.xyz * v.vertex.y) * Width) * Width, 1);
				
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				
				return o;
			}
	
			half4 frag(v2f i) : COLOR{
				if(c == 1f)
					return half4(i.m,0,0,1);	
				if(c == 2f)
					return half4(0,i.m,0,1);	
				if(c == 3f)
					return half4(0,0,i.m,1);	
				if(c == 4f)
					return half4(0,0,0,1);	
					
			}		
			
			ENDCG	
		}
	} 
}
