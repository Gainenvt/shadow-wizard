using UnityEngine;

public class circleGen : MonoBehaviour
{
	public GameObject[] circlePrefabs;
	public float minSpawnTime = 0.5f;
	public float maxSpawnTime = 2f;
	public float difficultyIncreaseRate = 0.05f;
	public float spawnZone = 0.5f;
	public Transform spawnpoint;
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

		// check if space is free
		Collider2D hit = Physics2D.OverlapCircle(spawnpoint.position, spawnZone, circleLayer);

		if (hit == null) // only spawn if EMPTY
		{
			int index = Random.Range(0, circlePrefabs.Length);
			GameObject chosenCircle = circlePrefabs[index];

			Vector3 spawnPos = new Vector3(
			Random.Range(minX, maxX),
			spawnY,
			0f);

			Instantiate(chosenCircle, spawnPos, Quaternion.identity);
		}
	}

}