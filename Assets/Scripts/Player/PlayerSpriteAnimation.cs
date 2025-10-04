using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerSpriteAnimation : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private enum States
    {
        idle, walk, jump, death, lyingDown, inMidAir
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    public void FlipSprite(int direction)
    {
        transform.localRotation = Quaternion.Euler(0, direction == -1 ? 180 : 0, 0);
    }

    public void SetIdleAnimation() => State = States.idle;

    public void SetWalkAnimation() => State = States.walk;

    public void SetJumpAnimation()
    {
        State = States.jump;
        //StartCoroutine(WaitForJumpAnimation());
    }
    /*
    private IEnumerator WaitForJumpAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        //SetInMidAirAnimation();
        State = States.inMidAir;
    }*/

    //public void SetInMidAirAnimation() => State = States.inMidAir;

    public void SetDeathAnimation()
    {
        State = States.death;
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        SetLyingDownAnimation();
    }

    public void SetLyingDownAnimation() => State = States.lyingDown;
}