using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private Animator effectAnimoter;

    void Start()
    {
        //get animator from windEffect
        animator = GetComponent<Animator>();
        //get animator from child
        effectAnimoter = transform.GetChild(2).GetComponent<Animator>();
        effectAnimoter.enabled = false;
    }
    public void PlayWalkAnimation(float horizontal)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }
    public void PlayerJumpAnimation(bool isJumping)
    {
        animator.SetBool("IsJumping", isJumping);
    }
    //attack animation  and attack animation     
    public void PlayerAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
    public void PlayerIsHurtAnimation()
    {
        animator.SetTrigger("IsHurt");
    }
    public void PlayerIsDeadAnimation()
    {
        animator.SetBool("IsDead", true);
    }
    public void PlayerShootingAnimation()
    {
        animator.SetTrigger("Shooting");
    }
    public void PlayerCrouchAnimation(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
    public void PlayerDashAnimation(bool IsDashing)
    {
        if (IsDashing)
        {
            animator.SetTrigger("Dash");
            effectAnimoter.enabled = true;
            effectAnimoter.SetTrigger("DashingWind");
        }
    }
    public void PlayerBlockAnimation(bool IsBlocking)
    {
        animator.SetBool("IsBlocking", IsBlocking);
    }
}
