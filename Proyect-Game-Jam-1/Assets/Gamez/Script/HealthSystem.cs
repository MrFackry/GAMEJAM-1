using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int playerHealth = 200; // Vida inicial del jugador
    private EconomySystem economySystem;

    void Start()
    {
        economySystem = FindFirstObjectByType<EconomySystem>();
        Debug.Log($"Inicio del juego. Vida del jugador: {playerHealth}");
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            int remainingHealth = enemyStats.currentHealth; // Vida restante del enemigo
            playerHealth -= remainingHealth; // Se descuenta de la vida del jugador
            Debug.Log($"Enemigo destruido con {remainingHealth} de vida restante. Vida del jugador: {playerHealth}");

            if (playerHealth <= 0)
            {
                Debug.Log("¡El jugador ha sido derrotado!");
                // Aquí puedes agregar lógica de fin de juego
            }
        }

        economySystem.EnemyDefeated(enemy); // Recompensa monetaria del jugador
        enemy.SetActive(false);
    }
}
