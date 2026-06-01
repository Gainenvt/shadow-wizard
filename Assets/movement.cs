using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;
    [Header("Audio Settings")]
    public AudioSource playerAudioSource;
    public AudioClip victoriaPushSound;


    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Vector2.zero;

        

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            moveInput.x -= 1;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            moveInput.x += 1;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

       
    }

   void FixedUpdate()
{
    if (GameManager.Instance.IsGamePaused())
    {
        rb.linearVelocity = Vector2.zero;
        return;
    }

    Vector2 velocity = rb.linearVelocity;

    velocity.x = moveInput.x * moveSpeed;

    rb.linearVelocity = velocity;

    if (Mathf.Abs(moveInput.x) > 0.1f &&
    playerAudioSource != null &&
    victoriaPushSound != null &&
    !playerAudioSource.isPlaying)
{
    playerAudioSource.PlayOneShot(
        victoriaPushSound
    );
}
}
}