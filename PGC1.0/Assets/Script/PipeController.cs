using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    public GameObject moltenGlass;

    private GlassMatController _glassMatController;
    private InstructionController _instructionController;
    private Material _glassMat;
    private GameManager _gameManager;


    public bool heating = false;
    public GameObject Gloryhole;
    private float curremission;


    // Start is called before the first frame update
    void Start()
    {
        _glassMatController = FindObjectOfType<GlassMatController>();
       
        _instructionController = FindObjectOfType<InstructionController>();
        curremission = moltenGlass.GetComponent<MeshRenderer>().material.GetFloat("_EmissionGradient");
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        curremission = moltenGlass.GetComponent<MeshRenderer>().material.GetFloat("_EmissionGradient");
        // _instructionController.SetTextContent("current emission " + curremission);
        if (moltenGlass.GetComponent<MeshRenderer>().enabled 
            && curremission >=0.06f
            && heating ==false
            && _gameManager.currentState!= GameManager.GameState.Blowing)
        {
            _glassMatController.reduceEmission(moltenGlass);
          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "furnace")
        {
            moltenGlass.GetComponent<MeshRenderer>().enabled = true;
            heating = true;
            _instructionController.SetTextContent("You got Molten glass");
            _gameManager.SetState(GameManager.GameState.FurnaceStart);
        }
        if (other.tag == "gloryhole")
        {
            heating = true;
            Debug.Log("gloryhole");
            // Gloryhole.GetComponent<GloryHoleController>().enabled = true;
            // moltenGlass.GetComponent<MeshRenderer>().material.SetFloat("_EmissionGradient", 1);
            //StartCoroutine(_glassMatController.LerpEmission(moltenGlass, curremission, 0.5f, 3));
            moltenGlass.GetComponent<MeshRenderer>().material.SetFloat("_EmissionGradient", 0.5f);
            _instructionController.SetTextContent("You reheat Molten glass,stay 3s");
            _gameManager.SetState(GameManager.GameState.GloryHoleStart);
        }
       
        if (other.tag == "red" || other.tag == "green" || other.tag == "purple" || other.tag == "yellow" || other.tag == "blue" || other.tag == "white")
        {
            _glassMat = moltenGlass.GetComponent<MeshRenderer>().material;
            _glassMatController.SetMatColor(_glassMat, other);
            //_instructionController.SetTextContent("Got color " + other.tag);
        }
 
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other .tag == "furnace")
        {
            heating = false;
            _gameManager.SetState(GameManager.GameState.FurnaceEnd);     
        }

        if (other.tag == "gloryhole")
        {
            heating = false;
            _gameManager.SetState(GameManager.GameState.GloryHoleEnd);
        }
    }
}
