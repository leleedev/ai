using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PredatorAttack : PredatorState
{
    protected override void ExecuteTurn()
    {
        List<Vector2Int> adjacent = predator.map.GetAdjacentPositions(predator.GetTilemapPosition(), true);
        foreach(Vector2Int position in adjacent)
        {
            Prey prey = predator.map.GetObjectAtPosition(position)?.GetComponent<Prey>();
            if(prey != null)
            {
                predator.Attack(position);
                return;
            }
        }
    } 
}
