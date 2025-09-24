using UnityEngine;

namespace KitchenChaos.Player
{
    [System.Serializable]
    public class HighlightSettings
    {
        public Color Color = Color.white;
        public float Intensity = 1.2f;
        public float EmissionStrength = 0.3f;
        public AnimationCurve PulseCurve = AnimationCurve.EaseInOut(0, 1, 1, 1.2f);
    }
}