using UnityEngine;
using UnityEngine.AI; // Necesario para usar NavMeshAgent

public class EnemyAI : MonoBehaviour
{
    //public float speed = 2f; // Velocidad del enemigo
    private GameObject player; // Referencia al jugador

    private NavMeshAgent agent; // Componente NavMeshAgent
    private SpawnEnemies spawnEnemies;
    void Start()
    {
        // Obtiene el componente NavMeshAgent del enemigo
        agent = GetComponent<NavMeshAgent>();
        spawnEnemies = FindFirstObjectByType<SpawnEnemies>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
            // Configura el destino del enemigo hacia el jugador
            agent.SetDestination(player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("COLICION CON" + other.gameObject.name);
        // Verifica si el enemigo alcanzó al jugador
        if (other.CompareTag("Player"))
        {
            // Obtiene la vida del jugador
            /*PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            // Si el jugador tiene un sistema de vida, aplicamos daño
            if (playerHealth != null)
            {
                int damage = GetComponent<EnemyDifficulty>().damageValue; // Daño del enemigo
                playerHealth.TakeDamage(damage);
            }*/

            // Destruye al enemigo después de hacer daño
            spawnEnemies.DisablePrefabs(gameObject);
        }
    }
}