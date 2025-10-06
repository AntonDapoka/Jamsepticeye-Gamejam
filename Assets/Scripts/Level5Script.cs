using UnityEngine;

public class Level5Script : MonoBehaviour
{
    [SerializeField] private ButtonPlate ButtonPlate;
    [SerializeField] private Lever Lever;
    [SerializeField] private GameObject liquid;
    [SerializeField] private GameObject liquidCollider;
    [SerializeField] private GameObject Container;
    [SerializeField] private GameObject ContainerInside;

    private void Update()
    {
        if (!ButtonPlate.isActivatedByPlayer && !Lever.isActivated)
        {
            liquid.SetActive(true);
            liquidCollider.SetActive(true);
            Container.SetActive(false);
            ContainerInside.SetActive(false);
        }
        if (ButtonPlate.isActivatedByPlayer && !Lever.isActivated)
        {
            liquid.SetActive(false);
            liquidCollider.SetActive(false);
            Container.SetActive(false);
            ContainerInside.SetActive(false);
        }
        if (ButtonPlate.isActivatedByPlayer && Lever.isActivated)
        {
            liquid.SetActive(false);
            liquidCollider.SetActive(false);
            Container.SetActive(true);
            ContainerInside.SetActive(false);
        }

        if (!ButtonPlate.isActivatedByPlayer && Lever.isActivated)
        {
            liquid.SetActive(false);
            liquidCollider.SetActive(false);
            Container.SetActive(true);
            ContainerInside.SetActive(true);
        }
    }
}
