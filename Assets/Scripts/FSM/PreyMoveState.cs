using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileAndDistance
{
    public Vector2Int position;
    public float distance;
    public TileAndDistance(Vector2Int _position, float _distance)
    {
        position = _position;
        distance = _distance;
    }
}

public class PreyMoveState : StateMachineBehaviour
{
    Prey prey;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        prey = animator.gameObject.GetComponent<Prey>();
        prey.OnExecuteTurn += ExecuteTurn;
    }

    void ExecuteTurn()
    {
        List<Vector2Int> possibleTiles = prey.map.GetAdjacentPositions(prey.GetTilemapPosition());
        List<TileAndDistance> choices = new List<TileAndDistance>();

        for (int i = 0; i < possibleTiles.Count; i++)
        {
            choices.Add(new TileAndDistance(possibleTiles[i], Vector2Int.Distance(possibleTiles[i], prey.game.predator.GetTilemapPosition())));
        }

        choices.Sort(delegate (TileAndDistance a, TileAndDistance b)
        {
            if (a.distance > b.distance)
            {
                return -1;
            }
            else if (a.distance == b.distance)
            {
                return 0;
            }
            return 1;
        });

        if (choices.Count > 0)
        {
            prey.Move(choices[0].position);
        }

    }

    void OnExitState()
    {
        prey.OnExecuteTurn -= ExecuteTurn;
    }

}
