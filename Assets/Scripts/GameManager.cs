using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIGame uiGame;
    private SpawnManager spawnManager;
    private PlayerController playerController;
    
    private bool isGameOver;

    private void Start()
    {
        Time.timeScale = 1;

        isGameOver = false;
        
        uiGame = FindObjectOfType<UIGame>();
        spawnManager = FindObjectOfType<SpawnManager>();
        playerController = FindObjectOfType<PlayerController>();
        
        uiGame.ShowGamePanel();
        uiGame.HidePausePanel();
        uiGame.HideGameOverPanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // TODO: ARREGLAR QUE LOS ENEMIGOS Y PLAYER NO SE PARAN CON PAUSE
            // * Fuerza a 0
            // * Bool que controla pause / no pause
            // * Desactivar rigidbody
            // * Desactivar la componente PlayerController / Enemy
            
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0; 
        uiGame.HideGamePanel();
        uiGame.ShowPausePanel();
    }

    public void UnPause()
    {
        Time.timeScale = 1; 
        uiGame.ShowGamePanel();
        uiGame.HidePausePanel();
    }

    public void GameOver()
    {
        // TODO: CONSEGUIR PARAR ENEMIGOS CUANDO GAME OVER
        isGameOver = true;
        
        uiGame.HideGamePanel();
        
        uiGame.UpdateTotalWavesText(spawnManager.GetWave());
        uiGame.UpdateTotalEnemiesDefeated(spawnManager.GetTotalEnemiesDefeated());
        uiGame.UpdateEnemiesToNextWaveText(spawnManager.GetEnemiesInScene());
        uiGame.UpdateTotalPowerupsText(playerController.GetTotalPowerups());
        uiGame.ShowGameOverPanel();
    }
    
    public bool GetIsGameOver()
    {
        return isGameOver;
    }
}
