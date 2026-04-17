using UnityEngine;

using System.Collections.Generic;

public class CircleController : MonoBehaviour
{
	private Rigidbody2D rb;

	public bool isFrozen = false;
	public bool canBeMoved = true;
	public float touchCheckRadius = 1.2f;
	public float pushForce = 5f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("FreezeLine"))
		{
			FreezeCircle();
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		CircleController otherCircle = collision.gameObject.GetComponent<CircleController>();

		if (otherCircle != null)
		{
			// Freeze spreading
			if (otherCircle.isFrozen && !isFrozen)
			{
				FreezeCircle();
			}
		}

		// Match checking
		if (collision.gameObject.CompareTag("circle"))
		{
			CheckForThreeTouching();
		}
	}

	void CheckForThreeTouching()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, touchCheckRadius);

		List<GameObject> touchingCircles = new List<GameObject>();

		foreach (Collider2D hit in hits)
		{
			if (hit.CompareTag("circle"))
			{
				touchingCircles.Add(hit.gameObject);
			}
		}

		if (touchingCircles.Count >= 3)
		{
			foreach (GameObject circle in touchingCircles)
			{
				Destroy(circle);
			}
		}
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && canBeMoved)
		{
			Vector2 direction = (transform.position - collision.transform.position);
			direction.y = 0;
			direction = direction.normalized;

			rb.linearVelocity = new Vector2(direction.x * pushForce, rb.linearVelocity.y);
		}
	}

	void FreezeCircle()
	{
		isFrozen = true;
		canBeMoved = false;

		rb.linearVelocity = Vector2.zero;
		rb.angularVelocity = 0f;

		rb.gravityScale = 0f;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
	}
}