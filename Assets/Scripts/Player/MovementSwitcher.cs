using UnityEngine;

public class MovementSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D playerRb;
    [SerializeField] private GameObject ghost;
    private Rigidbody2D ghostRb;
    private PlayerMovement PM;
    private GhostMovement GM;
    private CapsuleCollider2D playerCollider;
    private CapsuleCollider2D ghostCollider;
    [SerializeField] private GhostSwitchCollider ghostSwitchCollider;
    [SerializeField] private bool isGhost = false;
    private float playerGravityScale;

    private void Start()
    {
        PM = player.GetComponent<PlayerMovement>();
        GM = ghost.GetComponent<GhostMovement>();
        playerCollider = player.GetComponent<CapsuleCollider2D>();
        ghostCollider = ghost.GetComponent<CapsuleCollider2D>();
        playerRb = player.GetComponent<Rigidbody2D>();
        ghostRb = ghost.GetComponent<Rigidbody2D>();

        ghostCollider.enabled = false;
        playerCollider.enabled = true;

        playerGravityScale = playerRb.gravityScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (isGhost == false)
            {
                isGhost = true;
                PM.enabled = false;
                GM.enabled = true;
                ghostRb.bodyType = RigidbodyType2D.Dynamic;
                ghost.transform.SetParent(null);


                ghostCollider.isTrigger = false;
                playerCollider.isTrigger = true;
                ghostCollider.enabled = true;


                playerRb.gravityScale = 0f;
                Debug.Log(playerRb.gravityScale);
            }
            else if (ghostSwitchCollider.IsOnTriggerEnterPlayer())
            {
                isGhost = false;
                PM.enabled = true;
                GM.enabled = false;

                ghostRb.bodyType = RigidbodyType2D.Kinematic;
                ghost.transform.SetParent(player.transform);
                ghost.transform.localPosition = new Vector3(0f,0f, -1f);
                ghost.transform.localRotation = Quaternion.identity;

                GM.StopMovement();

                ghostCollider.isTrigger = true;
                playerCollider.isTrigger = false;
                ghostCollider.enabled = false;
                playerRb.gravityScale = playerGravityScale;
            }
                
        }
    }
}
