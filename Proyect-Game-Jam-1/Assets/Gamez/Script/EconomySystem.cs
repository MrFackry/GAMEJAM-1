using System;
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
    }
    public void TowerPut(GameObject tower)
    {
        CanAfford(tower); // cobra la recompensa segun el tag
        Debug.Log($"Pusiste la torre {tower.tag}. perdiste {playerCoins} monedas. Total: {playerCoins}");
    }

    private int GetRewardByTag(string enemyTag)
    {
        switch (enemyTag)
        {
            case "Enemy":
                return 10;
            case "EnemyFaster":
                return 20;
            case "EnemyBoss":
                return 50;
            default:
                Debug.LogWarning($"El enemigo con tag {enemyTag} no tiene recompensa asignada.");
                return 0; // Si el tag no esta definido, no se otorgan monedas
        }
    }
    public bool CanAfford(GameObject tower)
    {
        TowerIA towerIA = tower.GetComponent<TowerIA>();
        if (towerIA == null)
        {
            Debug.LogWarning("No se pudo obtener TowerIA del prefab.");
            return false;
        }

        return playerCoins >= towerIA.GetPrice();
    }

    public void PayTower(GameObject tower)
    {
        TowerIA towerIA = tower.GetComponent<TowerIA>();
        if (towerIA != null)
        {
            playerCoins -= towerIA.GetPrice();
            Debug.Log($"Pagaste {towerIA.GetPrice()} monedas. Total restante: {playerCoins}");
        }
    }


    public void EndRound()
    {
        playerCoins += roundReward; // Se suman las monedas de recompensa por ronda
        Debug.Log($"Ronda terminada. Recibiste {roundReward} monedas. Total: {playerCoins}");
    }
}