Shader "Custom/LightRipple" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		Cull Off

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float3 _ContactPosition[3];
		float2 _RippleDistance[3];

		// returns 0 if the objectPosition is less than rippleDistance away from contactPosition, -1 otherwise
		int insideRipple(float3 objectPosition, float3 contactPosition, float rippleDistance) {
			float d = distance(contactPosition, objectPosition);
			if (d > rippleDistance) {
				return -1;
			}
			return 0;
		}

		void ripple(float3 worldPos) {
			float3 objectPos = mul(_World2Object, float4(worldPos, 1));

			for (int i = 0; i < 3; i++) {
				if (insideRipple(objectPos, _ContactPosition[i], _RippleDistance[i].x) == 0) {
					return;
				}
			}
			clip(-1);
		}

		void surf(Input IN, inout SurfaceOutputStandard o) {
			ripple(IN.worldPos);
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

