using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeOnExitAnimation : StateMachineBehaviour
{
    public List<string> setBoolFalseForString = new List<string>();
    public List<string> setBoolTrueForString = new List<string>();
    
    public List<string> resetTriggerFor = new List<string>();
    public List<string> setTriggerFor = new List<string>();

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("VAR");
        foreach (string s in setBoolFalseForString)
        {
            animator.SetBool(s, false);
        }
        foreach (string s in setBoolTrueForString)
        {
            animator.SetBool(s, true);
        }
        foreach (string s in resetTriggerFor)
        {
            animator.ResetTrigger(s);
        }
        foreach (string s in setTriggerFor)
        {
            animator.SetTrigger(s);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
