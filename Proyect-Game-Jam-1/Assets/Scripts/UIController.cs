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
    private SpawnEnemies spawnEnemies;

    public bool IsGameOver=false;
    public bool isPlay=false;
    private Button play;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnEnemies= FindFirstObjectByType<SpawnEnemies>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame(){
        panelMenu.SetActive(false);
        panelGemplay.SetActive(true);
        isPlay=true;
    }
    public void GameOver(){

    }
    public void ResetGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } 
    public void SumarScore(int score){
        int multScore = score*2;
        textScore.text = "Score:"+multScore;
    }
    public void CountEnemies(){
       int count = spawnEnemies.EnemysEnable();
       textEnemies.text = "Enemies:"+count;
    }
    public void CountWave(){
        int count=spawnEnemies.wave;
        textWeve.text="Waves:"+count;
    }
}
