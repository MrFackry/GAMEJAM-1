using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Necesario para modificar la velocidad de los enemigos

public class TowerPosion : TowerIA
{
    public float slowDuration = 5f; // Duración de la ralentización en segundos
    public float slowAmount = 0.2f; // 20% de ralentización
    public float slowInterval = 10f; // Cada 10 segundos

    void Start()
    {
        StartCoroutine(corrutineAttack());
        StartCoroutine(SlowEffectRoutine());
    }

    IEnumerator SlowEffectRoutine()
    {
        while (true)
        {
            ApplySlowToEnemies();
            yield return new WaitForSeconds(slowInterval);
        }
    }

    void ApplySlowToEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (var col in colliders)
        {
            EnemyAI enemyAI = col.GetComponent<EnemyAI>(); // Buscamos el script EnemyAI
            if (enemyAI != null)
            {
                StartCoroutine(ApplySlow(enemyAI));
            }
        }
    }

    IEnumerator ApplySlow(EnemyAI enemyAI)
    {
        NavMeshAgent agent = enemyAI.GetComponent<NavMeshAgent>(); // Obtenemos el NavMeshAgent
        if (agent != null)
        {
            float originalSpeed = agent.speed; // Guardamos la velocidad original
            agent.speed *= (1f - slowAmount); // Aplicamos la ralentización del 20%
            yield return new WaitForSeconds(slowDuration);
            agent.speed = originalSpeed; // Restauramos la velocidad original
        }
    }
}

