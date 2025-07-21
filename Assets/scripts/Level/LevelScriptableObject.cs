using Puzzle.Board;
using Puzzle.Gaps;
using Puzzle.Spider;
using UnityEngine;

namespace Puzzle.Level
{
    [CreateAssetMenu( fileName = "NewLevelScriptableObject", menuName = "ScriptableObjects/LevelScriptableObject")]
    public class LevelScriptableObject : ScriptableObject
    {
        public int id;
        public float cameraSize;

        public BoardData boardData;
        public SpiderData spiderData;
        public GapData gapData;
    }
}