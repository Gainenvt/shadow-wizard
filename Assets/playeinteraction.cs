using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
	public float interactRange = 1.5f;

	void Update()
	{
		if (Keyboard.current.spaceKey.wasPressedThisFrame)
		{
			TryMoveCircle();
		}
	}

	void TryMoveCircle()
	{
		GameObject[] circles = GameObject.FindGameObjectsWithTag("circle");

		foreach (GameObject circle in circles)
		{
			float distance = Vector2.Distance(transform.position, circle.transform.position);

			if (distance < interactRange)
			{
				// Check if player is BELOW the circle
				if (transform.position.y < circle.transform.position.y)
				{
					circle.transform.position += new Vector3(1f, 6f, 1f);
					break; // only move one circle
				}
			}
		}
	}
}