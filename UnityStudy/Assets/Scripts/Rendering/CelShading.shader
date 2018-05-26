Shader "Custom/CelShading" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_OutlineThickness("OutlineThickness", Range(0, 0.05)) = 0.01
		_OutlineColor("OutlineColor", Color) = (0, 0, 0, 1)
	}
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		// 先渲染外边轮廓
		Cull Front
		CGPROGRAM

		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
		fixed4 _Color;
		float _OutlineThickness;
		fixed4 _OutlineColor;

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = _OutlineColor;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		void vert(inout appdata_full v)
		{
			v.vertex.xyz += v.normal * _OutlineThickness;
		}
		ENDCG

		// 后渲染实际模型
		Cull Back
		CGPROGRAM
		//#pragma surface surf CustomLambert
		#pragma surface surf CelShading

		#pragma target 3.0

		fixed4 _Color;
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		half4 LightingCelShading(SurfaceOutput s, half3 lightDir, half atten)
		{
			half NdotL = dot(s.Normal, lightDir);
			if (NdotL > 0.9)
			{
				NdotL = 1.0;
			}
			else if (NdotL > 0.5)
			{
				NdotL = 0.6;
			}
			else
			{
				NdotL = 0;
			}

			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten);
			c.a = s.Alpha;
			return c;
		}

		half4 LightingCustomLambert(SurfaceOutput s, half3 lightDir, half atten)
		{
			half NdotL = dot(s.Normal, lightDir);
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten);
			c.a = s.Alpha;
			return c;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
