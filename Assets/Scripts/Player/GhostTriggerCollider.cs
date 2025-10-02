using UnityEngine;

public class GhostTriggerCollider : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D solidCollider;

    private void Awake()
    {
        if (solidCollider != null)
            solidCollider.enabled = false; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BlockGhost>() != null)
        {
            if (solidCollider != null)
            {
                solidCollider.enabled = true; 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<BlockGhost>() != null)
        {
            solidCollider.enabled = false;
        }
    }
}