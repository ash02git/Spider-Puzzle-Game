using Puzzle.Gaps;
using System;
using UnityEngine;

namespace Puzzle.Spider
{
    public class SpiderController
    {
        private SpiderView spiderView;

        private Vector3 targetPosition;
        private SpiderColor spiderColor;

        public SpiderController(SpiderView spiderPrefab, Vector3 spiderPos)
        {
            spiderView = GameObject.Instantiate(spiderPrefab, spiderPos, Quaternion.identity);
            spiderView.SetController(this);
        }

        public SpiderColor GetSpiderColor() => spiderColor;

        public void SetSpiderColor(SpiderColor spiderColor) => this.spiderColor = spiderColor;

        public void SetViewColor(Color color) => spiderView.SetColor(color);

        public Vector3 GetPosition() => spiderView.transform.position;

        public void SetPosition(Vector3 position) => spiderView.transform.position = position;

        public void ChangeAnimation(bool value) => spiderView.ChangeAnimation(value);

        public void PlayGameWonAnimation() => spiderView.PlayGameWonAnimation();

        public void StartMovement(Vector3 targetPosition) => spiderView.StartMovement(targetPosition);

        public void Destroy() => GameObject.Destroy(spiderView.gameObject);
    }
}