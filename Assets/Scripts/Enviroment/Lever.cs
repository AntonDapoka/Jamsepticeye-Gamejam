using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject thirdObject; 
    [SerializeField] private KeyCode interactKey = KeyCode.T; 
    [SerializeField] private bool isOnTriggerEnterGhost = false;
    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<GhostMovement>()!=null)
        {
            isOnTriggerEnterGhost = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<GhostMovement>() != null) 
        {
            isOnTriggerEnterGhost = false;
        }
    }

    private void Update()
    {
        if (isOnTriggerEnterGhost && Input.GetKeyDown(interactKey))
        {
            ActivateLever();
        }
    }

    private void ActivateLever()
    {
        isActivated = !isActivated;

        transform.rotation = Quaternion.Euler(0f, 0f, isActivated ? 45f:-45f);


        if (thirdObject != null)
        {
            thirdObject.SetActive(!thirdObject.activeSelf);
        }
    }
}