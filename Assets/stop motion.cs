using UnityEngine;

public class Stopmotion : MonoBehaviour
{
    private Rigidbody2D rb;
    private const string FREEZELINE_TAG = "FreezeLine";
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
		if( other.CompareTag("FreezeLine"))
		{
			rb.linearVelocity = Vector2.zero;
			rb.angularVelocity = 0f;
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
		}
	}
}
