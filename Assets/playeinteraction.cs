using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 0.5f;
    public float moveSpeed = 5f;
    Rigidbody2D currentCircle;
    public float verticalRange = 11f;


    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            Vector2 boxSize = new Vector2(0.2f, verticalRange);
            Vector2 boxCenter = new Vector2(transform.position.x, transform.position.y + verticalRange / 2);

            Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f);

            GameObject closestCircle = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider2D hit in hits)
            {
                float xDistance = Mathf.Abs(transform.position.x - hit.transform.position.x);

                if (!hit.CompareTag("circle")) continue;

                float yDistance = hit.transform.position.y - transform.position.y;

                if (xDistance < 0.25f && yDistance > 0 && yDistance < closestDistance)
                {
                    closestDistance = yDistance;
                    closestCircle = hit.gameObject;
                }

            }

            if (closestCircle != null)
            {
                CircleController circleController = closestCircle.GetComponent<CircleController>();
                Rigidbody2D rb = closestCircle.GetComponent<Rigidbody2D>();

                if (circleController != null && circleController.canBeMoved && rb != null)
                {
                    Vector2 moveDir = Vector2.up;

                    if (Keyboard.current.aKey.isPressed)
                        moveDir += Vector2.left;

                    if (Keyboard.current.dKey.isPressed)
                        moveDir += Vector2.right;

                    moveDir.Normalize();

                    if (currentCircle != null)
                        currentCircle.linearVelocity = Vector2.zero;

                    currentCircle = rb;
                    currentCircle.linearVelocity = moveDir * moveSpeed;
                }
            }
        }
    }
}