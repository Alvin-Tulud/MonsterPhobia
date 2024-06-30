using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : StateMachineBehaviour
{
    private PlayerHurt hit;
    private float timer  = 1.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hit = GameObject.FindWithTag("Player").GetComponent<PlayerHurt>();
        if (hit.isHit)
        {
            animator.SetBool("isDead", true);
        }
        if (stateInfo.IsName("Death"))
        {
            if(timer > 0.0f)
            {
                timer -= 0.04f;
            }
            animator.speed = timer;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
