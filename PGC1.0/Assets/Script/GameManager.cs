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
        FurnaceStart,
        FurnaceEnd,
        ColorTable,
        GloryHoleStart,
        GloryHoleEnd,
        BlowStart,
        Blowing,
        BlowFinish,
        Bat,
        GameEnd
    }

    void Start()
    {
        currentState = GameState.GameStart;

    }

    public void SetState(GameState newState)
    {
        currentState = newState;
    }
    private void Update()
    {
        Debug.Log("state :" + currentState);
    }

   



}
