using UnityEngine;

public class TowerClickDetector : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log($"✅ Torre {gameObject.name} seleccionada.");
    }
}

