using UnityEngine;
using UnityEngine.AI; // Necesario para usar NavMeshAgent

public class EnemyAI : MonoBehaviour
{
    //public float speed = 2f; // Velocidad del enemigo
    private GameObject player; // Referencia al jugador

    public NavMeshAgent agent; // Componente NavMeshAgent
    private SpawnEnemies spawnEnemies;
    private HealthSystem healthSystem;
    void Start()
    {
        // Obtiene el componente NavMeshAgent del enemigo
        agent = GetComponent<NavMeshAgent>();
        spawnEnemies = FindFirstObjectByType<SpawnEnemies>();
        player = GameObject.FindWithTag("Player");
        healthSystem = FindFirstObjectByType<HealthSystem>();
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
            // Si el jugador tiene un sistema de vida, aplicamos daño
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(gameObject);
            }

            // Destruye al enemigo después de hacer daño
            spawnEnemies.DisablePrefabs(gameObject);
        }
    }
}