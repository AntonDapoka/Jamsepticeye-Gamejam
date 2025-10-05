using UnityEngine;

public class GhostSwitchCollider : MonoBehaviour
{
    private bool isOnTriggerEnterPlayer = false;
    private bool isInPossessable = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerSwitchCollider>() != null)
        {
            isOnTriggerEnterPlayer = true;
        }
        if (other.GetComponent<Possessables>() != null)
        {
            isInPossessable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerSwitchCollider>() != null)
        {
            isOnTriggerEnterPlayer = false; 
        }
        if (other.GetComponent<Possessables>() != null)
        {
            isInPossessable = false;
        }
    }

    public bool IsOnTriggerEnterPlayer()
    {
        return isOnTriggerEnterPlayer;
    }
}
