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

		foreach (GameObject circle in circles)
		{
			float xDistance = Mathf.Abs(transform.position.x - circle.transform.position.x);
			float yDistance = Mathf.Abs(transform.position.y - circle.transform.position.y);

			if (xDistance < 1.5f && yDistance > 0 && yDistance < interactRange)
			{
				if (transform.position.y < circle.transform.position.y)
				{
					CircleController circleController = circle.GetComponent<CircleController>();

					if (circleController != null && circleController.canBeMoved)
					{
						Rigidbody2D circleRb = circle.GetComponent<Rigidbody2D>();

						if (circleRb != null)
						{
							Vector2 moveDir = Vector2.up;

							if (Keyboard.current.aKey.isPressed)
								moveDir += Vector2.left;

							if (Keyboard.current.dKey.isPressed)
								moveDir += Vector2.right;

							moveDir.Normalize();

							circleRb.linearVelocity = moveDir * moveSpeed;
						}

					}
				}
			}
		}
	}

}
