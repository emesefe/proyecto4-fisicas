using System;
using System.Collections;
using System.Timers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private float speed = 10f;
    private float forwardInput;

    private SpawnManager spawn;
    [SerializeField] private GameObject focalPointGameObject;

    public bool hasPowerup;
    private float powerupForce = 30f;

    [SerializeField] private GameObject[] powerupIndicators;

    private int lives;
    private int maxLives = 3;
    private float lowerLimit = -3f;
    
    private Vector3 initialPosition;

    private int totalPowerups;

    private GameManager gameManager;
    private UIGame uiGame;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        
        hasPowerup = false;
        initialPosition = Vector3.zero;
        
    }

    private void Start()
    {
        spawn = FindObjectOfType<SpawnManager>();
        gameManager = FindObjectOfType<GameManager>();
        uiGame = FindObjectOfType<UIGame>();
        
        lives = maxLives;
        HideAllPowerupIndicators();
    }

    private void Update()
    {
        if (transform.position.y < lowerLimit)
        {
            LoseLife();
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.GetIsGameOver())
        {
            return;
        }
        
        Movement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            StartCoroutine(PowerupCountdown());
            Destroy(other.gameObject);
            totalPowerups++;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            // El enemigo sufre un empujón alejándolo del player
            Rigidbody enemyRigidbody = other.gameObject.
                GetComponent<Rigidbody>();
            
            Vector3 direction = (other.transform.position -
                                 transform.position).normalized;
            
            enemyRigidbody.AddForce(direction * powerupForce,
                ForceMode.Impulse);
        }
    }

    private void Movement()
    {
        forwardInput = Input.GetAxis("Vertical");
        
        playerRigidbody.AddForce(focalPointGameObject.transform.forward * 
                                 speed * forwardInput);
        
        // SI QUEREMOS QUE EL PLAYER FRENE CUANDO NO PULSAMOS VERTICAL INPUT
        // // forwardInput > -0.01f && forwardInput < 0.01f
        // if (Mathf.Abs(forwardInput) < 0.01f)
        // {
        //     playerRigidbody.velocity = Vector3.zero;
        // }
        // else
        // {
        //     playerRigidbody.AddForce(focalPointGameObject.transform.forward * 
        //                              speed * forwardInput);
        // }
    }

    private IEnumerator PowerupCountdown()
    {
        for (int i = 0; i < powerupIndicators.Length; i++)
        {
            powerupIndicators[i].SetActive(true);
            yield return new WaitForSeconds(2);
            powerupIndicators[i].SetActive(false);
        }
    
        hasPowerup = false;
        spawn.PowerupFinished();
    }

    private IEnumerator InvulnerabilityCountdown()
    {
        playerRigidbody.constraints = RigidbodyConstraints.FreezePosition |
            RigidbodyConstraints.FreezeRotation;
        yield return new WaitForSeconds(0.5f);
        playerRigidbody.constraints = RigidbodyConstraints.None;
    }

    private void HideAllPowerupIndicators()
    {
        foreach (GameObject indicator in powerupIndicators)
        {
            indicator.SetActive(false);
        }
    }

    public int GetTotalPowerups()
    {
        return totalPowerups;
    }

    private void LoseLife()
    {
        lives--;
        lives = Math.Clamp(lives, 0, maxLives);
        uiGame.ShowLives(lives);

        if (lives == 0)
        {
            //GAME OVER
            gameManager.GameOver();
        }
        else
        {
            // Puedo seguir jugando
            transform.position = initialPosition;
            playerRigidbody.velocity = Vector3.zero;
            StartCoroutine(InvulnerabilityCountdown());
        }
    }

    public void WinLife()
    {
        lives++;
        lives = Math.Clamp(lives, 0, maxLives);
        uiGame.ShowLives(lives);
    }
}