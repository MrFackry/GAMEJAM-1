using System.Collections;
using UnityEngine;

public class ArcherTowerIA : TowerIA
{
    public int boostedAttackMultiplier = 2; // El ataque potenciado hace el doble de daño
    private int attackCounter = 0;

    // Sobrescribimos el método Attack
    public new void Attack()
    {
        if (currentTarget == null) return;

        attackCounter++;

        if (attackCounter == 5)
        {
            // Ataque potenciado
            currentTarget.TakeDamage(damage * boostedAttackMultiplier);
            attackCounter = 0; // Reinicia el contador
        }
        else
        {
            // Ataque normal
            currentTarget.TakeDamage(damage);
        }
    }

    // Sobrescribimos la corrutina para usar el nuevo Attack
    IEnumerator corrutineAttack()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(timeShoot);
        }
    }

    // Start es igual, pero iniciamos la corrutina local
    void Start()
    {
        StartCoroutine(corrutineAttack());
    }
}