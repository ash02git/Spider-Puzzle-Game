using Puzzle.Main;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Spider
{
    public class SpiderService
    {
        private SpiderView spiderPrefab;
        private List<SpiderController> spiders;
        
        private SpiderData spiderData;

        public SpiderService(SpiderView spiderView)
        {
            this.spiderPrefab = spiderView;
            spiders = new List<SpiderController>();

            SubscribeToEvents();
        }

        ~SpiderService() => UnsubscribeToEvents();

        private void SubscribeToEvents()
        {
            GameService.Instance.EventService.OnLevelSelected.AddListener(CreateSpiders);
            GameService.Instance.EventService.OnGameWon.AddListener(PlayWonAnimations);
            GameService.Instance.EventService.OnGameOver.AddListener(DestroySpiders);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.EventService.OnLevelSelected.RemoveListener(CreateSpiders);
            GameService.Instance.EventService.OnGameWon.RemoveListener(PlayWonAnimations);
            GameService.Instance.EventService.OnGameOver.RemoveListener(DestroySpiders);
        }

        private void CreateSpiders(int levelId)
        {
            spiderData = GameService.Instance.LevelService.GetSpiderData(levelId);

            foreach(SpiderTypeData data in spiderData.spiderList)
            {
                float angle = data.startingAngle;

                for(int i =0; i < data.count; i++)
                {
                    Vector3 spiderPos = new Vector3(data.radius * Mathf.Cos(angle * Mathf.Deg2Rad), data.radius * Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                    SpiderController newSpider = new SpiderController(spiderPrefab, spiderPos);
                    //SpiderView newSpiders = GameObject.Instantiate(spiderPrefab, spiderPos, Quaternion.identity);
                    newSpider.SetSpiderColor(data.spiderColor);
                    //newSpiders.spiderColor = data.spiderColor;
                    newSpider.SetViewColor(data.viewColor);
                    //newSpiders.SetColor(data.viewColor);

                    spiders.Add(newSpider);

                    angle += data.addOnAngle;
                }
            }

            ShuffleSpiders();
        }

        private void ShuffleSpiders()
        {
            foreach(SpiderController spider in spiders)
            {
                SpiderController spiderToSwap = spiders[UnityEngine.Random.Range(0, spiders.Count)];

                //swapping
                Vector3 temp = spider.GetPosition();
                spider.SetPosition(spiderToSwap.GetPosition());
                spiderToSwap.SetPosition(temp);
            }
        }

        private void PlayWonAnimations()
        {
            foreach (SpiderController spider in spiders)
                spider.PlayGameWonAnimation();
        }

        private void DestroySpiders()
        {
            foreach (SpiderController spider in spiders)
                spider.Destroy();
            
            spiders.Clear();
        }
    }
}