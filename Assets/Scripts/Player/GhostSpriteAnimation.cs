using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GhostSpriteAnimation : MonoBehaviour
{
    public void FlipSprite(int direction)
    {
        transform.localRotation = Quaternion.Euler(0, direction == -1 ? 180 : 0, 0);
    }
}
