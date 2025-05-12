using UnityEngine;
using UnityEngine.AI; // Necesario para usar NavMeshAgent

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f; // Velocidad del enemigo
    public GameObject player; // Referencia al jugador
    private NavMeshAgent agent; // Componente NavMeshAgent

    void Start()
    {
        // Obtiene el componente NavMeshAgent del enemigo
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed; // Establece la velocidad del agente
    }

    void Update()
    {
        if (player != null)
        {
            // Configura el destino del enemigo hacia el jugador
            agent.SetDestination(player.transform.position);
        }
    }

   /* void OnTriggerEnter(Collider other)
    {
        // Verifica si el enemigo alcanzó al jugador
        if (other.gameObject == player)
        {
            // Obtiene la vida del jugador
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            // Si el jugador tiene un sistema de vida, aplicamos daño
            if (playerHealth != null)
            {
                int damage = GetComponent<EnemyDifficulty>().damageValue; // Daño del enemigo
                playerHealth.TakeDamage(damage);
            }

            // Destruye al enemigo después de hacer daño
            Destroy(gameObject);
        }
    }*/
}




