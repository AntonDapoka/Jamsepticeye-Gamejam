using UnityEngine;

public class ButtonPlate : MonoBehaviour
{
    public GameObject gameobject;

    private void ActivateButton()
    {
        //Debug.Log("Кнопка нажата!");
        gameobject.SetActive(false);
    }

    private void DisactivateButton()
    {
        //Debug.Log("Кнопка нажата!");
        gameobject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerSwitchCollider>() != null || other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            ActivateButton();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerSwitchCollider>() != null || other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            DisactivateButton();
        }
    }
}