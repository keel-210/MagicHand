Shader "Custom/BarrierBox"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [PowerSlider(3.0)]
        _WireframeVal ("Wireframe width", Range(0., 0.34)) = 0.05
        _FrontColor ("Front color", color) = (1., 1., 1., 1.)
        _FaceColor ("Face color", color) = (1., 1., 1., 1.)

        _XFace("X Test",Range(-1,1)) = 0.5
        _YFace("Y Test",Range(-1,1)) = 0.5
        _ZFace("Z Test",Range(-1,1)) = 0.5
        _XLine("XLine Test",Range(-1,1)) = 0.5
        _YLine("YLine Test",Range(-1,1)) = 0.5
        _ZLine("ZLine Test",Range(-1,1)) = 0.5
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha 
            Cull Back
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
            #include "UnityCG.cginc"

            struct v2g {
                float4 pos : SV_POSITION;
                float3 localPos : TEXCOORD0;
            };

            struct g2f {
                float4 pos : SV_POSITION;
                float3 bary : TEXCOORD0;
                float3 LocalPos : TEXCOORD1;
            };
            v2g vert(appdata_base v) {
                v2g o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.localPos = mul(unity_ObjectToWorld, v.vertex)-unity_ObjectToWorld._m03_m13_m23;
                return o;
            }

            [maxvertexcount(3)]
            void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream) {
                g2f o;
                o.pos = IN[0].pos;
                o.bary = float3(1., 0., 0.);
                o.LocalPos = IN[0].localPos;
                triStream.Append(o);
                o.pos = IN[1].pos;
                o.bary = float3(0., 0., 1.);
                o.LocalPos = IN[1].localPos;
                triStream.Append(o);
                o.pos = IN[2].pos;
                o.bary = float3(0., 1., 0.);
                o.LocalPos = IN[2].localPos;
                triStream.Append(o);
            }

            float _WireframeVal;
            fixed4 _FrontColor;
            fixed4 _FaceColor;

            fixed _XFace,_YFace,_ZFace;
            fixed _XLine,_YLine,_ZLine;
            
            fixed4 frag(g2f i) : SV_Target 
            {
                if(!any(bool3(i.bary.x < _WireframeVal, i.bary.y < _WireframeVal, i.bary.z < _WireframeVal)))
                {
                    if(!any(bool3(i.LocalPos.x > _XFace, i.LocalPos.y > _YFace, i.LocalPos.z > _ZFace)))
                    return _FaceColor;
                    else
                    discard;
                }

                if(any(bool3(i.LocalPos.x > _XLine, i.LocalPos.y > _YLine, i.LocalPos.z > _ZLine)))
                discard;
                return _FrontColor;
            }
            ENDCG
        }
    }
}
