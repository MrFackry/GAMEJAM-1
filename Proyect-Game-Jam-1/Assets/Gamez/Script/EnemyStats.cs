using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 10; // Vida base del enemigo
    public int currentHealth; // Vida actual del enemigo

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemigo recibió {damage} de daño. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            FindObjectOfType<HealthSystem>().EnemyDestroyed(gameObject);
        }
    }
}

