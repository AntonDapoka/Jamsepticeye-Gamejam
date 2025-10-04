using Unity.Burst.CompilerServices;
using UnityEngine;

public class ExitLevelDoor : MonoBehaviour
{
    [SerializeField] private GameObject hint;
    [SerializeField] private LevelManager LM;
    [SerializeField] private bool isOnTriggerEnterPlayer = false;
    [SerializeField] private KeyCode switchKey = KeyCode.T;

    private void Start()
    {
        if (hint!=null) hint.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(switchKey) && isOnTriggerEnterPlayer)
        {
            ExitLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerSwitchCollider>() != null)
        {
            isOnTriggerEnterPlayer = true;
            if (hint != null) hint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerSwitchCollider>() != null)
        {
            isOnTriggerEnterPlayer = false;
            if (hint != null) hint.SetActive(false);
        }
    }

    private void ExitLevel()
    {
        LM.SetNextLevel();
    }
}
