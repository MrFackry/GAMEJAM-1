using System.Collections;
using UnityEngine;
public class TowerFego : TowerIA
{
    private int count;
    private bool isBurning = false;
    private float burnDuration = 3f;
    private float damageMultiplier = 1.3f;
    private float originalDamage;
    [SerializeField] GameObject Tower;

    public override void Attack() {
        count++;
        if (count == 3 && !isBurning) {
            StartCoroutine(Quemar());
            count = 0;
        }
        base.Attack(); // Usa el daño actual (aumentado o no)
    }
    IEnumerator Quemar()
    {
        isBurning = true;
        originalDamage = damage; // Guarda el daño base
        damage = Mathf.RoundToInt(damage * damageMultiplier); // Aplica aumento

        yield return new WaitForSeconds(burnDuration);

        damage = Mathf.RoundToInt(originalDamage); // Restaura daño original
        isBurning = false;
    }
}