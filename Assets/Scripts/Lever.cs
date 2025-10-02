using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject thirdObject; // Объект, который нужно удалить
    [SerializeField] private KeyCode interactKey = KeyCode.E; // Клавиша взаимодействия
    [SerializeField]  private bool playerInRange = false;
    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<GhostMovement>()!=null) // Игрок заходит в зону
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<GhostMovement>() != null) // Игрок выходит из зоны
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && !isActivated && Input.GetKeyDown(interactKey))
        {
            ActivateLever();
        }
    }

    private void ActivateLever()
    {
        isActivated = true;

        // Повернём рычаг на 180 градусов
        transform.rotation = Quaternion.Euler(0f, 0f, 230f);

        // Удалим третий объект
        if (thirdObject != null)
        {
            Destroy(thirdObject);
        }
    }
}