using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.UI
{
    public class LevelSelectionUIView : MonoBehaviour
    {
        private LevelSelectionUIController controller;
        [SerializeField] private Transform buttonsGridTransform;
        [SerializeField] private Button quitButton;

        private void Start()
        {
            quitButton.onClick.AddListener( ()=>Application.Quit() );
        }

        public void SetController(LevelSelectionUIController controller) => this.controller = controller;

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);

        public LevelButtonView AddButton(LevelButtonView prefab) => Instantiate<LevelButtonView>(prefab, buttonsGridTransform);
    }
}