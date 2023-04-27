using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    public GameObject obstaclePrefab;
    public GameObject PowerUpPrefabBoost;
    public GameObject PowerUpPrefabPoints;

    private void Start()
    {
        groundSpawner = FindObjectOfType<GroundSpawner>();
        SpawnObstacle(5, 10, obstaclePrefab);
        SpawnPowerUp();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            groundSpawner.SpawnTile();
            Destroy(gameObject, 2);
        }
    }

    void SpawnObstacle(int begin, int end, GameObject gameObject)
    {
        int obstacleSpawnIndex = Random.Range(begin, end);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        Instantiate(gameObject, spawnPoint.position, Quaternion.identity, transform);
    }

    void SpawnPowerUp()
    {
        int prefab = Random.Range(0, 2);

        GameObject gameObject;

        if (prefab == 0)
        {
            gameObject = PowerUpPrefabPoints;
        }
        else
        {
            gameObject = PowerUpPrefabBoost ;
        }

        double probOfSpawn = Random.Range(0, 100);

        if (probOfSpawn/100 <= 0.2)
        {
            SpawnObstacle(2, 4, gameObject);
        }
        
    }
}

