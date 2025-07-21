using Puzzle.Spider;
using UnityEngine;

namespace Puzzle.Utilities
{
    public static class Utility
    {
        public static void SetScale(GameObject gameObject, float scale) => gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }
}