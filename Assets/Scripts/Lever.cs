using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject thirdObject; // ������, ������� ����� �������
    [SerializeField] private KeyCode interactKey = KeyCode.E; // ������� ��������������
    [SerializeField]  private bool playerInRange = false;
    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<GhostMovement>()!=null) // ����� ������� � ����
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<GhostMovement>() != null) // ����� ������� �� ����
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

        // ������� ����� �� 180 ��������
        transform.rotation = Quaternion.Euler(0f, 0f, 230f);

        // ������ ������ ������
        if (thirdObject != null)
        {
            Destroy(thirdObject);
        }
    }
}