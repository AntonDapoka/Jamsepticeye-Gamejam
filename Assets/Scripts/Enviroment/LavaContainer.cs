using UnityEngine;

public class LavaContainer : MonoBehaviour
{
    [SerializeField] private GameObject lava;
    [SerializeField] private GameObject lavaInside;

    private void OnEnable()
    {
        lava.SetActive(false);
        lavaInside.SetActive(true);
    }
}
