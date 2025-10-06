using UnityEngine;

public class LavaContainer : MonoBehaviour
{
    [SerializeField] private GameObject lava;
    [SerializeField] private GameObject lavaInside;

    private void OnEnable()
    {
        lava.SetActive(false);
        lavaInside.SetActive(false);
    }

    private void OnDisable()
    {
        lava.SetActive(true);
        lavaInside.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf && lava.activeSelf)
        {
            lava.SetActive(false);
            lavaInside.SetActive(true);
        }
    }
}
