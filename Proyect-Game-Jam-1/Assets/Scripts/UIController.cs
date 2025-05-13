using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject panelMenu;
    [SerializeField] GameObject panelGemplay;
    [SerializeField] GameObject panelGameOver;
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI textWeve;
    [SerializeField] TextMeshProUGUI textEnemies;
    [SerializeField] TextMeshProUGUI textCoins;
    [SerializeField] EconomySystem economySystem;
    private SpawnEnemies spawnEnemies;
    private UIController uiController;
    private int totalScore=0;
    public bool IsGameOver = false;
    public bool isPlay = false;
    private Button play;
    void Awake()
    {
        spawnEnemies = FindFirstObjectByType<SpawnEnemies>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnEnemies= FindFirstObjectByType<SpawnEnemies>();
        uiController = FindFirstObjectByType<UIController>(); // Encuentra el objeto en la escena
        CountWave();
        CountCoint();
    }

    // Update is called once per frame
    void Update()
    {
        CountEnemies();
    }

    public void PlayGame()
    {
        panelMenu.SetActive(false);
        panelGemplay.SetActive(true);
        isPlay = true;
        if (spawnEnemies != null)
        {
            StartCoroutine(spawnEnemies.SpawnRutine());
        }
    }
    public void GameOver(){
         
    {
        Debug.Log("¡El jugador ha sido derrotado!");
        uiController.IsGameOver = true; // Marca el juego como terminado
        uiController.panelGameOver.SetActive(true); // Muestra el panel de Game Over
    }

    }
    public void ResetGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SumarScore(int score)
    {
        totalScore += score * 2;
        textScore.text = "Score:" + totalScore;
    }
    public void CountEnemies()
    {
        int count = spawnEnemies.EnemysEnable();
        Debug.Log("Cantidad de enemigos activos: " + count);
        textEnemies.text = "Enemies:" + count;
    }
    public void CountWave()
    {
        int count = spawnEnemies.wave;
        textWeve.text = "Waves:" + count;
    }
    public void CountCoint()
    {
        if (economySystem != null && textCoins != null)
        {
            textCoins.text = "Coins:" + economySystem.playerCoins;
        }
        else
        {
            Debug.LogWarning("CountCoint no se pudo ejecutar: economySystem o textCoins es null.");
        }
    }
}
