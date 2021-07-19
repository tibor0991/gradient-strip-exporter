using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.tibor0991.gradient
{
    public static class GradientExtension
    {
        public static Texture2D ConvertToTextureStrip(this Gradient gradient, int hResolution, int vResolution, TextureFormat fmt)
        {
            Texture2D texture = new Texture2D(hResolution, vResolution, fmt, false, true);
            for (int u = 0; u < hResolution; u++)
            {
                float t = u / (float)(hResolution - 1);
                texture.SetPixel(u, 1, gradient.Evaluate(t));
            }

            return texture;
        }

        public static Texture2D ConvertToTextureStrip(this Gradient gradient, int hResolution, TextureFormat fmt)
        {
            return ConvertToTextureStrip(gradient, hResolution, 1, fmt);
        }

        public static Texture2D ConvertToTextureStrip(this Gradient gradient, int hResolution)
        {
            return ConvertToTextureStrip(gradient, hResolution, 1, TextureFormat.ARGB32);
        }
    }
}