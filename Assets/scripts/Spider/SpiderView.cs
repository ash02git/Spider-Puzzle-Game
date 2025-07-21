using Puzzle.Gaps;
using System;
using UnityEngine;

namespace Puzzle.Spider
{
    public class SpiderView : MonoBehaviour
    {
        private SpiderController controller;
        
        bool startedMoving = false;
        Vector3 targetPosition;
        //GapView currentGap;

        private float speed;

        private Animator animator;
        private string parameterName;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            parameterName = "isAwake";
        }

        public void SetController(SpiderController controller)
        {
            this.controller = controller;
        }

        public SpiderController GetController() => controller;

        public void StartMovement(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
            float distance = Vector3.Distance(transform.position, targetPosition);
            speed = distance * 2; // to complete the movement in 0.5s (instead of *2, you can also do /0.5f)
            startedMoving = true;
        }

        private void Update()
        {
            if (startedMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                if (transform.position == targetPosition)
                {
                    startedMoving = false;
                }
            }
        }

        public void SetColor(Color viewColor) => GetComponent<SpriteRenderer>().color = viewColor;

        public void ChangeAnimation(bool value) => animator.SetBool(parameterName, value);

        public void PlayGameWonAnimation() => animator.SetBool("hasWon", true);
    }
}