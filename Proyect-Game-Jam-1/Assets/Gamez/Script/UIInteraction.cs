using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{
    public Button myButton; // Referencia al botón en la UI

    void Start()
    {
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick); // Agrega el evento de clic
        }
        else
        {
            Debug.LogError("⚠ El botón no está asignado en el Inspector.");
        }
    }

    void OnButtonClick()
    {
        Debug.Log("✅ ¡Botón de la UI presionado!");
        // Aquí puedes agregar la lógica para seleccionar una torre o ejecutar otra acción
    }
}

