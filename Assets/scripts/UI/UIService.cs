using Puzzle.Main;
using System;
using UnityEngine;

namespace Puzzle.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Level Selection")]
        [SerializeField] private LevelSelectionUIView levelSelectionView;
        private LevelSelectionUIController levelSelectionController;
        [SerializeField] private LevelButtonView levelButtonPrefab;

        private void Awake()
        {
            levelSelectionController = new LevelSelectionUIController(levelSelectionView, levelButtonPrefab);
        }

        private void Start()
        {
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.EventService.OnGameOver.AddListener(ShowLevelSelectionUI);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.EventService.OnGameOver.RemoveListener(ShowLevelSelectionUI);
        }

        public void CreateLevelSelectionUI(int level) => levelSelectionController.Create(level);

        public void ShowLevelSelectionUI() => levelSelectionController.Show();
    }
}
