using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 2.5f;
    public float moveSpeed = 5f;
    Rigidbody2D currentCircle;


    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);

            GameObject closestCircle = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider2D hit in hits)
            {
                if (!hit.CompareTag("circle")) continue;

                float xDistance = Mathf.Abs(transform.position.x - hit.transform.position.x);
                float yDistance = hit.transform.position.y - transform.position.y;

                if (xDistance < 1.2f && yDistance > 0 && yDistance < closestDistance)
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

                    // Stop previous circle
                    if (currentCircle != null)
                    {
                        currentCircle.linearVelocity = Vector2.zero;
                    }

                    // Set new circle
                    currentCircle = rb;

                    // Move only this one
                    currentCircle.linearVelocity = moveDir * moveSpeed;
                }
            }
        }
    }
}