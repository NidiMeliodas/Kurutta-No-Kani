using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Script_Mouvement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float move;

    public float Speed;
    public float jump;
    public Vector2 boxSize;
    public float castDistance;
    public float castHorizontalOffset;
    public LayerMask LevelLayer;
    bool Grounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(move * Speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump * 10));
        }
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 1, -transform.up, castDistance, LevelLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Level"))
    //     {
    //         Vector3 normal = other.GetContact(0).normal;
    //         if(normal == Vector3.up)
    //         {
    //             Grounded = true;
    //         }
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Level"))
    //     {
    //         Grounded = false;
    //     }
    // }

}
