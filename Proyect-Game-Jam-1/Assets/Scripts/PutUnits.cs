using System.Collections.Generic;
using UnityEngine;

public class PutUnits : MonoBehaviour
{
    //atributos serializados
    [SerializeField] GameObject unitPrefab;
    private EconomySystem economySystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        economySystem = FindFirstObjectByType<EconomySystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        Debug.Log("se hizo clic en la plataforma");
        if (economySystem.CanAfford(unitPrefab))
        {
            economySystem.PayTower(unitPrefab);
            PutUnit(unitPrefab);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para colocar esta torre.");
        }

    }

    public void PutUnit(GameObject unit)
    {
        GameObject newUnit = Instantiate(unit, transform.position, Quaternion.identity);
        Debug.Log("Unidad" + newUnit + "newUnit instanciada en la posición: " + transform.position);
    }
}
