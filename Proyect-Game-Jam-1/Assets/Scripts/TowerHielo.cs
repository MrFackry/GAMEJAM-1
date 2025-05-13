using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class TowerHielo : TowerIA
{

    [SerializeField] GameObject Tower;
    private int count;
    private bool isFrozen;
    private float frozenDuration = 10f;
    private EnemyAI enemyAI;

    public override void Attack()
    {
        count++;
        if (count == 5 && !isFrozen)
        {
            enemyAI = FindFirstObjectByType<EnemyAI>();
            StartCoroutine(Congelar(enemyAI));
            count = 0;
        }
        base.Attack();
    }

    IEnumerator Congelar(EnemyAI enemyAI)
    {
        if (count == 10 && !isFrozen)
        {
            NavMeshAgent agent = enemyAI.GetComponent<NavMeshAgent>();
            isFrozen = true;
            float originalSpeed = agent.speed;
            agent.speed = 0;
            yield return new WaitForSeconds(frozenDuration);
            isFrozen = false;
            agent.speed = originalSpeed;
        }
    }
}