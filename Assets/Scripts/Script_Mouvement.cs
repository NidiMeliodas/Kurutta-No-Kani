using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    [Header("Mouvement")]
    private float move;
    public float Speed;
    private bool isFacingRight;

    // Saut
    public float jump;
    [Header("Floor Detection")]

    // Détection du sol
    public Vector2 boxSize;
    public float castDistance;
    public float castHorizontalOffset;
    public LayerMask LevelLayer;

    public CoinManager CoinScript;

    private bool Dead;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip landingSound;
    [SerializeField] private AudioClip runningSound;
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

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded())
        {
            Sound_Manager.instance.PlaySound(jumpSound);
            rb.AddForce(new Vector2(rb.velocity.x, jump * 10));
        }

        

        if (move != 0)
        {
            anim.SetBool("isRunning", true);
            Sound_Manager.instance.PlaySound(runningSound);

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
            anim.SetBool("IsDead", true);
            Sound_Manager.instance.PlaySound(deathSound);
            // Destroy(gameObject);
            StartCoroutine(RestartLevel());
            this.enabled = false;
            
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            Sound_Manager.instance.PlaySound(coinSound);
            Destroy(other.gameObject);
            CoinScript.Coin_Count++;
        }
    }

   
    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}