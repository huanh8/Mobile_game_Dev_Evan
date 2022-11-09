using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_movement : StateMachineBehaviour
{
    Transform player;
    FlipPlayer flipBoss;
    Rigidbody2D rb;
    Health health;
    public float speed = 2f;
    public float attackRange = 3f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        flipBoss = animator.GetComponent<FlipPlayer>();
        rb = animator.GetComponent<Rigidbody2D>();
        health = animator.GetComponent<Health>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null) return;
        if (health.IsDead) return;
        // move towards player
        Vector2 target = new Vector2(player.position.x, animator.transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(animator.transform.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        //flip the boss
        flipBoss.Filp(animator.transform.position.x - newPos.x);
        //check if the boss is close to the player
        if (Vector2.Distance(player.position, animator.transform.position) <= attackRange)
            animator.SetTrigger("Attack");

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }


}
