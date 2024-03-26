using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerupPrefab;

    private float spawnLimit = 8f;

    private int enemiesInScene;
    private int enemiesPerWave = 1;
    private int totalEnemiesDefeated;
    [SerializeField] private int totalEnemiesToDefeatToWinLife = 10;

    private GameManager gameManager;
    private UIGame uiGame;
    private PlayerController playerController;

    private bool powerupInScene;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiGame = FindObjectOfType<UIGame>();
        playerController = FindObjectOfType<PlayerController>();

        powerupInScene = false;
        enemiesPerWave = 1;
        SpawnEnemyWave(enemiesPerWave);
    }

    private void Update()
    {
        //enemiesInScene = FindObjectsOfType<Enemy>().Length;
        
        if (enemiesInScene <= 0 && !gameManager.GetIsGameOver())
        {
            enemiesPerWave++;
            if (!gameManager.GetIsGameOver())
            {
                SpawnEnemyWave(enemiesPerWave);
            }  
        }
        
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateRandomPosition(), Quaternion.identity);
            enemiesInScene++;
        }
        
        uiGame.UpdateEnemiesInSceneText(enemiesInScene, enemiesPerWave);
        uiGame.UpdateCurrentWaveText(enemiesPerWave);

        if (!powerupInScene && enemiesToSpawn > 1)
        {
            Instantiate(powerupPrefab,
                GenerateRandomPosition(), 
                Quaternion.identity);

            powerupInScene = true;
        }
    }

    public void PowerupFinished()
    {
        powerupInScene = false;
    }

    public void EnemyDestroyed()
    {
        enemiesInScene--;
        totalEnemiesDefeated++;

        if (totalEnemiesDefeated % totalEnemiesToDefeatToWinLife == 0)
        {
            playerController.WinLife();
        }
        
        uiGame.UpdateEnemiesInSceneText(enemiesInScene, enemiesPerWave);
    }

    private Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(-spawnLimit, spawnLimit);
        float z = Random.Range(-spawnLimit, spawnLimit);

        return new Vector3(x, 0, z);
    }

    public int GetWave()
    {
        return enemiesPerWave;
    }
    
    public int GetTotalEnemiesDefeated()
    {
        return totalEnemiesDefeated;
    }
    
    public int GetEnemiesInScene()
    {
        return enemiesInScene;
    }
}
