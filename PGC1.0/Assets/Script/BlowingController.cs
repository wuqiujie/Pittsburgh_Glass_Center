using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowingController : MonoBehaviour
{

    private GameManager _gameManager;
    private SoundManager _soundManager;
    public GameObject blowTool;
    public GameObject pipe;
    public GameObject moltenGlassRef;
    private bool isBlowFinish = false; 

    private InstructionController _instructionController;

   
    AudioSource audioSource;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _instructionController = FindObjectOfType<InstructionController>();
        _soundManager = FindObjectOfType<SoundManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pipe" && !isBlowFinish 
            && _gameManager.currentState == GameManager.GameState.GloryHoleEnd)
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
        pipe.transform.position = new Vector3(7.6f, 0.8f, 3f);

        pipe.transform.rotation = Quaternion.Euler(-90, 0, 0);
       
    }

    public void startBlowing()
    {

        _gameManager.SetState(GameManager.GameState.Blowing);
        _instructionController.SetTextContent("Blowing...");
        _soundManager.playBlowSound();
       
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
