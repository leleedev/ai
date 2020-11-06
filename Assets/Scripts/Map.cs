using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject TilePrefab;
    List<GameObject> tiles = new List<GameObject>();
    public float tileSize = 1;
    public float tileSpacing = 0.5f;

    int maxSize = 30;
    GameObject[,] tileMap = new GameObject[30,30];

    Vector2Int[] neighborChecks = new Vector2Int[8] { new Vector2Int(0,1), new Vector2Int(-1,1), new Vector2Int(-1,0), new Vector2Int(-1,-1), 
                                                         new Vector2Int(0,-1), new Vector2Int(1,-1), new Vector2Int(1, 0), new Vector2Int(1,1) };

   void Start()
   {
       for(int x = 0; x < 30; x++)
       {
           for(int z = 0; z < 30; z++)
           {
               GameObject newTile = GameObject.Instantiate(TilePrefab);
               newTile.transform.parent = transform;
               tiles.Add(newTile);
               newTile.transform.localPosition = GetWorldPosition(new Vector2Int(x,z));
           }
       }
   }
    
    public void Set(Vector2Int pos, GameObject gameObject)
    {
        GameObject objectAtPosition = tileMap[pos.x,pos.y];
        if(gameObject == null && objectAtPosition != null)
        {
            GameObject.Destroy(objectAtPosition);
        }
        tileMap[pos.x,pos.y] = gameObject;
    }

    public bool Move(Vector2Int from, Vector2Int to)
    {
        if(!IsEmpty(to)) return false;
        GameObject gameObject = GetObjectAtPosition(from);
        if(gameObject == null) return false;

        tileMap[from.x, from.y] = null;
        tileMap[to.x, to.y] = gameObject;

        return true;
    }

    public List<Vector2Int> GetAdjacentPositions(Vector2Int pos, bool ignoreOccupied = false)
    {
        List<Vector2Int> possibleDestinations = new List<Vector2Int>(8);

        foreach(Vector2Int direction in neighborChecks)
        {
            Vector2Int checkPosition = pos + direction;
            if(IsValidPosition(checkPosition) && (ignoreOccupied || IsEmpty(checkPosition)))
            {
                possibleDestinations.Add(checkPosition);
            }
        }

        return possibleDestinations;
    }


    public bool IsValidPosition(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < 30 && pos.y < 30;
    }

    public bool IsEmpty(Vector2Int pos)
    {
        if(!IsValidPosition(pos)) return false;
        return tileMap[pos.x, pos.y] == null;
    }

    public GameObject GetObjectAtPosition(Vector2Int pos)
    {
        if(!IsValidPosition(pos)) return null;
        return tileMap[pos.x, pos.y];
    }

    public Vector3 GetWorldPosition(Vector2Int pos)
    {
        return new Vector3(pos.x * (tileSize + tileSpacing), 0, pos.y * (tileSize + tileSpacing));
    }

   public int GetMaxSize()
    {
        return maxSize;
    }
}
