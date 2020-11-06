using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorPursuit : PredatorState
{
    protected override void ExecuteTurn()
    {
        if(predator.TargetPrey == null)
        {
            return;
        }

       predator.MoveToward(predator.TargetPrey.GetTilemapPosition());
    }
}
