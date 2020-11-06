using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorState : StateMachineBehaviour
{
    protected Predator predator;
    protected Animator predatorAnimator;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        predatorAnimator = animator;
        predator = animator.gameObject.GetComponent<Predator>();
        predator.OnExecuteTurn += ExecuteTurn;
    }

    protected virtual void ExecuteTurn()
    {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator,stateInfo,layerIndex);
        predator.OnExecuteTurn -= ExecuteTurn;
    }

}
