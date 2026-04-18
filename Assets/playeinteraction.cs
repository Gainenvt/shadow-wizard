using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
	public float interactRange = 10f;
	public float moveSpeed = 5f;


	void Update()
	{
		if (Keyboard.current.spaceKey.isPressed)
		{
			TryMoveCircle();
		}
	}

	void TryMoveCircle()
	{
		GameObject[] circles = GameObject.FindGameObjectsWithTag("circle");

		GameObject closestCircle = null;
		float closestDistance = Mathf.Infinity;

		foreach (GameObject circle in circles)
		{
			float xDistance = Mathf.Abs(transform.position.x - circle.transform.position.x);
			float yDistance = circle.transform.position.y - transform.position.y;

			// must be above player and roughly aligned
			if (xDistance < 1.5f && yDistance > 0 && yDistance < interactRange)
			{
				if (yDistance < closestDistance)
				{
					closestDistance = yDistance;
					closestCircle = circle;
				}
			}
		}

		if (closestCircle != null)
		{
			CircleController circleController = closestCircle.GetComponent<CircleController>();
			Rigidbody2D rb = closestCircle.GetComponent<Rigidbody2D>();

			if (circleController != null && circleController.canBeMoved && rb != null)
			{
				Vector2 moveDir = Vector2.up;

				if (Keyboard.current.aKey.isPressed)
					moveDir += Vector2.left;

				if (Keyboard.current.dKey.isPressed)
					moveDir += Vector2.right;

				moveDir.Normalize();

				rb.linearVelocity = moveDir * moveSpeed;
			}
		}
	}
}