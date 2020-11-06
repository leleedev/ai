using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Game game;
    public Text preysCount;

    public Text turnCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        preysCount.text = game.preysLeft.ToString();
        turnCount.text = game.currentTurn.ToString();
    }
}
