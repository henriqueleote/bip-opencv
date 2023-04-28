using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    public GameObject obstaclePrefab; 
    public GameObject obstaclePrefab2;
    public GameObject obstaclePrefab3;
    public GameObject obstaclePrefab4;
    public GameObject obstaclePrefab5;
    public GameObject PowerUpPrefabBoost;
    public GameObject PowerUpPrefabPoints;

    private void Start()
    {
        groundSpawner = FindObjectOfType<GroundSpawner>();
        Skyboxbjects();
        SpawnPowerUp();
    }

    private void Skyboxbjects()
    {
        string currentSkyboxName = RenderSettings.skybox.name;

        int randomNumber = Random.Range(0, 2);

        // Spawn objects based on the current skybox
        if (currentSkyboxName == "SkyBox-1")
        {
            if (randomNumber == 0)
            {
                SpawnObstacle(5, 10, obstaclePrefab2);
            }
            else
            {
                SpawnObstacle(5, 10, obstaclePrefab5);
            }
        }
        else if (currentSkyboxName == "SkyBox-2")
        {
            if (randomNumber == 0)
            {
                SpawnObstacle(5, 10, obstaclePrefab);
            }
            else
            {
                SpawnObstacle(5, 10, obstaclePrefab3);
            }
        }
        else if (currentSkyboxName == "SkyBox-3")
        {
            if (randomNumber == 0)
            {
                SpawnObstacle(5, 10, obstaclePrefab);
            }
            else
            {
                SpawnObstacle(5, 10, obstaclePrefab4);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            groundSpawner.SpawnTile();
            Destroy(gameObject, 2);
        }
    }

    void SpawnObstacle(int begin, int end, GameObject a)
    {
        int obstacleSpawnIndex = Random.Range(begin, end);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        if (a.name == "rock")
        {
            Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y - 0.3f, spawnPoint.position.z);
            Instantiate(a, spawnPosition, Quaternion.identity, transform);
        }
        else
        {
            Instantiate(a, spawnPoint.position, Quaternion.identity, transform);
        }
        
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

