using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform target;

    public float spawnInterval = 10f;
    private GameManager gameManager;

    // Spawn range boundaries
    public float minX = -41f;
    public float maxX = 40f;
    public float minZ = -13f;
    public float maxZ = 28f;
    public float yPosition = 0.5f; // Y position to spawn cubes

    private float timer = 0f;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    void Update()
    {
         // Only spawn cubes if the game has started
        if (gameManager == null || !gameManager.IsGameStarted()) return;
        
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCube();
            timer = 0f;
        }
    }

    void SpawnCube()
    {
        Vector3 spawnPos = Vector3.zero;

        int edge = Random.Range(0, 4); // Choose a random edge: 0 = Top, 1 = Bottom, 2 = Left, 3 = Right

        switch (edge)
        {
            case 0: // Top edge (Z = maxZ)
                spawnPos = new Vector3(Random.Range(minX, maxX), yPosition, maxZ);
                break;
            case 1: // Bottom edge (Z = minZ)
                spawnPos = new Vector3(Random.Range(minX, maxX), yPosition, minZ);
                break;
            case 2: // Left edge (X = minX)
                spawnPos = new Vector3(minX, yPosition, Random.Range(minZ, maxZ));
                break;
            case 3: // Right edge (X = maxX)
                spawnPos = new Vector3(maxX, yPosition, Random.Range(minZ, maxZ));
                break;
        }

        GameObject cube = Instantiate(cubePrefab, spawnPos, Quaternion.identity);

        TestCubeScript testScript = cube.GetComponent<TestCubeScript>();
        if (testScript != null)
        {
            testScript.target = target;
        }
    }
}
