using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Mouvement : MonoBehaviour
{
    // Composants
    private Rigidbody2D rb;
    private Animator anim;

    // Mouvement horizontal
    private float move;
    public float Speed;
    private bool isFacingRight;

    // Saut
    public float jump;

    // Détection du sol
    public Vector2 boxSize;
    public float castDistance;
    public float castHorizontalOffset;
    public LayerMask LevelLayer;

    public CoinManager CM;

    // Initialisation
    void Start()
    {
        isFacingRight = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Mise à jour
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(move * Speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump * 10));
        }

        if (move != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        anim.SetBool("isJumping", !isGrounded());

        if (!isFacingRight && move > 0)
        {
            Flip();
        }
        else if (isFacingRight && move < 0)
        {
            Flip();
        }
    }

    // Inversion de direction
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    // Vérification au sol
    public bool isGrounded()
    {
        Vector2 castPosition = new Vector2(transform.position.x + castHorizontalOffset, transform.position.y);
        if (Physics2D.BoxCast(castPosition, boxSize, 0, -Vector2.up, castDistance, LevelLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Dessin des Gizmos
    private void OnDrawGizmos()
    {
        Vector2 gizmoPosition = new Vector2(transform.position.x + castHorizontalOffset, transform.position.y);
        Gizmos.DrawWireCube(gizmoPosition - Vector2.up * castDistance, boxSize);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Killzone"))
        {
            RestartLevel();
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            CM.Coin_Count++;
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
