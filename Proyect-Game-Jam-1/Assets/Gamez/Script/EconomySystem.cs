using UnityEngine;

public class EconomySystem : MonoBehaviour
{
    public int playerCoins = 100; // Monedas iniciales del jugador
    public int roundReward = 50; // Recompensa por completar la ronda
    public GameObject[] enemies; // Array público de enemigos

    void Start()
    {
        Debug.Log("Inicio del juego. Monedas del jugador: " + playerCoins);
    }

    public void EnemyDefeated(GameObject enemy)
    {
        int reward = GetRewardByTag(enemy.tag); // Obtiene la recompensa según el tag
        playerCoins += reward; // Suma las monedas al total del jugador

        Debug.Log($"Eliminaste un enemigo tipo {enemy.tag}. Ganaste {reward} monedas. Total: {playerCoins}");

        //enemy.SetActive(false); // Destruye al enemigo tras ser derrotado
    }

    private int GetRewardByTag(string enemyTag)
    {
        switch (enemyTag)
        {
            case "Enemy":
                return 5;
            case "Rapido":
                return 7;
            case "Boss":
                return 20;
            default:
                Debug.LogWarning($"El enemigo con tag {enemyTag} no tiene recompensa asignada.");
                return 0; // Si el tag no está definido, no se otorgan monedas
        }
    }

    public void EndRound()
    {
        playerCoins += roundReward; // Se suman las monedas de recompensa por ronda
        Debug.Log($"Ronda terminada. Recibiste {roundReward} monedas. Total: {playerCoins}");
    }
}