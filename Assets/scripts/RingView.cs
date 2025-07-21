using Puzzle.Main;
using Puzzle.Spider;
using System.Collections.Generic;
using UnityEngine;

public class RingView : MonoBehaviour
{
    List<SpiderController> spidersInRing = new List<SpiderController>(); // maybe spiderView is enough
    [SerializeField] private SpiderColor ringColor;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpiderController sp = collision.GetComponent<SpiderView>().GetController();//maybe just spiderView is enough
        if(sp != null)
            spidersInRing.Add(sp);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SpiderController sp = collision.GetComponent<SpiderView>().GetController();// maybe just spiderView is enough
        if (sp != null)
            spidersInRing.Remove(sp);
    }

    public bool CheckWinCondition() //for now changing it to void, actually return type is bool
    {
        bool hasWon = true;

        foreach(SpiderController spider in  spidersInRing)
        {
            if(spider.GetSpiderColor() != ringColor)
            {
                hasWon = false;
                break;
            }
        }

        return hasWon;
    }

    public void SetViewColor(Color color) => spriteRenderer.color = color;

    public void SetRingColor(SpiderColor color) => ringColor = color;

    public void SetSortingOrder(int value) => spriteRenderer.sortingOrder = value;
}
