using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class RebornPoint : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform ghost;
    [SerializeField] private MovementSwitcher MS;
    [SerializeField] private GameObject hint;
    [Header("Controls")]
    [SerializeField] private KeyCode switchKey = KeyCode.T;
    private bool isOnTriggerEnterGhost = false;

    private void Awake()
    {
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        hint.SetActive(false);
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
            if (hint != null) hint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<GhostSwitchCollider>() != null)
        {
            isOnTriggerEnterGhost = false;
            if (hint != null) hint.SetActive(false);
        }
    }

    private void RebornPlayer()
    {
        player.position = gameObject.transform.position;
        MS.ExitGhost();

    }
}
