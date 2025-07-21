using Puzzle.Main;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Gaps
{
    public class GapService
    {
        private GapView gapPrefab;
        private List<GapView> gaps;

        private GapData gapData;

        public GapService(GapView gapPrefab)
        {
            this.gapPrefab = gapPrefab;
            gaps = new List<GapView>();

            SubscribeToEvents();
        }

        ~GapService() => UnsubscribeToEvents();

        private void SubscribeToEvents()
        {
            GameService.Instance.EventService.OnLevelSelected.AddListener(CreateGaps);
            GameService.Instance.EventService.OnTurnInitiated.AddListener(DisableGaps);
            GameService.Instance.EventService.OnTurnCompleted.AddListener(EnableGaps);
            GameService.Instance.EventService.OnGameWon.AddListener(DisableGaps);
            GameService.Instance.EventService.OnGameOver.AddListener(DestroyGaps);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.EventService.OnLevelSelected.RemoveListener(CreateGaps);
            GameService.Instance.EventService.OnTurnInitiated.RemoveListener(DisableGaps);
            GameService.Instance.EventService.OnTurnCompleted.RemoveListener(EnableGaps);
            GameService.Instance.EventService.OnGameWon.RemoveListener(DisableGaps);
            GameService.Instance.EventService.OnGameOver.RemoveListener(DestroyGaps);
        }

        private void CreateGaps(int levelId)
        {
            gapData = GameService.Instance.LevelService.GetGapData(levelId);

            foreach(GapLayerDetails gapDetails in gapData.levelGapDetails)
            {
                float angle = gapDetails.startingAngle;

                for (int i = 0; i < gapDetails.gapCount; i++)
                {
                    Vector3 gapPos = new Vector3(gapDetails.radius * Mathf.Cos(angle * Mathf.Deg2Rad), gapDetails.radius * Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                    GapView newGap = GameObject.Instantiate(gapPrefab, gapPos, Quaternion.identity);

                    gaps.Add(newGap);

                    angle += gapDetails.addOnAngle;
                }
            }
        }

        private void DestroyGaps()
        {
            foreach (GapView gap in gaps)
                GameObject.Destroy(gap.gameObject);

            gaps.Clear();
        }

        private void DisableGaps()
        {
            foreach(GapView gap in gaps)
                gap.DisableCollider();
        } 

        private void EnableGaps()
        {
            foreach (GapView gap in gaps)
                gap.EnableCollider();
        }
    }
}