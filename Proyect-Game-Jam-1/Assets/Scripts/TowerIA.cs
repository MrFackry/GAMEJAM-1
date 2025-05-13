using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class TowerIA : MonoBehaviour, TowerAttack
{
    [SerializeField] protected float range;
    [SerializeField] protected int timeShoot;
    [SerializeField] protected int damage;
    [SerializeField] protected int price;
    protected EnemyStats currentTarget;
    protected List<EnemyStats> currentTargets = new List<EnemyStats>();
    protected Transform rotationLook;
    protected EconomySystem economySystem;

    public virtual void Attack()
    {
        if (currentTarget != null)
            currentTarget.TakeDamage(damage);
    }

    protected virtual void EnemyDetection()
    {
        var enemies = Physics.OverlapSphere(transform.position, range)
            .Select(c => c.GetComponent<EnemyStats>())
            .Where(e => e != null)
            .ToList();

        currentTargets = enemies;

        currentTarget = currentTargets.Count > 0 ? currentTargets[0] : null;
    }

    protected virtual void LookEnemy()
    {
        if (currentTarget != null&& rotationLook != null)
            rotationLook.LookAt(currentTarget.transform);
            
    }

    protected virtual IEnumerator CoroutineAttack()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(timeShoot);
        }
    }

    protected virtual void Start()
    {
        StartCoroutine(CoroutineAttack());
    }

    protected virtual void Update()
    {
        EnemyDetection();
        LookEnemy();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public int GetPrice() => price;
}

