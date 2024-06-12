using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement_Script : MonoBehaviour
{
    public float speed = 2f; // Vitesse de déplacement du joueur
    public float jump = 75f; // Force de saut

    private float Move; // Variable pour stocker l'entrée horizontale

    public Rigidbody2D rb; // Référence au Rigidbody2D du joueur

    public bool isJumping; // Variable pour vérifier si le joueur est en train de sauter

    // Start est appelé avant la première frame de mise à jour
    void Start()
    {
        // Initialisation des variables si nécessaire
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update est appelé une fois par frame
    void Update()
    {
        // Récupérer l'entrée horizontale (flèches gauche/droite ou touches A/D)
        Move = Input.GetAxis("Horizontal");

        // Appliquer la vitesse de déplacement au Rigidbody2D
        rb.velocity = new Vector2(speed * Move, rb.velocity.y);

        // Vérifier si le joueur appuie sur le bouton de saut et s'il n'est pas déjà en train de sauter
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            // Ajouter une force vers le haut pour effectuer le saut
            rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            isJumping = true; // Marquer le joueur comme étant en train de sauter
        }
    }

    // Cette méthode est appelée lorsqu'une collision 2D est détectée avec le corps (CapsuleCollider2D)
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Vérifier si l'objet en collision a le tag "Level"
        if (other.gameObject.CompareTag("Level"))
        {
            isJumping = false; // Le joueur n'est plus en train de sauter (pieds touchent le sol)
        }
    }

    // Cette méthode est appelée lorsqu'une collision 2D se termine avec le corps (CapsuleCollider2D)
    private void OnCollisionExit2D(Collision2D other)
    {
        // Vérifier si l'objet en collision a le tag "Level"
        if (other.gameObject.CompareTag("Level"))
        {
            isJumping = true; // Le joueur est de nouveau en train de sauter (pieds quittent le sol)
        }
    }

    // Cette méthode est appelée lorsqu'un autre collider entre dans le trigger (BoxCollider2D aux pieds ou CapsuleCollider2D)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si l'objet en collision a le tag "Killzone"
        if (other.CompareTag("Killzone"))
        {
            RestartLevel(); // Réinitialiser le niveau
        }
    }

    // Fonction pour réinitialiser le niveau en rechargeant la scène
    void RestartLevel()
    {
        // Recharger la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}