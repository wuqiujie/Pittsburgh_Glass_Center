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
    private BlazeController _blazeController;
   // private InstructionController _instructionController;

    [SerializeField] private GameObject blaze_model;
    private Animator animator;


    AudioSource audioSource;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
      //  _instructionController = FindObjectOfType<InstructionController>();
        _soundManager = FindObjectOfType<SoundManager>();
        _blazeController = FindObjectOfType<BlazeController>();
        animator = blaze_model.GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pipe" && !isBlowFinish 
            && _gameManager.currentState == GameManager.GameState.GloryHoleEnd)
        {
            _soundManager.playMagnetic();
            _gameManager.SetState(GameManager.GameState.BlowStart);
            blowTool.SetActive(true);
            adjustPosition();
            _blazeController.SpeakBlow();
            animator.SetTrigger("StartBlow");

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
      //  _instructionController.SetTextContent("Blowing...");
        _soundManager.playBlowSound();
       
    }


    public void endBlowing()
    {
      //  _instructionController.SetTextContent("Blow Finished.");
        _gameManager.SetState(GameManager.GameState.BlowFinish);
        moltenGlassRef.SetActive(false);
        blowTool.SetActive(false);
        isBlowFinish = true;

        animator.SetTrigger("EndBlow");
        _blazeController.SetActionFinished();
        

    }


}
