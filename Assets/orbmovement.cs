using UnityEngine;

public class orbmovement : MonoBehaviour
{
	private Rigidbody2D rb;

	public bool isFrozen = false;
	public bool canbeMoving = true;


	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   void ontriggerEnter2D(Collider2D other)
   {
	   if(other.CompareTag("FreezeLine"))
	   {
		  FreezeCircles();
		  FreezeCircles();
	   }
	}
}
