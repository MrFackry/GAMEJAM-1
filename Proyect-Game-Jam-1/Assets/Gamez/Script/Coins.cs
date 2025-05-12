using UnityEngine;

public class EconomySystem : MonoBehaviour
{
    public int playerCoins = 100; // Monedas iniciales del jugador
    public int roundReward = 50; // Recompensa por completar la ronda
    public GameObject[] enemies; // Array público de enemigos
    public int[] enemyValues; // Array público para definir el valor de cada enemigo

    void Start()
    {
        Debug.Log("Inicio del juego. Monedas del jugador: " + playerCoins);
        AssignEnemyValues(); // Asignamos valores de dificultad manualmente
    }

    void AssignEnemyValues()
    {
        // Verificamos que el tamaño del array de valores coincida con el de enemigos
        if (enemyValues.Length != enemies.Length)
        {
            Debug.LogError("La cantidad de valores de dificultad no coincide con la cantidad de enemigos.");
            return;
        }

        // Asigna la dificultad según la posición en el array
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].CompareTag("Enemy"))
            {
                EnemyDifficulty difficulty = enemies[i].GetComponent<EnemyDifficulty>();
                difficulty.difficultyLevel = enemyValues[i]; // Se asigna el valor manualmente
                Debug.Log($"Asignado nivel {enemyValues[i]} al enemigo {enemies[i].name}");
            }
        }
    }

    public void EnemyDefeated(GameObject enemy)
    {
        // Verifica que el enemigo tenga la etiqueta "Enemy"
        if (enemy.CompareTag("Enemy"))
        {
            int reward = enemy.GetComponent<EnemyDifficulty>().GetReward();
            playerCoins += reward; // Se suman las monedas al total del jugador

            Debug.Log($"Eliminaste un enemigo. Ganaste {reward} monedas. Total: {playerCoins}");
        }
    }

    public void EndRound()
    {
        playerCoins += roundReward; // Se suman las monedas de recompensa
        Debug.Log($"Ronda terminada. Recibiste {roundReward} monedas. Total: {playerCoins}");
    }
}

// Clase que define la dificultad de un enemigo y su recompensa
public class EnemyDifficulty : MonoBehaviour
{
    public int difficultyLevel = 1; // Nivel de dificultad del enemigo

    public int GetReward()
    {
        return difficultyLevel * 10; // Calcula la recompensa en función de la dificultad
    }
}


