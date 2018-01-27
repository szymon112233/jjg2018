Shader "Custom/Water Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Amplitude ("Amplitude", Float) = 1.5
		_Frequency ("Frequency", Float) = 0.75
		_OffsetMultiplier ("OffsetMultiplier", Float) = 0.2
		_TexScale ("_TexScale", Float) = 1.0
    }
    SubShader
    {
        Pass
        {
            // indicate that our pass is the "base" pass in forward
            // rendering pipeline. It gets ambient and main directional
            // light data set up; light direction in _WorldSpaceLightPos0
            // and color in _LightColor0
            Tags {"LightMode"="ForwardBase"}
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc" // for UnityObjectToWorldNormal
            #include "UnityLightingCommon.cginc" // for _LightColor0

            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed4 diff : COLOR0; // diffuse lighting color
                float4 vertex : SV_POSITION;
            };

			float _Amplitude;
			float _Frequency;
			float _OffsetMultiplier;
			float _TexScale;

            v2f vert (appdata_base v)
            {
                v2f o;
				float4 worldspace = UnityObjectToClipPos(v.vertex);
				o.vertex = worldspace;
                o.vertex.y += _Amplitude * sin(6.28 * _Time.y * _Frequency + (worldspace.x * worldspace.z) * _OffsetMultiplier);
                o.uv = v.texcoord;
                // get vertex normal in world space
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                // dot product between normal and light direction for
                // standard diffuse (Lambert) lighting
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                // factor in the light color
                o.diff = nl * _LightColor0;
                return o;
            }
            
            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                // sample texture
                fixed4 col = tex2D(_MainTex, i.uv * _TexScale);
                // multiply by lighting
                col *= i.diff;
                return col;
            }
            ENDCG
        }
    }
}