using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint; // Point de réapparition

    private Rigidbody2D rb; // Référence au Rigidbody2D du joueur

    // Start est appelé avant la première frame de mise à jour
    void Start()
    {
        // Initialisation du Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        if (respawnPoint == null)
        {
            Debug.LogError("Respawn Point is not set. Please set a respawn point in the inspector.");
        }
    }

    // Cette méthode est appelée lorsqu'un autre collider entre dans le trigger (Killzone)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si l'objet en collision a le tag "Killzone"
        if (other.CompareTag("Killzone"))
        {
            Respawn(); // Réapparaître au point de réapparition
        }
    }

    // Fonction pour réapparaître le joueur au point de réapparition
    void Respawn()
    {
        // Déplacer le joueur au point de réapparition
        rb.velocity = Vector2.zero; // Réinitialiser la vitesse pour éviter des mouvements résiduels
        transform.position = respawnPoint.position; // Déplacer le joueur au point de réapparition
    }
}
