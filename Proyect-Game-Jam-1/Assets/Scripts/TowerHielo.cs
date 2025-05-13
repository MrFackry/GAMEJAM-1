using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TowerHielo : TowerIA
{

    [SerializeField] GameObject Tower;
    [SerializeField] int count;
    [SerializeField] bool isFrozen;
    [SerializeField] float frozenDuration = 10f;
    [SerializeField] 
    private EnemyAI enemyAI;

    public override void Attack()
    {
        count++;
        if (count == 1 && !isFrozen)
        {
            foreach (var item in currentTargets)
            {
                if (item.GetComponent<EnemyAI>().agent.speed != 0)
                {
                    enemyAI = item.GetComponent<EnemyAI>();
                    StartCoroutine(Congelar(item.GetComponent<EnemyAI>()));
                }
            }
            count=0;
        }
        base.Attack();
    }

    IEnumerator Congelar(EnemyAI enemyAI)
    {

        Debug.Log("congelado");
        isFrozen = true;
        float originalSpeed = enemyAI.agent.speed;
        enemyAI.agent.speed = 0;
        yield return new WaitForSeconds(frozenDuration);
        isFrozen = false;
        Debug.Log("desacongelado");
        enemyAI.agent.speed = originalSpeed;
        count = 0;
        StopAllCoroutines();
    }
}