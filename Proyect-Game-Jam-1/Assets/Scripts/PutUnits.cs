using System.Collections.Generic;
using UnityEngine;

public class PutUnits : MonoBehaviour
{
    private EconomySystem economySystem;

    void Start()
    {
        economySystem = FindFirstObjectByType<EconomySystem>();
    }

    void OnMouseDown()
    {
        if (TowerSelection.selectedTower != null && economySystem.CanAfford(TowerSelection.selectedTower))
        {
            economySystem.PayTower(TowerSelection.selectedTower);
            PutUnit(TowerSelection.selectedTower);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas o no seleccionaste una torre.");
        }
    }

    public void PutUnit(GameObject unit)
    {
        Vector3 y = new Vector3(0,3,0);
        GameObject newUnit = Instantiate(unit, transform.position+y, Quaternion.identity);
        Debug.Log("Unidad " + newUnit.name + " instanciada en la posición: " + transform.position);
    }
}

