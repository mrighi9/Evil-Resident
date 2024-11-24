using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int health = 150; // Default health, can be randomized or set via another script

    void Start()
    {
        // Optionally, randomize health between 100 and 150
        health = Random.Range(100, 151);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining HP: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        // Play death animation or effects here
        Destroy(gameObject); // Remove the zombie from the scene
    }
}
