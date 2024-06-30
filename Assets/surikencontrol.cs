using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class surikencontrol : MonoBehaviour
{
    public GameObject point A;
    public GameObject point B;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool('isMoving', true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.velocity == new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        
    }
}
