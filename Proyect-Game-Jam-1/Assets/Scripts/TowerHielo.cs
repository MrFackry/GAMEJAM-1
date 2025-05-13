using System.Collections;
using UnityEngine;
public class TowerHielo:TowerIA{

[SerializeField] GameObject Tower;
private int count;
private bool isFrozen;
private float frozenDuration=10f;
public override void Attack(){
    count++;
    base.Attack();
}

IEnumerator Congelar(){
    if (count==10&&!isFrozen)
    {
        isFrozen=true;
        yield return new WaitForSeconds(frozenDuration);
        isFrozen=false;
    }
}
}