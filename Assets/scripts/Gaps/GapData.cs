using System.Collections.Generic;

namespace Puzzle.Gaps
{
    [System.Serializable]
    public struct GapData
    {
        public List<GapLayerDetails> levelGapDetails;
    }

    [System.Serializable]
    public struct GapLayerDetails
    {
        public int layerNumber;
        public int gapCount;
        public float radius;
        public float startingAngle;
        public float addOnAngle;
    }
}