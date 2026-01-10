using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StevesWalls
{
    public static class ColorBlenderUtil
    {
        public static Color BlendColors(List<Color> colors)
        {
            if (colors == null || colors.Count == 0) return Color.white;

            float avgR = colors.Average(c => c.r);
            float avgG = colors.Average(c => c.g);
            float avgB = colors.Average(c => c.b);
            float avgA = colors.Average(c => c.a);

            Color avgColor = new Color(avgR, avgG, avgB, avgA);
            return avgColor;
        }
    }
}
