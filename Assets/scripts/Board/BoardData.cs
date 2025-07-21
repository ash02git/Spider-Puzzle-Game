using Puzzle.Spider;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Board
{
    [System.Serializable]
    public struct BoardData
    {
        public int numberOfRings;
        public float startingScale;//2.5f
        public float addOnScale;//1.5f
        public List<RingColorPair> ringColorPairs;
    }

    [System.Serializable]
    public struct RingColorPair
    {
        public SpiderColor ringColor;
        public Color viewColor;
    }
}