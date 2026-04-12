using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
	public float moveSpeed = 5f;
	private Vector2 moveInput;

	void Start()
	{

	}

	void Update()
	{
		moveInput = Vector2.zero;

		if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
			moveInput.y += 2;

		if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
			moveInput.y -= 2;

		if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
			moveInput.x -= 2;

		if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
			moveInput.x += 2;

		transform.Translate(moveInput * moveSpeed * Time.deltaTime);
	}
}