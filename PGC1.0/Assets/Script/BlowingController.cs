using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowingController : MonoBehaviour
{

    private GameManager _gameManager;
    public GameObject blowTool;
    public GameObject pipe;
    public GameObject moltenGlassRef;
    private bool isBlowFinish = false;
 

    private InstructionController _instructionController;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _instructionController = FindObjectOfType<InstructionController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pipe" && !isBlowFinish)
        {
            _gameManager.SetState(GameManager.GameState.BlowStart);
            blowTool.SetActive(true);
            adjustPosition();
            
        }
    }

    private void Update()
    {
        if (_gameManager.currentState == GameManager.GameState.BlowStart
            && !isBlowFinish)
        {

            adjustPosition();
        }
    }

    public void adjustPosition()
    {
        pipe.transform.position = new Vector3(10.554f, 0.729f, 1.5f);
        pipe.transform.rotation = Quaternion.Euler(-90, -180, 0);
       
    }

    public void startBlowing()
    {

        _gameManager.SetState(GameManager.GameState.Blowing);
        _instructionController.SetTextContent("Blowing...");
      
    }


    public void endBlowing()
    {
        _instructionController.SetTextContent("Blow Finished.");
        _gameManager.SetState(GameManager.GameState.BlowFinish);
        moltenGlassRef.SetActive(false);
        blowTool.SetActive(false);
        isBlowFinish = true;


    }


}
