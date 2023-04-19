using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    public GameObject moltenGlass;
    public GameObject pipe;
    public GameObject frit;

    private GlassMatController _glassMatController;
    //private InstructionController _instructionController;
    private Material _glassMat;
    private GameManager _gameManager;
    private BlazeController _blazeController;
    //[SerializeField] private GameObject blaze_model;
   // private Animator animator;


    public bool heating = false;
   // public GameObject Gloryhole;
    private float curremission;

    private SoundManager _soundManager;



    // Start is called before the first frame update
    void Start()
    {
        _glassMatController = FindObjectOfType<GlassMatController>();
       
        //_instructionController = FindObjectOfType<InstructionController>();
        curremission = moltenGlass.GetComponent<MeshRenderer>().material.GetFloat("_EmissionGradient");
        _gameManager = FindObjectOfType<GameManager>();
        _soundManager = FindObjectOfType<SoundManager>();
        _blazeController = FindObjectOfType<BlazeController>();
        //animator = blaze_model.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        curremission = moltenGlass.GetComponent<MeshRenderer>().material.GetFloat("_EmissionGradient");
        if (moltenGlass.GetComponent<MeshRenderer>().enabled 
            && curremission >=0.06f
            && heating ==false
            && _gameManager.currentState != GameManager.GameState.BlowStart
            && _gameManager.currentState!= GameManager.GameState.Blowing)
        {
            _glassMatController.reduceEmission(moltenGlass);
          
        }

        if (_gameManager.currentState  == GameManager.GameState.FurnaceStart)
        {
            rotatePipe();
        }

    }
    public void grabPipe()
    {
        if(_gameManager.currentState == GameManager.GameState.GameStart)
        {
            _blazeController.SetActionFinished();
            _gameManager.SetState(GameManager.GameState.Pipe);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "furnace"
            && _gameManager.currentState == GameManager.GameState.Pipe)
        {
            moltenGlass.GetComponent<MeshRenderer>().enabled = true;
            heating = true;
           // _instructionController.SetTextContent("You got Molten glass");
            _gameManager.SetState(GameManager.GameState.FurnaceStart);

            _soundManager.playMoltenGlass();
           
        }
        if(other.tag== "water"
            && _gameManager.currentState == GameManager.GameState.FurnaceEnd)
        {
            _gameManager.SetState(GameManager.GameState.WaterStart);
        }

        if ((other.tag == "red" || other.tag == "green" || other.tag == "purple" 
            || other.tag == "yellow" || other.tag == "blue" || other.tag == "white")
            && 
            (_gameManager.currentState == GameManager.GameState.WaterFinish
            || _gameManager.currentState == GameManager.GameState.ColorStart))
        {
            _gameManager.SetState(GameManager.GameState.ColorStart);
            _glassMat = moltenGlass.GetComponent<MeshRenderer>().material;
            _glassMatController.SetMatColor(_glassMat, other);
            _soundManager.playColorSound();
            other.GetComponent<MeshCollider>().enabled = false;
            frit.SetActive(true);
           
        }
        if (other.tag == "gloryhole"
             && _gameManager.currentState == GameManager.GameState.ColorEnd)
        {
            heating = true;
            moltenGlass.GetComponent<MeshRenderer>().material.SetFloat("_EmissionGradient", 0.5f);
           // _instructionController.SetTextContent("You reheat Molten glass,stay 3s");
            _gameManager.SetState(GameManager.GameState.GloryHoleStart);
            _blazeController.Speak123();
            _soundManager.playMoltenGlass();
            frit.SetActive(false);
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "furnace"
            && _gameManager.currentState == GameManager.GameState.FurnaceStart)
        {
            heating = false;
            Debug.Log("speak");
            _blazeController.SetActionFinished();
            _gameManager.SetState(GameManager.GameState.FurnaceEnd);     
        }
        if (other.tag == "water"
            && _gameManager.currentState == GameManager.GameState.WaterStart)
        {
            _blazeController.SetActionFinished();
            _gameManager.SetState(GameManager.GameState.WaterFinish);
        }

        if (other.tag == "gloryhole"
            && _gameManager.currentState == GameManager.GameState.GloryHoleStart)
        {
            heating = false;
            _blazeController.SetActionFinished();
            _gameManager.SetState(GameManager.GameState.GloryHoleEnd);
        }
    }
    private void rotatePipe()
    {
        pipe.transform.Rotate(0.0f, 0.0f, 1.0f, Space.World);
    }
}
