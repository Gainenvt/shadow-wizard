using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    private Rigidbody2D rb;

    public Color orbColor;

    public bool isFrozen = false;
    public bool canBeMoved = true;
    private bool isDestroying = false;
    public float touchCheckRadius = 1.2f;
    public float pushForce = 5f;

    public GameObject destroyParticles;

    public enum CircleType
    {
        Fire,
        Lightning,
        Darkness,
        Light,
        Cursed,
        Amp
    }

    public CircleType colorType;

    public bool hasBeenCounted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       if (transform.position.y < -4f && !isDestroying)
{
    isDestroying = true;
    
    if (colorType == CircleType.Cursed)
    {
        GameManager.Instance.Score -= 1;

        GameManager.Instance.UpdateScoreUI();

        if (GameManager.Instance.Score <= -3)
        {
            GameManager.Instance.EndGame(false);
        }
    }
    else
    {
        GameManager.Instance.CircleMissed();
    }
    

    StartCoroutine(DestroyAnimation());
}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FreezeLine"))
        {
            FreezeCircle();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CircleController otherCircle =
            collision.gameObject.GetComponent<CircleController>();

        if (otherCircle != null)
        {
            if (otherCircle.isFrozen && !isFrozen)
            {
                FreezeCircle();
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canBeMoved)
        {
            Vector2 direction =
                (transform.position - collision.transform.position);

            direction.y = 0;

            direction = direction.normalized;

            rb.linearVelocity =
                new Vector2(
                    direction.x * pushForce,
                    rb.linearVelocity.y
                );
        }
    }

    void TryMatch()
    {
        if (!isFrozen)
            return;

        CheckMatch();
    }

    void CheckMatch()
    {
        List<CircleController> group =
            GetConnectedCircles();

        if (group.Count >= 3)
        {
            bool hasCursedOrb = false;
            bool hasAmpOrb = false;

            foreach (CircleController c in group)
            {
                if (c.colorType == CircleType.Cursed)
                {
                    hasCursedOrb = true;
                }

                if (c.colorType == CircleType.Amp)
                {
                    hasAmpOrb = true;
                }
            }

            if (hasCursedOrb)
            {
                GameManager.Instance.Score -= 1;

                GameManager.Instance.UpdateScoreUI();

                if (GameManager.Instance.Score <= -3)
                {
                 GameManager.Instance.EndGame(false);
                }           
            }
            else
            {
                int bonus = hasAmpOrb ? 2 : 0;

                GameManager.Instance.AddMatchScore(
                    group.Count + bonus
                );
            }

            foreach (CircleController c in group)
            {
                StartCoroutine(
                    c.DestroyAnimation()
                );
            }
        }
    }

    List<CircleController> GetConnectedCircles()
    {
        List<CircleController> result =
            new List<CircleController>();

        Queue<CircleController> queue =
            new Queue<CircleController>();

        queue.Enqueue(this);

        result.Add(this);

        while (queue.Count > 0)
        {
            CircleController current =
                queue.Dequeue();

            Collider2D[] hits =
                Physics2D.OverlapCircleAll(
                    current.transform.position,
                    touchCheckRadius
                );

            foreach (Collider2D hit in hits)
            {
                CircleController other =
                    hit.GetComponent<CircleController>();

                if (other != null)
                {
                    bool canMatch =
                    (
                        other.colorType == this.colorType ||

                        (
                            other.colorType ==
                            CircleType.Cursed &&

                            this.colorType !=
                            CircleType.Amp
                        ) ||

                        (
                            this.colorType ==
                            CircleType.Cursed &&

                            other.colorType !=
                            CircleType.Amp
                        ) ||

                        (
                            other.colorType ==
                            CircleType.Amp &&

                            this.colorType !=
                            CircleType.Cursed
                        ) ||

                        (
                            this.colorType ==
                            CircleType.Amp &&

                            other.colorType !=
                            CircleType.Cursed
                        )
                    );

                    if (
                        canMatch &&
                        !result.Contains(other)
                    )
                    {
                        result.Add(other);

                        queue.Enqueue(other);
                    }
                }
            }
        }

        return result;
    }

    void FreezeCircle()
    {
        if (isFrozen)
            return;

        isFrozen = true;

        canBeMoved = false;

        rb.linearVelocity = Vector2.zero;

        rb.angularVelocity = 0f;

        rb.gravityScale = 0f;

        rb.constraints =
            RigidbodyConstraints2D.FreezeAll;

        Invoke(nameof(TryMatch), 0.2f);
    }

    IEnumerator DestroyAnimation()
    {
        float duration = 0.2f;

        float timer = 0f;

        Vector3 startScale =
            transform.localScale;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float scale =
                Mathf.Lerp(
                    1f,
                    0f,
                    timer / duration
                );

            transform.localScale =
                startScale * scale;

            yield return null;
        }

        GameObject particles =
            Instantiate(
                destroyParticles,
                transform.position,
                Quaternion.identity
            );

        ParticleSystem ps =
            particles.GetComponent<ParticleSystem>();

        var main = ps.main;

        main.startColor = orbColor;

        Destroy(gameObject);
    }
}