
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
	public float moveSpeed = 5f;
	private Vector2 moveInput;
	private Rigidbody2D rb;

	void Start()
	{

	}

	void Update()
	{
		moveInput = Vector2.zero;

		if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
			moveInput.y += 2;

		

		if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
			moveInput.x -= 2;

		if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
			moveInput.x += 2;

		transform.Translate(moveInput * moveSpeed * Time.deltaTime);
	}
}