using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteNotActive;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private bool isOnTriggerEnterGhost = false;
    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<GhostMovement>() != null)
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

        //transform.rotation = Quaternion.Euler(0f, 0f, isActivated ? 45f:-45f);
        gameObject.GetComponent<SpriteRenderer>().sprite = isActivated ? spriteActive : spriteNotActive;

        foreach (GameObject obj in objects)
        {

            if (obj != null)
            {
                if (obj.GetComponent<Box>() != null)
                {
                    Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                    rb.gravityScale = -rb.gravityScale;
                }
                else
                    obj.SetActive(!obj.activeSelf);
            }
        }
    }
}