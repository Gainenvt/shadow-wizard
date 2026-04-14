using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
	public float interactRange = 10f;

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

			if(xDistance < 1.5f &&  yDistance > 0&& yDistance < 10f)
			{
				if (transform.position.y < circle.transform.position.y)
				{
					Vector3 moveDir = new Vector3(0, 2f, 0);

					if (Keyboard.current.aKey.isPressed)
						moveDir += new Vector3(-1f, 0, 0);

					if (Keyboard.current.dKey.isPressed)
						moveDir += new Vector3(2f, 0, 0);

					circle.transform.position += moveDir * Time.deltaTime * 5f;

					


				}

			}
		}
	}
}