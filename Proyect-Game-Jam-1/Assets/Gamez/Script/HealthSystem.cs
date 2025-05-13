using UnityEngine;
using UnityEngine.UI; // Necesario para usar Slider

public class HealthSystem : MonoBehaviour
{
    public int playerHealth = 200; // Vida inicial del jugador
    public Slider healthSlider; // Referencia al Slider de la UI

    private EconomySystem economySystem;
    private SpawnEnemies spawnEnemies;

    void Start()
    {
        economySystem = FindFirstObjectByType<EconomySystem>();
        spawnEnemies = FindFirstObjectByType<SpawnEnemies>();

        // Inicializar el Slider
        if (healthSlider != null)
        {
            healthSlider.maxValue = playerHealth; // Establece el valor máximo de la barra
            healthSlider.value = playerHealth;    // Inicializa la barra con la vida completa
        }

        Debug.Log($"Inicio del juego. Vida del jugador: {playerHealth}");
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        economySystem.EnemyDefeated(enemy); // Recompensa monetaria del jugador
        spawnEnemies.DisablePrefabs(enemy);
    }

    public void TakeDamage(GameObject enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            int remainingHealth = enemyStats.currentHealth;

            if (remainingHealth > 0)
            {
                playerHealth -= remainingHealth; // Resta la vida del jugador
                
                // Actualiza la barra de vida
                if (healthSlider != null)
                {
                    healthSlider.value = playerHealth; 
                }

                Debug.Log($"Enemigo alcanzó la base con {remainingHealth} de vida. Vida del jugador: {playerHealth}");

                if (playerHealth <= 0)
                {
                    Debug.Log("¡El jugador ha sido derrotado!");
                    // Lógica de fin de juego aquí
                }
            }
        }
    }
}

