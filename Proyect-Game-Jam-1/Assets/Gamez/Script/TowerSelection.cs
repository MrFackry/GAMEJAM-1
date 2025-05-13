using System.Collections.Generic;
using UnityEngine;

public class TowerSelection : MonoBehaviour
{
    [SerializeField] List<GameObject> TowerList = new List<GameObject>(); // Lista de prefabs de torres
    public static GameObject selectedTower; // Torre actualmente seleccionada

    public void SelectTower(int index)
    {
        if (index >= 0 && index < TowerList.Count)
        {
            selectedTower = TowerList[index];
            Debug.Log($"✅ Torre seleccionada: {selectedTower.name}");
        }
        else
        {
            Debug.LogWarning("⚠ No se ha seleccionado una torre válida.");
        }
    }
}


