using UnityEngine;

public class GhostSwitchCollider : MonoBehaviour
{
    private bool isOnTriggerEnterPlayer = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerSwitchCollider>() != null)
        {
            isOnTriggerEnterPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerSwitchCollider>() != null)
        {
            isOnTriggerEnterPlayer = false; 
        }
    }

    public bool IsOnTriggerEnterPlayer()
    {
        return isOnTriggerEnterPlayer;
    }
}
