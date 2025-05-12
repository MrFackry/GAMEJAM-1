using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class TowerIA : MonoBehaviour, TowerAttack
{
    float range;
    public EnemyStats currentTarget;
    int acttack;
    int timeShoot;
    int damage;
    List<EnemyStats>currentTargets = new List<EnemyStats>();
    Transform rotationLook;
    public void Attack()
    {
        currentTarget.TakeDamage(damage);
    }

    public void EnemyDetection(){
        var enemy = Physics.OverlapSphere(transform.position,range).Where(currentEnemy =>currentEnemy.GetComponent<GameObject>()).Select(currentEnemy=> currentEnemy.GetComponent<GameObject>()).ToList();
        if (currentTargets.Count>0)
        {
            currentTarget =currentTargets[0];
        }else if(currentTargets.Count==0)
        {
            currentTarget=null;
        }
    }

    public void lookEnemy(){
        if (currentTarget)
        {
            rotationLook.LookAt(currentTarget.transform);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(corrutineAttack());
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDetection();
    }


    IEnumerator corrutineAttack(){
        
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(timeShoot);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position,range);
    }
}
