using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    //Atributos privados
    private EconomySystem economySystem;
    private HealthSystem healthSystem;
    private EnemyStats enemyStats;
    private List<GameObject> PoolEnemies = new List<GameObject>();
    private int poolSize = 10;
    public int wave = 1;
    private int enemiesPerWave = 3;
    private bool isSpawningWave = false;
    private int count = 0;
    private UIController uIController;

    //Atributos Serializados
    [SerializeField] List<GameObject> prefabEnemy = new List<GameObject>();
    [SerializeField] GameObject boss;
    [SerializeField] int spawnRate;
    [SerializeField] GameObject spawnPoint1;
    [SerializeField] GameObject spawnPoint2;
    [SerializeField] GameObject spawnPoint3;


    void Awake()
    {
        economySystem = FindFirstObjectByType<EconomySystem>();
        healthSystem = FindFirstObjectByType<HealthSystem>();
        uIController = FindFirstObjectByType<UIController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddToPool(poolSize);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //metodo para selecionar un punto de spawn random
    public Vector3 SpawnPoint()
    {

        int randomPoint = Random.Range(1, 4);
        Vector3 selectPoint = new Vector3();
        if (randomPoint == 1)
        {
            return selectPoint = spawnPoint1.transform.position;
        }
        else if (randomPoint == 2)
        {
            return selectPoint = spawnPoint2.transform.position;
        }
        else if (randomPoint == 3)
        {
            return selectPoint = spawnPoint3.transform.position;
        }
        return selectPoint;
    }
    //metodo paraagregar los enemigos a la picina 
    public void AddToPool(int maxPrefab)
    {
        //creamos un for para que se agregen enemigos al pool deacuerdo al limite de objetos
        for (int i = 0; i < maxPrefab; i++)
        {
            AuxAddEnemy();
        }
    }

    //metodo para el Spawn de los enemigos
    public GameObject SpawnEnemy()
    {
        foreach (GameObject Enemy in PoolEnemies)
        {
            if (!Enemy.activeInHierarchy)
            {
                return Enemy;
            }
        }
        // Si no hay enemigos disponibles, crear uno nuevo, agregarlo a la pool y devolverlo
        GameObject newEnemy = Instantiate(prefabEnemy[Random.Range(0, prefabEnemy.Count)], SpawnPoint(), Quaternion.identity);
        newEnemy.SetActive(false);
        PoolEnemies.Add(newEnemy);
        return newEnemy;
    }
    //metodo auxiliar para el agregar de enemigos
    public void AuxAddEnemy()
    {
        foreach (GameObject enemy in prefabEnemy)
        {
            GameObject prefab;//intanciamos un GameObject para instanciar los prefabs
            prefab = Instantiate(enemy, SpawnPoint(), Quaternion.identity);//se instancia el prefab en el punto de spawn
            prefab.SetActive(false);//se desactiva para usarce cuando sea necesario
            PoolEnemies.Add(prefab);// Lo agrega a la lista de la piscina
        }
    }
    //metodo para crear una rutina de spawn
    public IEnumerator SpawnRutine()
    {
        while (true)
        {
            if (!isSpawningWave)
            {
                isSpawningWave = true;
                yield return StartCoroutine(SpawnWave());
            }
            yield return null;
        }
    }
    //metodo para crear una rutina de spawn
    public IEnumerator SpawnWave()
    {

        Debug.Log($"Iniciando oleada {wave}");

        int enemiesToSpawn = enemiesPerWave;
        bool spawnBoss = wave % 5 == 0;
        bool maxBoss = true;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = SpawnEnemy(); // retorna un enemigo
            enemy.transform.position = SpawnPoint();

            EnemyStats stats = enemy.GetComponent<EnemyStats>();
            if (stats != null)
            {
                stats.maxHealth += wave * 2;  // Aumenta la vida base segun la oleada
                stats.currentHealth = stats.maxHealth;
                Debug.Log("-----------------");
                Debug.Log("Vida de enemigo: " + stats.gameObject.name + " vida:" + stats.maxHealth);
                Debug.Log("-----------------");
            }

            enemy.SetActive(true);

            if (spawnBoss && maxBoss)
            {
                maxBoss = false;
                GameObject enemyBoss = Instantiate(boss, SpawnPoint(), Quaternion.identity);

                EnemyStats bossStats = enemyBoss.GetComponent<EnemyStats>();
                if (bossStats != null)
                {
                    bossStats.maxHealth += wave * 5; // Más vida para el jefe
                    Debug.Log("-----------------");
                    Debug.Log("Vida de enemigo: " + boss.gameObject.name + " vida:" + bossStats.maxHealth);
                    Debug.Log("-----------------");
                    bossStats.currentHealth = bossStats.maxHealth;
                }

                enemyBoss.SetActive(true);
            }

            yield return new WaitForSeconds(spawnRate);
        }

        yield return new WaitUntil(() => EnemysEnable() == 0);

        wave++;
        enemiesPerWave += 1;
        isSpawningWave = false;
        NotifyUI();
    }

    private void NotifyUI()
    {
        if (uIController != null)
        {
            uIController.CountEnemies();
            uIController.CountWave();
        }
    }

    //metodos para contar los enemigos activos
    public int EnemysEnable()
    {
        int count = 0;
        foreach (GameObject prefab in PoolEnemies)
        {
            if (prefab.activeInHierarchy)
            {
                count++;
            }
        }
        return count;

    }

    //metodo para desactivar un enemigo
    public void DisablePrefabs(GameObject Enemy)
    {
        count++;
        Enemy.SetActive(false);
        uIController.SumarScore(count);
        economySystem.EnemyDefeated(Enemy);
        healthSystem.EnemyDestroyed(Enemy);
        uIController.CountEnemies();
    }
}
