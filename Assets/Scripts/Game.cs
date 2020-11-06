using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject predatorPrefab;
    public GameObject preyPrefab;
    public GameObject mapPrefab;


   public Predator predator;
   public List<Prey> preys = new List<Prey>();
   public float timeBetweenTurns = 0.1f;
   Map map;

   public int currentTurn;
   public int preysLeft;

    void Start()
    {
        StartRun();
    }

    void StartRun()
    {
        map = GameObject.Instantiate(mapPrefab, new Vector3(0,0,0), Quaternion.identity).GetComponent<Map>();
        int startingPreys = Random.Range(5, 11);
        for(int i =0; i < startingPreys; i++)
        {
            Vector2Int preyPos = new Vector2Int(0,0);
            do
            {
                preyPos = new Vector2Int(Random.Range(0,30), Random.Range(0,30));
            } while (!map.IsEmpty(preyPos));

            AddPrey(preyPos);
        }

        Vector2Int pos = new Vector2Int();
        do
        {
            pos = new Vector2Int(Random.Range(0,30), Random.Range(0,30));
        } while (!map.IsEmpty(pos));
        predator = AddPredator(pos);

        currentTurn = 0;
        preysLeft = startingPreys;
        
       StartCoroutine(TurnRepeat());
    }

    // Update is called once per frame
    void Update()
    {
        //ExecuteTurn();
    }

    void ExecuteTurn()
    {
        predator.ExecuteTurn();
        for(int i = preys.Count -1; i >= 0; i--)
        {
            Prey prey = preys[i];
            if(prey != null)
            {
                prey.ExecuteTurn();
            }
            else
            {
                preys.RemoveAt(i);
               
            }
        }
        preysLeft = preys.Count;
        currentTurn++;
    }

    IEnumerator TurnRepeat()
    {
        ExecuteTurn();
        yield return new WaitForSeconds(timeBetweenTurns);
        if(preysLeft > 0)
        {
            StartCoroutine(TurnRepeat());
        }
    }

    public Prey AddPrey(Vector2Int pos)
    {
        GameObject preyObject = GameObject.Instantiate(preyPrefab);
        Prey preyComponent = preyObject.GetComponent<Prey>();
        
        map.Set(pos, preyObject);
        preyComponent.Initialize(pos, map, this);
        
        preys.Add(preyComponent);
        return preyComponent;
    }

    public Predator AddPredator(Vector2Int pos)
    {
        Predator predator = GameObject.Instantiate(predatorPrefab).GetComponent<Predator>();
        map.Set(pos, predator.gameObject);
        predator.Initialize(pos, map, this);
        return predator;
    }

}
