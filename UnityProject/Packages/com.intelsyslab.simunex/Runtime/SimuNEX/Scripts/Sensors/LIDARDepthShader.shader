Shader "Custom/LidarDepthShader"  // The name of the shader
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" }  // Tag to specify rendering type, typically for opaque objects
        Pass
        {
            // Begin the programmable GPU code block
            CGPROGRAM
            // Vertex shader: processes each vertex of a mesh
            #pragma vertex vert
            // Fragment (or pixel) shader: processes each pixel of a rendered image
            #pragma fragment frag

            // Struct for input data coming from the vertex shader to the fragment shader
            struct appdata
            {
                float4 vertex : POSITION;  // The position of the vertex in 3D space
            };

            // Struct for output data from the vertex shader to the fragment shader
            struct v2f
            {
                float4 pos : SV_POSITION;  // The position in screen space (after projection)
                float depth : TEXCOORD0;   // Depth value to pass to the fragment shader
            };

            // Vertex shader function
            v2f vert(appdata v)
            {
                v2f o;
                // Transform the vertex position from object space to clip space (screen space)
                o.pos = UnityObjectToClipPos(v.vertex);
                
                // Calculate the depth value: divide the z-coordinate by the w-coordinate to normalize depth
                // This converts the depth into a [0, 1] range where 0 is near and 1 is far.
                o.depth = o.pos.z / o.pos.w;
                return o;  // Pass the output to the fragment shader
            }

            // Function to encode a float value into an RGBA color
            // This function breaks a 32-bit float into four 8-bit parts for R, G, B, and A channels
            float4 EncodeFloatRGBA(float v)
            {
                // Multiply the float by powers of 256 to separate the float into its byte components
                float4 enc = frac(v * float4(1.0, 256.0, 65536.0, 16777216.0));
                
                // Remove the carryover that would happen due to the multiplication above
                enc -= enc.yzww * float4(1.0/256.0, 1.0/256.0, 1.0/256.0, 0.0);
                
                return enc;  // Return the encoded color as a float4 (RGBA)
            }

            // Fragment shader function
            float4 frag(v2f i) : SV_Target
            {
                // Encode the depth value into an RGBA color
                return EncodeFloatRGBA(i.depth);
            }
            ENDCG  // End the programmable GPU code block
        }
    }
}
