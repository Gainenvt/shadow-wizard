using UnityEngine;

public class circleGen : MonoBehaviour
{
	public GameObject circlePrefab;
	public float minSpawnTime = 0.005f;
	public float maxSpawnTime = 2f;
	public float difficultyIncreaseRate = 0.05f;

	private float spawnTimer;
	private float nextSpawnTime;

	void Update()
	{

		spawnTimer += Time.deltaTime;

		minSpawnTime = Mathf.Max(0.2f, minSpawnTime - difficultyIncreaseRate * Time.deltaTime);
		maxSpawnTime = Mathf.Max(0.5f, maxSpawnTime - difficultyIncreaseRate * Time.deltaTime);

		if (spawnTimer >= nextSpawnTime)
		{
			spawnTimer = 0f;
			nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

			Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 6f, 0f);
			Instantiate(circlePrefab, spawnPos, Quaternion.identity);
		}
	}
}