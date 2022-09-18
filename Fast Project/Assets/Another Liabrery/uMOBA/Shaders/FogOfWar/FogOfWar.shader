// Simple Fog of War shader:
// - Takes Black & White Render Texture
// - Converts white part to Transparency
// - Blurs it for a better effect
Shader "FogOfWarMask" {

Properties {
    _Color("Color", Color) = (0,0,0,1)
    _MainTex ("Base (RGB)", 2D) = "white" {}
    size ("Blur", Range(0, 0.1)) = 0.004
}

SubShader {
    Tags { "Queue"="Transparent" "RenderType"="Transparent" }
    LOD 100

    ZWrite Off // otherwise other transparent objects aren't visible anymore (indicator etc.)
    Blend SrcAlpha OneMinusSrcAlpha

    Lighting Off

    Pass {
        CGPROGRAM
        #pragma vertex vert_img
        #pragma fragment frag

        #include "UnityCG.cginc"

        fixed4 _Color;
        uniform sampler2D _MainTex;
        uniform float size;

        fixed4 frag(v2f_img i) : SV_Target{
            // without blur:
            //half4 mainColor = tex2D(_MainTex, i.uv);
            //float alpha = _Color.a - (mainColor.r + mainColor.b + mainColor.g) / 3;
            //return float4(_Color.rgb, alpha);

            // 8+8+1 tap filter with predefined gaussian weights
            // samples 8 values horiontally and 8 values vertically, not diagonally though
            // => looks better than 8 neighborhood
            // note: tex2D(tex, coord) -> second param is uv coord [0..1]
            float2 t = i.uv;
            float4 sum = float4(0.0, 0.0, 0.0, 0.0);
            sum += tex2D(_MainTex, float2(t.x - 4.0*size, t.y))            * 0.0162162162 / 2;
            sum += tex2D(_MainTex, float2(t.x - 3.0*size, t.y))            * 0.0540540541 / 2;
            sum += tex2D(_MainTex, float2(t.x - 2.0*size, t.y))            * 0.1216216216 / 2;
            sum += tex2D(_MainTex, float2(t.x - 1.0*size, t.y))            * 0.1945945946 / 2;
            sum += tex2D(_MainTex, float2(t.x + 1.0*size, t.y))            * 0.1945945946 / 2;
            sum += tex2D(_MainTex, float2(t.x + 2.0*size, t.y))            * 0.1216216216 / 2;
            sum += tex2D(_MainTex, float2(t.x + 3.0*size, t.y))            * 0.0540540541 / 2;
            sum += tex2D(_MainTex, float2(t.x + 4.0*size, t.y))            * 0.0162162162 / 2;
            sum += tex2D(_MainTex, float2(t.x,            t.y))            * 0.2270270270;
            sum += tex2D(_MainTex, float2(t.x,            t.y - 4.0*size)) * 0.0162162162 / 2;
            sum += tex2D(_MainTex, float2(t.x,            t.y - 3.0*size)) * 0.0540540541 / 2;
            sum += tex2D(_MainTex, float2(t.x,            t.y - 2.0*size)) * 0.1216216216 / 2;
            sum += tex2D(_MainTex, float2(t.x,            t.y - 1.0*size)) * 0.1945945946 / 2;
            sum += tex2D(_MainTex, float2(t.x,            t.y + 1.0*size)) * 0.1945945946 / 2;
            sum += tex2D(_MainTex, float2(t.x,            t.y + 2.0*size)) * 0.1216216216 / 2;
            sum += tex2D(_MainTex, float2(t.x,            t.y + 3.0*size)) * 0.0540540541 / 2;
            sum += tex2D(_MainTex, float2(t.x,            t.y + 4.0*size)) * 0.0162162162 / 2;                
            
            // alpha from render texture white (rgb average)
            float alpha = _Color.a - (sum.r + sum.b + sum.g) / 3;
            return float4(_Color.rgb, alpha);
        }
        ENDCG
    }
}
Fallback "Diffuse"

}
