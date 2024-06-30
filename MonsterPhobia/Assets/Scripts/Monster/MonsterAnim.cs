using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnim : StateMachineBehaviour
{
    private MonsterBehaviour m_Behaviour;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Behaviour = GameObject.FindWithTag("Enemy").GetComponent<MonsterBehaviour>();
         if (m_Behaviour.Aggroed)
        {
            animator.SetBool("isAggro", true);
        }
        else
        {
            animator.SetBool("isAggro", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
