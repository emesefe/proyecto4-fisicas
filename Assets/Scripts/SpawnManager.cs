using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerupPrefab;

    private float spawnLimit = 8f;

    private void Start()
    {
        Instantiate(enemyPrefab,
            GenerateRandomPosition(), 
            Quaternion.identity);
        
        Instantiate(powerupPrefab,
            new Vector3(0, 0, -3f), 
            Quaternion.identity);
    }

    private Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(-spawnLimit, spawnLimit);
        float z = Random.Range(-spawnLimit, spawnLimit);

        return new Vector3(x, 0, z);
    }
}
