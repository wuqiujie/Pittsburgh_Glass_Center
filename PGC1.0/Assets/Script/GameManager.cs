using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }


    public GameState currentState;
    public enum GameState
    {   
        GameStart,
        Furnace,
        ColorTable,
        GloryHole,
        Blowing,
        BlowFinish,
        Bat,
        GameEnd
    }

    void Start()
    {
        currentState = GameState.GameStart;

    }
    //GameScene
    public GameObject Furnace;
    public GameObject GloryHole;
    public GameObject ColorTable;
    public GameObject Marver;
    public GameObject Bat;
    public GameObject Seat;
    public GameObject Cooler;
    public void SetState(GameState newState)
    {
        currentState = newState;
    }
    private void Update()
    {
        Debug.Log("state :" + currentState);
    }

    /*
    public void SwitchState(GameState newState)
    {
       switch (newState)
        {
            case GameState.Furnace:
                gatherGlassFromFurnace();
                break;
            case GameState.ColorTable:
                dipIntoColor();
                break;
            case GameState.GloryHole:
                heatTheGlass();
                break;

            case GameState.Blow:
                blowGlass();
                break;

            case GameState.GameEnd:
                gameEnd();
                break;
        }
        currentState = newState;
     }
    

    void gatherGlassFromFurnace()
    {

    }
    void dipIntoColor()
    {

    }

    void heatTheGlass()
    {

    }

    void blowGlass()
    {

    }

    void gameEnd()
    {

    }
    */



}
