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
    private int totalScore = 0;

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
        if (textCoins == null) Debug.LogError("textCoins está null en UIController.");
        if (economySystem == null) Debug.LogError("economySystem está null en UIController.");
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
    public void GameOver()
    {

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
