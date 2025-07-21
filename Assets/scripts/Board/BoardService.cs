using Puzzle.Main;
using Puzzle.Utilities;
using System.Collections.Generic;
using UnityEngine;
using Puzzle.Sound;
using System.Threading.Tasks;

namespace Puzzle.Board
{
    public class BoardService
    {
        //prefabs
        private GameObject turnImage;
        private GameObject nonRingPrefab;
        private RingView ringPrefab;

        //gameObject references
        private List<RingView> rings;
        private List<GameObject> nonRings;

        private BoardData boardData;
        private float ringScale;

        public BoardService(GameObject gapCirclePrefab, RingView ringPrefab, GameObject turnImage)
        {
            this.nonRingPrefab = gapCirclePrefab;
            this.ringPrefab = ringPrefab;
            this.turnImage = GameObject.Instantiate(turnImage);

            rings = new List<RingView>();
            nonRings = new List<GameObject>();

            SubscribeToEvents();
        }

        ~BoardService() => UnSubscribeToEvents();

        private void SubscribeToEvents()
        {
            GameService.Instance.EventService.OnLevelSelected.AddListener(CreateBoard);
            GameService.Instance.EventService.OnTurnCompleted.AddListener(CheckWinCondition);
            GameService.Instance.EventService.OnGameOver.AddListener(DestoryBoard);
        }

        private void UnSubscribeToEvents()
        {
            GameService.Instance.EventService.OnLevelSelected.RemoveListener(CreateBoard);
            GameService.Instance.EventService.OnTurnCompleted.RemoveListener(CheckWinCondition);
            GameService.Instance.EventService.OnGameOver.RemoveListener(DestoryBoard);
        }

        private void CreateBoard(int levelId)
        {
            boardData = GameService.Instance.LevelService.GetBoardData(levelId);
            ringScale = boardData.startingScale;
            int ringCount = boardData.numberOfRings;

            for(int i = 1; i <= ringCount; i++)
            {
                CreateNonRing(i);
                CreateRing(i);
            }
        }

        private void CreateRing(int index)
        {
            RingView ring = GameObject.Instantiate(ringPrefab);
            Utility.SetScale(ring.gameObject, ringScale);
            ring.SetViewColor(boardData.ringColorPairs[index - 1].viewColor);
            ring.SetRingColor(boardData.ringColorPairs[index - 1].ringColor);
            ring.SetSortingOrder((boardData.numberOfRings - index) * 2);

            rings.Add(ring);

            UpdateRingScale();
        }

        private void CreateNonRing(int index)
        {
            GameObject nonRing = GameObject.Instantiate(nonRingPrefab);
            Utility.SetScale(nonRing, ringScale);
            nonRing.GetComponent<SpriteRenderer>().sortingOrder = (boardData.numberOfRings - index) * 2 + 1;

            nonRings.Add(nonRing);

            UpdateRingScale();
        }

        private void DestoryBoard()
        {
            foreach(RingView ring in rings)
            {
                GameObject.Destroy(ring.gameObject);
            }

            foreach(GameObject nonRing in nonRings)
            {
                GameObject.Destroy(nonRing);
            }

            rings.Clear();
            nonRings.Clear();

            turnImage.SetActive(false); 
        }

        private void UpdateRingScale() => ringScale += boardData.addOnScale;

        public void ShowTurnImage(Vector2 position)
        {
            turnImage.SetActive(true);
            turnImage.transform.position = position;
        }

        public void HideTurnImage() => turnImage.SetActive(false);

        private void CheckWinCondition()
        {
            bool hasWon = true;

            foreach(RingView ring in rings)
            {
                if( !ring.CheckWinCondition() )
                {
                    hasWon = false;
                    break;
                }
            }

            if (hasWon)
            {
                Debug.Log("Completed game");
                GameService.Instance.SoundService.PlaySoundEffects(SoundType.GAME_WON);
                GameService.Instance.EventService.OnGameWon.InvokeEvent();

                _ = OnGameOver();
            }
        }

        private async Task OnGameOver()
        {
            await Task.Delay(4000);//time in milliseconds
            GameService.Instance.EventService.OnGameOver.InvokeEvent();
        }
    }
}