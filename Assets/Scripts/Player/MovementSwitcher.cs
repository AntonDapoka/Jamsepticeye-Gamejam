using System;
using UnityEngine;

public class MovementSwitcher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ghost;
    [SerializeField] private GhostSwitchCollider ghostSwitchCollider;
    [SerializeField] private PlayerSwitchCollider playerswitchcol;
    [SerializeField] private GhostPassableEnviromentSwitcher enviromentSwitcher;
    [SerializeField] private PlayerSpriteAnimation PSA;
    [SerializeField] private GhostSpriteAnimation GSA;
    //[SerializeField] private CapsuleCollider2D playerCollider2;

    [Header("Controls")]
    [SerializeField] private KeyCode switchKey = KeyCode.E;

    // Кэш компонентов
    private Rigidbody2D playerRb;
    private Rigidbody2D ghostRb;
    private PlayerMovement playerMovement;
    private GhostMovement ghostMovement;
    private CapsuleCollider2D playerCollider;
    private CapsuleCollider2D ghostCollider;

    static (float, float) var = (0, 0);
    private (float,float)[] speed = {var, var};
    public LayerMask Lava;
    private bool facingRight = true;

    private bool isGhost = false;
    //private float playerGravityScale;

    private void Awake()
    {
        if (player == null || ghost == null)
        {
            enabled = false;
            return;
        }

        playerRb = player.GetComponent<Rigidbody2D>();
        ghostRb = ghost.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<PlayerMovement>();
        ghostMovement = ghost.GetComponent<GhostMovement>();
        playerCollider = player.GetComponent<CapsuleCollider2D>();
        ghostCollider = ghost.GetComponent<CapsuleCollider2D>();

        //playerGravityScale = playerRb != null ? playerRb.gravityScale : 1f;

        InitializeState();
    }

    private void InitializeState()
    {
        isGhost = false;

        if (playerMovement != null) playerMovement.enabled = true;
        if (ghostMovement != null) ghostMovement.enabled = false;

        if (playerCollider != null) { playerCollider.enabled = true; playerCollider.isTrigger = false; }
        if (ghostCollider != null) { ghostCollider.enabled = false; ghostCollider.isTrigger = true; }

        if (ghostRb != null) ghostRb.bodyType = RigidbodyType2D.Kinematic;
        if (ghost != null) ghost.transform.SetParent(player.transform);
    }

    private void Update()
    {
        speed[1] = speed[0];
        speed[0].Item1 = playerRb.linearVelocityX; speed[0].Item2 = playerRb.linearVelocityY;
        if (Input.GetKeyDown(switchKey)|| (Physics2D.OverlapPoint(playerCollider.transform.position, Lava) && !isGhost))
            TryToggle();
    }

    private void TryToggle()
    {
        if (!isGhost)
        {
            EnterGhost();
            return;
        }

        if (ghostSwitchCollider != null && ghostSwitchCollider.IsOnTriggerEnterPlayer())
        {
            ExitGhost();
        }
    }

    private void EnterGhost()
    {
        isGhost = true;
        ghost.SetActive(true);

        if (playerMovement != null) playerMovement.enabled = false;
        if (ghostMovement != null) ghostMovement.enabled = true;

        if (ghostRb != null) ghostRb.bodyType = RigidbodyType2D.Dynamic;
        if (ghost != null) ghost.transform.SetParent(null);

        playerMovement.StopMovement();
        PSA.SetDeathAnimation();

        if (ghostCollider != null) { ghostCollider.enabled = true; ghostCollider.isTrigger = false; }
        //if (playerCollider != null) playerCollider.isTrigger = true;

        //if (playerRb != null) playerRb.gravityScale = 0f;

        enviromentSwitcher?.EnableGhostMode();
        ghostRb.AddForce(new Vector2(speed[1].Item1*30, speed[1].Item2*25+150f));

        playerswitchcol.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        playerswitchcol.transform.position = new Vector3(playerswitchcol.transform.position.x + (PlayerMovement.facingRight? 1f:-1f), playerswitchcol.transform.position.y - 1f, 0f);
        playerCollider.direction = CapsuleDirection2D.Horizontal;
        playerCollider.size = new Vector2(3.45f, 1f);
        playerCollider.offset = new Vector2((PlayerMovement.facingRight ? 1f : -1f), -1f);
        
    }

    public void ExitGhost()
    {
        isGhost = false;

        if (playerMovement != null) playerMovement.enabled = true;
        if (ghostMovement != null) ghostMovement.enabled = false;

        if (ghostRb != null) ghostRb.bodyType = RigidbodyType2D.Kinematic;

        if (ghost != null)
        {
            ghost.transform.SetParent(player.transform);
            ghost.transform.localPosition = new Vector3(0f, 0f, -1f);
            ghost.transform.localRotation = Quaternion.identity;
        }

        ghostMovement.StopMovement();

        if (ghostCollider != null) { ghostCollider.isTrigger = true; ghostCollider.enabled = false; }
        if (playerCollider != null) playerCollider.isTrigger = false;

        //if (playerRb != null) playerRb.gravityScale = playerGravityScale;

        enviromentSwitcher?.DisableGhostMode();
        ghost.SetActive(false);

        playerswitchcol.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        playerswitchcol.transform.position = new Vector3(playerswitchcol.transform.position.x - (PlayerMovement.facingRight ? 1f : -1f), playerswitchcol.transform.position.y + 1f, 0f);
        playerCollider.direction = CapsuleDirection2D.Vertical;
        playerCollider.size = new Vector2(1f, 3f);
        playerCollider.offset = new Vector2(0,0);
    }

    public void ForceEnterGhost() => EnterGhost();
    public void ForceExitGhost() => ExitGhost();
}
