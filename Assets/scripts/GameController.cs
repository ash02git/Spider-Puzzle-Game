using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool redcheck;
    public bool greencheck;
    public bool bluecheck;
    public bool isInCollision = false;
    public GameObject[] redSpiders;
    public GameObject[] greenSpiders;
    public GameObject[] blueSpiders;
    void Start()
    {
        redSpiders = GameObject.FindGameObjectsWithTag("redspider");
        blueSpiders = GameObject.FindGameObjectsWithTag("bluespider");
        greenSpiders = GameObject.FindGameObjectsWithTag("greenspider");

    }

    // Update is called once per frame
    void Update()
    {

        //checking for red ring
        redcheck = true;
        foreach (GameObject red in redSpiders)
        {
            CircleCollider2D coll=red.GetComponent<CircleCollider2D>();
            float radius=coll.radius;
            
            Collider2D[] collidersInContact = Physics2D.OverlapCircleAll(red.transform.position, radius);

            isInCollision = false;
            
            foreach (Collider2D collider in collidersInContact)
            {
                if (collider.CompareTag("redring"))
                {
                    isInCollision = true;
                    break;
                }
            }

            if(!isInCollision)
            {
                redcheck = false;
                break;
            }
        }
        //checking for green ring
        greencheck = true;
        foreach (GameObject green in greenSpiders)
        {
            CircleCollider2D coll = green.GetComponent<CircleCollider2D>();
            float radius = coll.radius;
            
            Collider2D[] collidersInContact = Physics2D.OverlapCircleAll(green.transform.position, radius);

            isInCollision = false;

            foreach (Collider2D collider in collidersInContact)
            {
                if (collider.CompareTag("greenring"))
                {
                    isInCollision = true;
                    break;
                }
            }

            if (!isInCollision)
            {
                greencheck = false;
                break;
            }
        }

        //checking for blue ring
        bluecheck = true;
        foreach (GameObject blue in blueSpiders)
        {
            CircleCollider2D coll = blue.GetComponent<CircleCollider2D>();
            float radius = coll.radius;
            
            Collider2D[] collidersInContact = Physics2D.OverlapCircleAll(blue.transform.position, radius);

            isInCollision = false;

            foreach (Collider2D collider in collidersInContact)
            {
                if (collider.CompareTag("bluering"))
                {
                    isInCollision = true;
                    break;
                }
            }

            if (!isInCollision)
            {
                bluecheck = false;
                break;
            }
        }


        if(redcheck && greencheck && bluecheck)
        {
            SceneManager.LoadScene("Endscreen");
        }

        
    }
}
