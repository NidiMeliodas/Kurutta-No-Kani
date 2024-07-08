using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public float speed;
    public int starting_Point;
    public Transform[] points;
    public float PivotOffset;

    private int i ;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = (points[starting_Point].position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(transform.position.y<collision.transform.position.y - PivotOffset)
            collision.transform.SetParent(transform);
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
