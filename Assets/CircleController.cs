using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
	private Rigidbody2D rb;

	public bool isFrozen = false;
	public bool canBeMoved = true;
	public float touchCheckRadius = 1.2f;
	public float pushForce = 5f;
	public enum CircleType { Fire, Wind, Darkness , Light }
	public CircleType colorType;


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
			CircleController other = collision.gameObject.GetComponent<CircleController>();
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
    void TryMatch()
    {
        if (!isFrozen) return;

        CheckMatch();
    }

    void CheckMatch()
    {
        List<CircleController> group = GetConnectedCircles();

        if (group.Count >= 3)
        {
            GameManager.Instance.AddMatchScore(group.Count);

            foreach (CircleController c in group)
            {
                Destroy(c.gameObject);
            }
        }
    }
    List<CircleController> GetConnectedCircles()
    {
        List<CircleController> result = new List<CircleController>();
        Queue<CircleController> queue = new Queue<CircleController>();

        queue.Enqueue(this);
        result.Add(this);

        while (queue.Count > 0)
        {
            CircleController current = queue.Dequeue();

            Collider2D[] hits = Physics2D.OverlapCircleAll(current.transform.position, touchCheckRadius);

            foreach (Collider2D hit in hits)
            {
                CircleController other = hit.GetComponent<CircleController>();

                if (other != null &&
                    other.colorType == this.colorType &&
                    !result.Contains(other))
                {
                    result.Add(other);
                    queue.Enqueue(other);
                }
            }
        }

        return result;
    }

    void FreezeCircle()
	{
		if (isFrozen) return; 

		isFrozen = true;
		canBeMoved = false;

		rb.linearVelocity = Vector2.zero;
		rb.angularVelocity = 0f;
		rb.gravityScale = 0f;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;

		Invoke(nameof(TryMatch), 0.2f);
	}

}