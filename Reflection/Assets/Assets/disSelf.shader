   Properties {

        _Color ("Main Color", Color) = (1,1,1,1)

        _EdgeColor ("Edge Color", Color) = (1,0,0)

        _EdgeWidth ("Edge Width", Range(0,1)) = 0.1

        _MainTex ("Base (RGB)", 2D) = "white" {}

        _DissolveTex ("Dissolve (R)", 2D) = "white" {}

        _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)

        _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0

    }

     

    SubShader {

        Tags {"IgnoreProjector"="True" "RenderType"="TransparentCutout"}

        LOD 300

     

    CGPROGRAM

        #pragma surface surf Lambert alphatest:Zero

       

        sampler2D _MainTex;

        sampler2D _DissolveTex;

        float4 _Color;

        float3 _EdgeColor;

        float _EdgeWidth;

        float4 _RimColor;

        float _RimPower;

         

        struct Input {

            float2 uv_MainTex;

            float2 uv_DissolveTex;

            float3 viewDir;

        };

     

        void surf (Input IN, inout SurfaceOutput o) {

       

            half4 tex = tex2D(_MainTex, IN.uv_MainTex);

            half4 texd = tex2D(_DissolveTex, IN.uv_DissolveTex);

            o.Emission = tex.rgb * _Color.rgb;

            //o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb  ;

       

            o.Gloss = tex.a;

            o.Alpha = _Color.a - texd.r;

            o.Specular = 0;

       

            if ((o.Alpha < 0)&&(o.Alpha > -_EdgeWidth)&&(_Color.a>0))

            {

                o.Alpha = 1;

                o.Emission = _EdgeColor;

            }

 

 half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));

 

    o.Emission = _RimColor.rgb * pow (rim, _RimPower);

       

        }

    ENDCG

     

        }

    