using UnityEngine;
public class DisableEnemyByClic:MonoBehaviour{
    private SpawnEnemies spawnEmSpawnEnemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnEmSpawnEnemies = FindFirstObjectByType<SpawnEnemies>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//generamos un rayCastdesde la camara hasta el punto que queremos eliminar
            RaycastHit hit;//guarda el punto de colicion 
            if (Physics.Raycast(ray, out hit))//lanza el ray y comprueba si hay colicion 
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    spawnEmSpawnEnemies.DisablePrefabs(hit.collider.gameObject);//desatibamos el prefab al que le hicimos click
                }  
            }
        }
    }
}