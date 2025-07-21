using Puzzle.Main;
using Puzzle.Sound;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.UI
{
    public class LevelSelectionUIController
    {
        private LevelSelectionUIView levelSelectionView;
        private LevelButtonView levelButtonPrefab;
        private List<LevelButtonView> levelButtons;

        public LevelSelectionUIController(LevelSelectionUIView levelSelectionView, LevelButtonView levelButtonPrefab)
        {
            this.levelSelectionView = levelSelectionView;
            this.levelButtonPrefab = levelButtonPrefab;
            levelSelectionView.SetController(this);
            InitializeController();
        }

        private void InitializeController()
        {
            levelButtons = new List<LevelButtonView>();
            //Hide();
        }

        public void Create(int levelCount)
        {
            levelSelectionView.EnableView();
            CreateLevelButtons(levelCount);
        }

        public void Show() => levelSelectionView.EnableView();

        public void Hide()
        {
            ResetLevelButtons();
            levelSelectionView.DisableView();
        }

        private void ResetLevelButtons()
        {
            //levelButtons.ForEach(button => Object.Destroy(button.gameObject));

            foreach(LevelButtonView button in  levelButtons)
                GameObject.Destroy(button.gameObject);

            levelButtons.Clear();
        }

        public void CreateLevelButtons(int levelCount)
        {
            for (int i = 1; i <= levelCount; i++)
            {
                LevelButtonView newButton = levelSelectionView.AddButton(levelButtonPrefab);
                newButton.SetController(this);
                newButton.SetLevelID(i);
            }
        }

        public void OnLevelSelected(int levelId)
        {
            GameService.Instance.EventService.OnLevelSelected.InvokeEvent(levelId);
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.BUTTON_CLICK);
            Hide();
        }
    }
}