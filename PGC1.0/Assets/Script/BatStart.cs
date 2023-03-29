using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatStart : MonoBehaviour
{
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pipe")
        {
            _gameManager.SetState(GameManager.GameState.Bat);
        }
    }
}
