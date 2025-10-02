using UnityEngine;

public class ButtonPlate : MonoBehaviour
{
    public GameObject gameobject;

    private void ActivateButton()
    {
        Debug.Log("Кнопка нажата!");
        gameobject.SetActive(false);
    }

    private void DisactivateButton()
    {
        Debug.Log("Кнопка нажата!");
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