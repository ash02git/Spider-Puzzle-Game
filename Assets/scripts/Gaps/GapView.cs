using Puzzle.Main;
using Puzzle.Spider;
using Puzzle.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Gaps
{
    public class GapView : MonoBehaviour
    {
        private List<SpiderController> spidersInCollission = new List<SpiderController>();
        private List<Vector3> targetPositions = new List<Vector3>();
        [SerializeField] private float speed = 0.5f; //changable in inspector
        private float radius;
        private Collider2D collider2d;

        private void Start()
        {
            collider2d = GetComponent<CircleCollider2D>();
            radius = GetComponent<CircleCollider2D>().radius;
        }

        private void OnMouseEnter()
        {
            GameService.Instance.BoardService.ShowTurnImage(transform.position);
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.GAP_ENTERED);

            ClearSpidersList();
            GetSpidersInGap();
            ChangeAnimationForSpiders(true);
        }

        private void OnMouseExit()
        {
            GameService.Instance.BoardService.HideTurnImage();

            ChangeAnimationForSpiders(false);
        }

        public void DisableCollider() => collider2d.enabled = false;

        public void EnableCollider() => collider2d.enabled = true;

        private void OnMouseDown()
        {
            GapClicked();
            GameService.Instance.EventService.OnTurnInitiated.InvokeEvent();
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.GAP_CLICKED);
        }

        private void ClearSpidersList() => spidersInCollission.Clear();

        private void ChangeAnimationForSpiders(bool value)
        {
            foreach (SpiderController spider in spidersInCollission)
                spider.ChangeAnimation(value);
        }

        private void GetSpidersInGap()
        {
            Collider2D[] collidedSpiders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D col in collidedSpiders)
            {
                SpiderController sp = col.GetComponent<SpiderView>()?.GetController();
                if (sp != null)
                    spidersInCollission.Add(sp);
            }
        }

        private void GapClicked()
        {
            //Hide turn image
            GameService.Instance.BoardService.HideTurnImage();

            //Sort the Spiders in list based on angle from 0 to 360 around the gap's centre
            SortSpiders();

            //Assigning target positions to spiders
            AssignTargetPositions();

            //Start movement for each spider
            StartSpiderMovements();

            //Start the Coroutine
            StartCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            yield return new WaitForSeconds(speed);
            GameService.Instance.EventService.OnTurnCompleted.InvokeEvent();
        }

        private void AssignTargetPositions()
        {
            for (int i = 0; i <= spidersInCollission.Count - 2; i++)
                targetPositions.Add( spidersInCollission[i+1].GetPosition() );

            targetPositions.Add(spidersInCollission[0].GetPosition());
        }

        private void SortSpiders()
        {
            List<float> angles = new List<float>();

            foreach (SpiderController spider in spidersInCollission)
            {
                Vector2 dir = (Vector2)spider.GetPosition() - (Vector2)transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                if (angle < 0)
                    angle += 360;

                angles.Add(angle);
            }

            //bubble sort
            for (int i = 0; i < spidersInCollission.Count - 1; i++)
            {
                for (int j = 0; j < spidersInCollission.Count - 1 - i; j++)
                {
                    if (angles[j] > angles[j + 1])
                    {
                        SpiderController temp = spidersInCollission[j];
                        spidersInCollission[j] = spidersInCollission[j + 1];
                        spidersInCollission[j + 1] = temp;

                        float tempang = angles[j];
                        angles[j] = angles[j + 1];
                        angles[j + 1] = tempang;
                    }
                }
            }

        }

        private void StartSpiderMovements()
        {
            for (int i = 0; i < spidersInCollission.Count; i++)
                spidersInCollission[i].StartMovement(targetPositions[i]);
        }
    }
}