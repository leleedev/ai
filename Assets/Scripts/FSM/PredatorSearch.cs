using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorSearch : PredatorState
{
    List<Vector2Int> waypoints = new List<Vector2Int>();
    int currentWaypoint;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        int maxMapSize = predator.map.GetMaxSize() -1;
        
        waypoints.Add(new Vector2Int(0,0));
        waypoints.Add(new Vector2Int(maxMapSize,maxMapSize));
        waypoints.Add(new Vector2Int(0,maxMapSize));
        waypoints.Add(new Vector2Int(maxMapSize,0));
        currentWaypoint = 0;
    }
 
   protected override void ExecuteTurn()
   {
       ExecuteAction();
       base.ExecuteTurn();
   }


    void ExecuteAction()
    {
        if(predator.GetTilemapPosition() == waypoints[currentWaypoint])
        {
            if(currentWaypoint + 1 >= waypoints.Count)
            {
                currentWaypoint = 0;
            }
            else
            {
                currentWaypoint++;
            }
        }

        predator.MoveToward(waypoints[currentWaypoint]);
    }

  
}
