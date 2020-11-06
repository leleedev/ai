using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorSpin : PredatorState
{
    bool clockwise = true;
    int spins = 8;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

       clockwise = Random.Range(0.0f, 100.0f) <= 50.0f;
        animator.SetBool("IsSpinning", true);
        spins = 8;
    }
 
   protected override void ExecuteTurn()
   {
       if(spins <= 0)
       {
           predatorAnimator.SetBool("IsSpinning", false);
       }

       if(clockwise)
       {
           predator.SpinClockwise();
       }
       else
       {
           predator.SpinCClockwise();
       }
       spins--;
       base.ExecuteTurn();
   }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
           predatorAnimator.SetBool("IsSpinning", false);

    }



  
}
