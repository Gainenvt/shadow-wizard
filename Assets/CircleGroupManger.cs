using System.Collections.Generic;

using UnityEngine;

public class CircleGroupManager : MonoBehaviour
{
	public static CircleGroupManager Instance;
	public float destroyHeight = -10f;
	
    void Awake()
	{
		Instance = this;
	}

	public void CheckGroup(CircleController circle)
	{
		List<CircleController> group = GetConnectedCircles(circle);
		if (group.Count >= 3)
		{
			// prevent double scoring
			foreach (CircleController c in group)
			{
				c.enabled = false;
			}

			if (group.Count >= 3)
			{
				foreach (CircleController c in group)
				{
					Destroy(c.gameObject);
				}
				Debug.Log("Group size: " + group.Count);
			}


		}

		List<CircleController> GetConnectedCircles(CircleController start)
		{
			List<CircleController> result = new List<CircleController>();
			Queue<CircleController> queue = new Queue<CircleController>();

			queue.Enqueue(start);
			result.Add(start);

			while (queue.Count > 0)
			{
				CircleController current = queue.Dequeue();

				Collider2D[] hits = Physics2D.OverlapCircleAll(current.transform.position, current.touchCheckRadius);

				foreach (Collider2D hit in hits)
				{
					CircleController other = hit.GetComponent<CircleController>();

					if (other != null &&
						other.colorType == start.colorType &&
						other.isFrozen &&
						!result.Contains(other))
					{
						result.Add(other);
						queue.Enqueue(other);
					}
				}
			}

			return result;
		}
	}
    void Update()
    {
        if (transform.position.y < destroyHeight)
        {
            Destroy(gameObject);
        }
    }

}