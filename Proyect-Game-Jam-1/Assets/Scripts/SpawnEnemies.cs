using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    //Atributos privados
    private EconomySystem economySystem;
    private HealthSystem healthSystem;
    private List<GameObject> PoolEnemies = new List<GameObject>();
    private int poolSize = 10;
    private int wave = 4;
    private int enemiesPerWave = 3;
    private int baseHealth = 10;
    private bool isSpawningWave = false;

    //Atributos Serializados
    [SerializeField] List<GameObject> prefabEnemy = new List<GameObject>();
    [SerializeField] GameObject boss;
    [SerializeField] int spawnRate;
    [SerializeField] GameObject spawnPoint1;
    [SerializeField] GameObject spawnPoint2;
    [SerializeField] GameObject spawnPoint3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddToPool(poolSize);
        StartCoroutine(SpawnRutine());
        economySystem = FindFirstObjectByType<EconomySystem>();
        healthSystem = FindFirstObjectByType<HealthSystem>();
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
        return null;
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
        bool maxBoss=true;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = SpawnEnemy();
            enemy.transform.position = SpawnPoint();
            enemy.SetActive(true);
            if (spawnBoss&&maxBoss)
            {
                maxBoss=false;
                GameObject enemyBoss = boss;
                enemyBoss= Instantiate(boss,SpawnPoint(),Quaternion.identity);
                enemyBoss.SetActive(true);
            }
            yield return new WaitForSeconds(spawnRate);
        }

        // Espera a que todos los enemigos mueran
        yield return new WaitUntil(() => EnemysEnable() == 0);

        // Prepara siguiente oleada
        wave++;
        maxBoss=true;
        enemiesPerWave += 1;
        baseHealth += 5;
        isSpawningWave = false;
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
        Enemy.SetActive(false);
        economySystem.EnemyDefeated(Enemy);
        healthSystem.EnemyDestroyed(Enemy);
    }
}
