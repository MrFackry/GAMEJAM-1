using UnityEngine;

public class TowerSellOnClick : MonoBehaviour
{
    private EconomySystem economySystem;

    void Start()
    {
        economySystem = FindAnyObjectByType<EconomySystem>();
        if (economySystem == null)
        {
            Debug.LogError("No se encontró EconomySystem en la escena.");
        }
    }

    void OnMouseDown()
    {
        if (economySystem != null)
        {
            economySystem.SellTower(gameObject);
            Destroy(gameObject); // Elimina la torre de la escena
        }
    }
}
