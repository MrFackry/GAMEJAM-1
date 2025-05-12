using System.Collections.Generic;
using UnityEngine;

public class PutUnits : MonoBehaviour
{
    //atributos serializados
    [SerializeField] GameObject unitPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        Debug.Log("se hizo clic en la plataforma");
        PutUnit(unitPrefab);
    }

    public void PutUnit(GameObject unit){
        GameObject newUnit = Instantiate(unit,transform.position,Quaternion.identity);
        Debug.Log("Unidad"+ newUnit+ "newUnit instanciada en la posición: " + transform.position);
    }
}
