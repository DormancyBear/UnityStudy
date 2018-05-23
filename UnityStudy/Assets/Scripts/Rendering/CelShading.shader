Shader "Custom/CelShading" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf CelShading

			#pragma target 3.0

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
			};

			fixed4 _Color;

			void surf(Input IN, inout SurfaceOutput o) {
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}

			half4 LightingCelShading(SurfaceOutput s, half3 lightDir, half atten) {
				half NdotL = dot(s.Normal, lightDir);
				NdotL = 1 + clamp(floor(NdotL), -1, 0);   //把NdotL设置为0（小于0）或设置为1（大于0）
				half4 c;
				c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten * 2);
				c.a = s.Alpha;
				return c;
			}
			ENDCG
	}
		FallBack "Diffuse"
}
