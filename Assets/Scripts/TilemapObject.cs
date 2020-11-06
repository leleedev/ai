using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TilemapObject : MonoBehaviour
{
    public event Action OnExecuteTurn;
    Vector2Int tileMapPosition;
    public Map map;
    public Game game;
   public void Initialize(Vector2Int pos, Map mapObject, Game gameObject)
   {
       tileMapPosition = pos;
       map = mapObject;
       transform.position = mapObject.GetWorldPosition(pos);
       game = gameObject;
   }

   public virtual void ExecuteTurn()
   {
       StartCoroutine(ExecuteTurnNextFrame());
   }

    public virtual void Move(Vector2Int targetPos)
    {
        if(map.Move(tileMapPosition, targetPos))
        {
            tileMapPosition = targetPos;
            transform.position = map.GetWorldPosition(tileMapPosition);
        }
    }

    public Vector2Int GetTilemapPosition()
    {
        return tileMapPosition;
    }

    IEnumerator ExecuteTurnNextFrame()
    {
        yield return new WaitForSeconds(0.01f);
       OnExecuteTurn?.Invoke();
    }
}
