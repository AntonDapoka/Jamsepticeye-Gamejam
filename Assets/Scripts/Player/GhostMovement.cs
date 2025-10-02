using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public class GhostMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 20f; 
    [SerializeField] private float maxSpeed = 6f;      
    [SerializeField] private float friction = 5f;      
    private Vector2 moveInput = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.A)) horizontal = -1f;
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;
        moveInput.x = horizontal;

        float vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.W)) vertical = 1f;
        if (Input.GetKey(KeyCode.S)) vertical = -1f;
        moveInput.y = vertical;

        moveInput = moveInput.normalized;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (moveInput.sqrMagnitude > 0)
        {
            Vector2 targetVelocity = moveInput * maxSpeed;
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, Vector2.zero, friction * Time.fixedDeltaTime);
        }
    }


    public void StopMovement()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        moveInput = Vector2.zero;
    }
}

/*private void FlipSprite()
 {
     if (moveInput.x < 0f)
     {
         direction = -1;
         transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
     }
     else if (moveInput.x > 0f)
     {
         direction = 1;
         transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
     }
 }*/