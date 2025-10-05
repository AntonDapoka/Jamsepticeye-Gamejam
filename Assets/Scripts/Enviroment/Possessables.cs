using NUnit.Framework;
using UnityEngine;

public class Possessables : MonoBehaviour
{
    public string playerTag = "Player"; 
    private GameObject playerInRange = null;
    private bool canInteract = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = other.gameObject;
            canInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = null;
            canInteract = false;
        }
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInRange != null)
            {
                playerInRange.transform.SetParent(transform);

                playerInRange.transform.localPosition = Vector3.zero;
                playerInRange.transform.localRotation = Quaternion.identity;

                canInteract = false;
            }
        }
    }
}
