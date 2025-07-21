using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Spider
{
    [System.Serializable]
    public struct SpiderData
    {
        public List<SpiderTypeData> spiderList;
    }

    [System.Serializable]
    public struct SpiderTypeData
    {
        public int count;
        public float radius;
        public float startingAngle;
        public float addOnAngle;//may not be needed as it can be calculated
        public SpiderColor spiderColor;
        public Color viewColor;
    }
}