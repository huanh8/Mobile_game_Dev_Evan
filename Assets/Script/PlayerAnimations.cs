using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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
    public void PlayerCrouchAnimation(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
    public void PlayerDashAnimation()
    {
        animator.SetTrigger("Dash");
    }
}
