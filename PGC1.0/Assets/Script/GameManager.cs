using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Pipe,
        FurnaceStart,
        FurnaceEnd,
        WaterStart,
        WaterFinish,
        ColorStart,
        ColorEnd,
        GloryHoleStart,
        GloryHoleEnd,
        BlowStart,
        Blowing,
        BlowFinish,
        BatStart,
        BatAdjust,
        BatHit,
        BatEnd,
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
