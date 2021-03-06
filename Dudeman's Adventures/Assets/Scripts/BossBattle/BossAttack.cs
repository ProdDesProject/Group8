﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Boss boss;

    public float speed = 0f;
    public float attackRange = 5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // save player to variable
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // get boss Rigidbody2D
        rb = animator.GetComponent<Rigidbody2D>();

        // get boss Object
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // call for function to flip the boss so he is always facing the way where the player is
        boss.LookAtPlayer();

        // get target and move boss to target
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        // if player is not in attack range, boss goes back to running animation
        if (Vector2.Distance(player.position, rb.position) >= attackRange)
        {
            animator.SetBool("IsInRange", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
