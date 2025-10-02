using UnityEngine;

public class ButtonPlate : MonoBehaviour
{
    public GameObject gameobject;

    private void ActivateButton()
    {
        Debug.Log("������ ������!");
        gameobject.SetActive(false);
    }

    private void DisactivateButton()
    {
        Debug.Log("������ ������!");
        gameobject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerSwitchCollider>() != null || other.GetComponent<PlayerMovement>() != null)
        {
            ActivateButton();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerSwitchCollider>() != null || other.GetComponent<PlayerMovement>() != null)
        {
            DisactivateButton();
        }
    }
}