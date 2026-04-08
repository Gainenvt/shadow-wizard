using System.Diagnostics;

using UnityEngine;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class FallingCircle : MonoBehaviour
{
	public float fallSpeed = 3f;

	void Start()
	{

	}
	void Update()
	{
		transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

		if (transform.position.y < -6f)
		{
			Destroy(gameObject);
		}
	}

	private string GetDebuggerDisplay()
	{
		return ToString();
	}
}