using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //GameScene
    public GameObject Furnace;
    public GameObject GloryHole;
    public GameObject ColorTable;
    public GameObject Marver;
    public GameObject Bat;
    public GameObject Seat;
    public GameObject Cooler;




    public GameState state;
    public enum GameState
    {   
        GameStart,
        Furnace,
        ColorTable,
        GloryHole,
        Blow,
        GameEnd
    }

    void Start()
    {
        state = GameState.GameStart;

    }


    void Update()
    {
        Debug.Log("state: " + state);
        if( state == GameState.Furnace)
        {
            gatherGlassFromFurnace();
        }

        if( state == GameState.ColorTable)
        {
            dipIntoColor();
        }
        if ( state == GameState.GloryHole)
        {
            heatTheGlass();
        }  
        if ( state == GameState.Blow)
        {
            blowGlass();
        }
        
        if( state == GameState.GameEnd)
        {
            gameEnd();
        }
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




}
