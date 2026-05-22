using UnityEngine;

public class CircleGen : MonoBehaviour
{
    public GameObject[] circlePrefabs;

    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 2f;
    public float difficultyIncreaseRate = 0.05f;

    public float spawnZone = 0.5f;
    public LayerMask circleLayer;

    public float minX = -8f;
    public float maxX = 8f;
    public float spawnY = 6f;

    private float spawnTimer;
    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        // gradually increase difficulty
        minSpawnTime = Mathf.Max(0.2f, minSpawnTime - difficultyIncreaseRate * Time.deltaTime);
        maxSpawnTime = Mathf.Max(0.5f, maxSpawnTime - difficultyIncreaseRate * Time.deltaTime);

        if (spawnTimer >= nextSpawnTime)
        {
            spawnTimer = 0f;
            nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            TrySpawn();
        }
    }

    void TrySpawn()
    {
        // safety check so Unity doesn't explode
        if (circlePrefabs == null || circlePrefabs.Length == 0)
        {
            Debug.LogWarning("No prefabs assigned!");
            return;
        }

        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(minX, maxX),
                spawnY,
                0f
            );

            Collider2D hit = Physics2D.OverlapCircle(spawnPos, spawnZone, circleLayer);

            if (hit == null)
            {
                int index = Random.Range(0, circlePrefabs.Length);
                GameObject chosenCircle = circlePrefabs[index];

                if (chosenCircle == null)
                {
                    Debug.LogWarning("A prefab in the array is missing!");
                    continue;
                }

                Instantiate(chosenCircle, spawnPos, Quaternion.identity);
                return;
            }
        }
    }
}