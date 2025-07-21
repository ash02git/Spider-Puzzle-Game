using Puzzle.Board;
using Puzzle.Gaps;
using Puzzle.Spider;
using System.Collections.Generic;

namespace Puzzle.Level
{
    public class LevelService
    {
        private List<LevelScriptableObject> levelSOs;

        public LevelService(List<LevelScriptableObject> levelSOs) => this.levelSOs = levelSOs;

        public BoardData GetBoardData(int levelId) => levelSOs.Find(level => level.id == levelId).boardData;

        public SpiderData GetSpiderData(int levelId) => levelSOs.Find(level => level.id == levelId).spiderData;

        public GapData GetGapData(int levelId) => levelSOs.Find(level => level.id == levelId).gapData;

        public float GetCameraSize(int levelId) => levelSOs.Find(level => level.id == levelId).cameraSize;
    }
}