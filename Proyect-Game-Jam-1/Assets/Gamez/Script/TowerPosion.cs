using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                StartCoroutine(ApplySlow(enemy));
            }
        }
    }

    IEnumerator ApplySlow(EnemyStats enemy)
    {
        float originalSpeed = enemy.speed; // Asumiendo que EnemyStats tiene 'speed'
        enemy.speed *= (1f - slowAmount); // Aplica ralentización del 20%
        yield return new WaitForSeconds(slowDuration);
        enemy.speed = originalSpeed; // Restaura velocidad
    }
}
