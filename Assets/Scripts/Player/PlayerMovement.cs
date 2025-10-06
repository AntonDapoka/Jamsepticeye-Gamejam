using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    [SerializeField] private PlayerSpriteAnimation PSA;


    [Header("Movement Settings")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float airSpeedCoef = 0.6f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private float fallingGravityScale = 2.5f;
    [SerializeField] private float maxHorizontalSpeed = 8f;
    [SerializeField] private float maxVerticalSpeed = 20f;

    [Header("Ground Check")]
    [SerializeField] private float extraHeight = 0.07f;

    private Vector2 moveInput = Vector2.zero;
    private bool jumpRequest = false;
    private bool wasGrounded = false; // для детекции приземления
    private bool coyote = false;
    private bool startCoyote = false;
    private bool isOnTriggerEnterSpikes = false;
    [SerializeField] public static bool facingRight = true;
    public int direction { get; private set; } = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();

        rb.gravityScale = gravityScale;
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal"); 

        if (Input.GetKeyDown(KeyCode.Space)) //Input.GetButtonDown("Jump")
        {
            jumpRequest = true;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            PSA.FlipSprite(1);
            facingRight = false;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            PSA.FlipSprite(-1);
            facingRight = true;
        }

        wasGrounded = IsGrounded();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJumpAndGravity();
        ClampVelocity();
        UpdateAnimations();
        CoyoteTime();
    }

    private void CoyoteTime() {
        if (IsGrounded()) startCoyote = true;
        if (!coyote && startCoyote && !IsGrounded()) {
            startCoyote= false;
            StartCoroutine(CoyoteWindow());
        }
    }

    private IEnumerator CoyoteWindow()
    {
        coyote = true;
        yield return new WaitForSeconds(0.05f);
        coyote = false;
    }

    private void HandleMovement()
    {
        float currentSpeed = IsGrounded() ? speed : speed * airSpeedCoef;
        

        if (IsGrounded())
        {
            PSA.SetWalkAnimation();
        }

        Vector2 vel = rb.linearVelocity;
        vel.x = moveInput.x * currentSpeed;
        rb.linearVelocity = vel;
    }

    private void HandleJumpAndGravity()
    {
        if (jumpRequest && (IsGrounded() || coyote))
        {
            Vector2 vel = rb.linearVelocity;
            vel.y = jumpForce; // rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.linearVelocity = vel;
            PSA.SetJumpAnimation();
        }
        jumpRequest = false;
        startCoyote = false;

        rb.gravityScale = rb.linearVelocity.y >= 0f ? gravityScale : fallingGravityScale;
    }

    private void ClampVelocity()
    {
        float clampedX = Mathf.Clamp(rb.linearVelocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
        float clampedY = Mathf.Clamp(rb.linearVelocity.y, -maxVerticalSpeed, maxVerticalSpeed);
        rb.linearVelocity = new Vector2(clampedX, clampedY);
    }


    public bool IsGrounded()
    {
        var hits = Physics2D.BoxCastAll(
            cc.bounds.center,
            cc.bounds.size,
            0f,
            Vector2.down,
            extraHeight
        );

        foreach (var h in hits)
        {
            if (h.collider == null) continue;
            if (h.collider == cc) continue; 
            if (h.collider.TryGetComponent<Platform>(out var platform))
            {
                return true;
            }
        }

        return false;
    }

    private void UpdateAnimations()
    {
        bool grounded = IsGrounded();
        bool moving = Mathf.Abs(rb.linearVelocity.x) > 0.1f || Mathf.Abs(moveInput.x) > 0.1f;

        if (!grounded)
        {
            PSA.SetJumpAnimation();
            return;
        }

        if (grounded)
        {
            if (moving)
                PSA.SetWalkAnimation();
            else
                PSA.SetIdleAnimation();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Spikes>() != null)
        {
            isOnTriggerEnterSpikes = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Spikes>() != null)
        {
            isOnTriggerEnterSpikes = false;
        }
    }

    public bool GetIsOnTriggerEnterSpikes()
    {
        return isOnTriggerEnterSpikes;
    }

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        if (cc == null)
        {
            var c = GetComponent<CapsuleCollider2D>();
            if (c == null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(c.bounds.center + Vector3.down * extraHeight, c.bounds.size);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(cc.bounds.center + Vector3.down * extraHeight, cc.bounds.size);
        }
#endif
    }
}
