using System.Collections.Generic;
using UnityEngine;

public class InnerGap : MonoBehaviour
{
    Vector3 pos1;
    Vector3 pos2;
    Vector3 pos3;
    Vector3 pos4;
    Vector3 pos5;
    List<GameObject> collidedObjects = new List<GameObject>();
    List<float> angles = new List<float>();
    float radius;
    Vector2 pos;
    
    bool hasClicked = false;
    public bool startedMoving;
    public float speed;
    public GameObject prefab;
    private GameObject tempPrefab;
    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D coll = GetComponent<CircleCollider2D>();
        radius = coll.radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && hasClicked == false)
        {
            Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(mousepos))
            {

                //Debug.Log("has clicked");
                hasClicked = true;
                startedMoving = true;


                Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);

                angles.Clear();


                collidedObjects.Clear();

                foreach (Collider2D c in overlappedColliders)
                {
                    if (c.CompareTag("redspider") || c.CompareTag("greenspider") || c.CompareTag("bluespider"))
                    {
                        collidedObjects.Add(c.gameObject);
                        Vector2 dir = c.transform.position - gameObject.transform.position;
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        if (angle < 0)
                            angle = angle + 360;
                        angles.Add(angle);

                    }
                }

                for (int i = 0; i < collidedObjects.Count - 1; i++)
                {
                    for (int j = 0; j < collidedObjects.Count - i - 1; j++)
                    {
                        if (angles[j] > angles[j + 1])
                        {
                            GameObject temp = collidedObjects[j];
                            collidedObjects[j] = collidedObjects[j + 1];
                            collidedObjects[j + 1] = temp;

                            float tempang = angles[j];
                            angles[j] = angles[j + 1];
                            angles[j + 1] = tempang;
                        }
                    }
                }

                pos1 = collidedObjects[0].transform.position;
                pos2 = collidedObjects[1].transform.position;
                pos3 = collidedObjects[2].transform.position;
                pos4 = collidedObjects[3].transform.position;
                pos5 = collidedObjects[4].transform.position;
            }
        }
        
        if (hasClicked && startedMoving)
        {
            collidedObjects[0].transform.position = Vector3.MoveTowards(collidedObjects[0].transform.position, pos2, speed);
            collidedObjects[1].transform.position = Vector3.MoveTowards(collidedObjects[1].transform.position, pos3, speed);
            collidedObjects[2].transform.position = Vector3.MoveTowards(collidedObjects[2].transform.position, pos4, speed);
            collidedObjects[3].transform.position = Vector3.MoveTowards(collidedObjects[3].transform.position, pos5, speed);
            collidedObjects[4].transform.position = Vector3.MoveTowards(collidedObjects[4].transform.position, pos1, speed);

            if (collidedObjects[0].transform.position == pos2 && collidedObjects[1].transform.position == pos3 && collidedObjects[2].transform.position == pos4 && collidedObjects[3].transform.position == pos5 && collidedObjects[4].transform.position == pos1)
            {
                startedMoving = false;
                hasClicked = false;
                Debug.Log("completed a cycle");
            }

        }


        
        
    }

    private void OnMouseEnter()
    {

        tempPrefab = Instantiate(prefab, transform.position, prefab.transform.rotation);


    }

    private void OnMouseExit()
    {
        Destroy(tempPrefab);
    }



}

