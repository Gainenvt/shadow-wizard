using UnityEngine;

public class orbmovement : MonoBehaviour
{
	private Rigidbody2D rb;

	public bool isFrozen = false;
	public bool canbeMoved = true;


	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("FreezeLine"))
		{
			FreezeCircles();
		}
	}

	void OnCollisionEnter2D(UnityEngine.Collision2D collision)
	{
		CircleController otherCircle = collision.gameObject.GetComponent<CircleController>();
		if (otherCircle != null && otherCircle.isFrozen)
		{
			canbeMoved = false;
		}

	}
	void FreezeCircles()
	{
		isFrozen = true;
		rb.bodyType = RigidbodyType2D.Dynamic;
		
	}
}
