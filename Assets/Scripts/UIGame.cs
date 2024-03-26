using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIGame : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    
    [SerializeField] private Button pauseResumeButton;
    [SerializeField] private Button pauseRestartButton;
    [SerializeField] private Button pauseGoToMainMenuButton;

    [SerializeField] private GameObject gameOverPanel;
    
    [SerializeField] private Button gameOverRestartButton;
    [SerializeField] private Button gameOverGoToMainMenuButton;

    [SerializeField] private TextMeshProUGUI totalWavesText;
    [SerializeField] private TextMeshProUGUI totalEnemiesDefeatedText;
    [SerializeField] private TextMeshProUGUI enemiesToNextWaveText;
    [SerializeField] private TextMeshProUGUI totalPowerupsText;

    [SerializeField] private GameObject gamePanel;
    
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI enemiesInSceneText;

    [SerializeField] private GameObject[] lives;
    
    private GameManager gameManager;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        pauseResumeButton.onClick.AddListener(() => { gameManager.UnPause(); });
        pauseRestartButton.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        pauseGoToMainMenuButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
        
        gameOverRestartButton.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        gameOverGoToMainMenuButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
    }

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }
    
    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }
    
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    
    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGamePanel()
    {
        gamePanel.SetActive(true);
    }
    
    public void HideGamePanel()
    {
        gamePanel.SetActive(false);
    }
    
    public void UpdateTotalWavesText(int totalWaves)
    {
        totalWavesText.text = $"Total waves: {totalWaves}";
    }
    
    public void UpdateTotalEnemiesDefeated(int totalEnemies)
    {
        totalEnemiesDefeatedText.text = $"Total enemies defeated: {totalEnemies}";
    }
    
    public void UpdateEnemiesToNextWaveText(int enemiesToNextWave)
    {
        enemiesToNextWaveText.text = $"Enemies to next wave: {enemiesToNextWave}";
    }
    
    public void UpdateTotalPowerupsText(int totalPowerUps)
    {
        totalPowerupsText.text = $"Total powerups: {totalPowerUps}";
    }

    public void UpdateEnemiesInSceneText(int enemiesInScene, int enemiesPerWave)
    {
        enemiesInSceneText.text = $"{enemiesInScene} / {enemiesPerWave}";
    }
    
    public void UpdateCurrentWaveText(int currentWave)
    {
        currentWaveText.text = $"Wave: {currentWave}";
    }

    public void ShowLives(int livesToShow)
    {
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].SetActive(i < livesToShow);
        }
    }
}
