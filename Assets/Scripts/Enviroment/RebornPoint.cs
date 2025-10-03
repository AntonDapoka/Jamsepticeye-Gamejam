using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class RebornPoint : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform ghost;
    [SerializeField] private MovementSwitcher MS;
    [Header("Controls")]
    [SerializeField] private KeyCode switchKey = KeyCode.T;
    private bool isOnTriggerEnterGhost = false;

    private void Awake()
    {
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(switchKey) && isOnTriggerEnterGhost) //Input.GetButtonDown("Jump")
        {
            RebornPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<GhostSwitchCollider>() != null)
        {
            isOnTriggerEnterGhost = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<GhostSwitchCollider>() != null)
        {
            isOnTriggerEnterGhost = false;
        }
    }

    private void RebornPlayer()
    {
        player.position = gameObject.transform.position;
        MS.ExitGhost();

    }
}
