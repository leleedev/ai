using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : TilemapObject
{
    public TilemapObject TargetPrey;

    Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    public override void Move(Vector2Int targetPos) 
    {
        Vector3 newForward = map.GetWorldPosition(targetPos) - transform.position;
        transform.rotation = Quaternion.LookRotation(newForward , Vector3.up);
        base.Move(targetPos);
    }

    public void MoveToward(Vector2Int targetPos)
    {
         List<Vector2Int> possibleTiles = map.GetAdjacentPositions(GetTilemapPosition());
        List<TileAndDistance> choices = new List<TileAndDistance>();

        for (int i = 0; i < possibleTiles.Count; i++)
        {
            choices.Add(new TileAndDistance(possibleTiles[i], Vector2Int.Distance(possibleTiles[i], targetPos)));
        }

        choices.Sort(delegate (TileAndDistance a, TileAndDistance b)
        {
            if (a.distance > b.distance)
            {
                return 1;
            }
            else if (a.distance == b.distance)
            {
                return 0;
            }
            return -1;
        });

        if(choices.Count > 0)
        {
            Move(choices[0].position);
        }
    }

    bool CanAttack()
    {
        List<Vector2Int> adjacents = map.GetAdjacentPositions(GetTilemapPosition(), true);
        foreach(Vector2Int position in adjacents)
        {
            GameObject obj = map.GetObjectAtPosition(position);
            if(obj?.GetComponent<Prey>() != null)
            {
                return true;
            }
        }
        return false;
    }

    public override void  ExecuteTurn()
    {
        if(CanAttack())
        {
            animator.SetTrigger("CanAttack");
        }
        else
        {
            CheckForPrey();
            animator.SetBool("HasTarget", TargetPrey != null);
        }
        
        

        base.ExecuteTurn();
    }

    void CheckForPrey()
    {
        if(TargetPrey == null)
        {
            Vector3 forward = transform.forward;
            Vector2Int direction = new Vector2Int(Mathf.RoundToInt(forward.x), Mathf.RoundToInt(Mathf.RoundToInt(forward.z)));
            Vector2Int pos = GetTilemapPosition();
           for(int i = 1; i <= 5; i++)  
           {
               Prey prey = map.GetObjectAtPosition(pos + direction * i)?.GetComponent<Prey>();
               if(prey != null)
               {
                   TargetPrey = prey;
                   return;
               }
           }
        }
    }

    public void Attack(Vector2Int position)
    {
        map.Set(position, null);
    }

    public void SpinClockwise()
    {
        transform.Rotate(new Vector3(0,45,0), Space.World);
    }

    public void SpinCClockwise()
    {
        transform.Rotate(new Vector3(0,-45,0), Space.World);
    }


}
