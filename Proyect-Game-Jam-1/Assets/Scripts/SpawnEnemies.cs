using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    //Atributos privados
    private List<GameObject> PoolEnemies = new List<GameObject>();
    private int poolSize = 10;
    //Atributos Serializados
    [SerializeField] GameObject prefabEnemy;
    [SerializeField] int spawnRate;
    [SerializeField] GameObject spawnPoint1;
    [SerializeField] GameObject spawnPoint2;
    [SerializeField] GameObject spawnPoint3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddToPool(poolSize);
        StartCoroutine(SpawnRutine());
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
            GameObject prefab;//intanciamos un GameObject para instanciar los prefabs
            prefab = Instantiate(prefabEnemy, SpawnPoint(), Quaternion.identity);//se instancia el prefab en el punto de spawn
            prefab.SetActive(false);//se desactiva para usarce cuando sea necesario
            // Lo agrega a la lista de la piscina
            PoolEnemies.Add(prefab);
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
        GameObject newEnemy = Instantiate(prefabEnemy, SpawnPoint(), Quaternion.identity);
        newEnemy.SetActive(false);
        PoolEnemies.Add(newEnemy);
        return newEnemy;
    }
    //metodo para crear una rutina de spawn
    public IEnumerator SpawnRutine()
    {
        while (true)
        {
            int activeCount = PoolEnemies.Count(enemy => enemy.activeInHierarchy);//contador que cuenta cuantos enemigos estan activos
            //si el contador es menor va a spawnear a los enemigos
            if (activeCount < poolSize)
            {
                GameObject enemy = SpawnEnemy();
                enemy.transform.position = SpawnPoint();
                enemy.SetActive(true);
            }
            yield return new WaitForSeconds(spawnRate);
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
}
