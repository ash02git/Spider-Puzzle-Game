using Puzzle.Main;
using UnityEngine;

namespace Puzzle.GameCamera
{
    public class CameraService : MonoBehaviour
    {
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
            GameService.Instance.EventService.OnLevelSelected.AddListener(ChangeCameraSize);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.EventService.OnLevelSelected.RemoveListener(ChangeCameraSize);
        }

        private void ChangeCameraSize(int levelId)
        {
            float cameraSize = GameService.Instance.LevelService.GetCameraSize(levelId);

            GetComponent<Camera>().orthographicSize = cameraSize;
        }
    }
}