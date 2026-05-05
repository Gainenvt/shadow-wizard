using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGroupManager : MonoBehaviour
{
    public static CircleGroupManager Instance;

    public GameObject destroyParticles;
    

    public float destroyHeight = -10f;

    void Awake()
    {
        Instance = this;
    }

    IEnumerator DestroyEffect(GameObject obj)
    {
        float duration = 0.2f;
        float t = 0f;

        Vector3 startScale = obj.transform.localScale;

        while (t < duration)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(1.2f, 0f, t / duration);
            obj.transform.localScale = startScale * scale;
            yield return null;
        }

        Destroy(obj);
    }

    public void CheckGroup(CircleController circle)
    {
        List<CircleController> group = GetConnectedCircles(circle);

        if (group.Count >= 3)
        {
            foreach (CircleController c in group)
            {
                c.enabled = false;

                if (destroyParticles != null)
                    Instantiate(destroyParticles, c.transform.position, Quaternion.identity);
                Debug.Log("DestroyingParticles");



                StartCoroutine(DestroyEffect(c.gameObject));
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