using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody enemyRigidbody;
    
    private GameObject player;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        // Direction = destino - origen
        // destino = posición del player
        // origen = posición del enemigo
        Vector3 direction = player.transform.position -
                            transform.position;
        direction = direction.normalized;
        enemyRigidbody.AddForce(direction * speed);
    }
}
