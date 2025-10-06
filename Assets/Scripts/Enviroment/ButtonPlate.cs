using UnityEngine;

public class ButtonPlate : MonoBehaviour
{
    public GameObject[] gameobjects;
    public bool isActivatedByPlayer = false;
    public bool isActivatedByBox = false;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteNotActive;
    [SerializeField] bool isInitiallyActivated = false;

    private void ActivateButton()
    {
        foreach (GameObject obj in gameobjects)
        {
            obj.SetActive(false);
        }
    }

    private void DisactivateButton()
    {
        foreach (GameObject obj in gameobjects)
        {
            obj.SetActive(true);
        }
    }

    private void Update()
    {
        if (!isActivatedByPlayer && !isActivatedByBox)
        {
            foreach (GameObject obj in gameobjects)
            {
                if (obj != null)
                    obj.SetActive(isInitiallyActivated);
                gameObject.GetComponent<SpriteRenderer>().sprite = spriteNotActive;
            }
        }
        if (isActivatedByBox || isActivatedByPlayer)
        {
            foreach (GameObject obj in gameobjects)
            {
                if (obj != null)
                    obj.SetActive(!isInitiallyActivated);
                gameObject.GetComponent<SpriteRenderer>().sprite = spriteActive;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerSwitchCollider>() != null || other.gameObject.GetComponent<PlayerMovement>() != null )
        {
            isActivatedByPlayer = true;
        }
         if (other.gameObject.GetComponent<Box>() != null)
        {
            isActivatedByBox = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerSwitchCollider>() != null || other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            isActivatedByPlayer = false;
        }
        if (other.gameObject.GetComponent<Box>() != null)
        {
            isActivatedByBox = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerSwitchCollider>() != null || other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            isActivatedByPlayer = true;
        }
        if (other.gameObject.GetComponent<Box>() != null)
        {
            isActivatedByBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerSwitchCollider>() != null || other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            isActivatedByPlayer = false;
        }
        if (other.gameObject.GetComponent<Box>() != null)
        {
            isActivatedByBox = false;
        }
    }
}