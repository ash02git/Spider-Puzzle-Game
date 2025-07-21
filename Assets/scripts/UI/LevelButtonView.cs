using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.UI
{
    public class LevelButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buttonText;
        private LevelSelectionUIController controller;
        private int levelId;

        private void Start() => GetComponent<Button>().onClick.AddListener(OnLevelButtonClicked);

        private void OnDestroy() => GetComponent<Button>().onClick.RemoveListener(OnLevelButtonClicked);

        public void SetLevelID(int levelId)
        {
            this.levelId = levelId;
            buttonText.SetText("Level " + levelId);
        }

        public void SetController(LevelSelectionUIController controller) => this.controller = controller;

        private void OnLevelButtonClicked() => controller.OnLevelSelected(levelId);
    }
}