using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    public GameObject moltenGlass;

    private GlassMatController _glassMatController;
    private InstructionController _instructionController;
    private Material _glassMat;

    // Start is called before the first frame update
    void Start()
    {
        _glassMatController = FindObjectOfType<GlassMatController>();
        _glassMat = moltenGlass.GetComponent<MeshRenderer>().material;
        _instructionController = FindObjectOfType<InstructionController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "furnace")
        {
            moltenGlass.GetComponent<MeshRenderer>().enabled = true;
            _instructionController.SetTextContent("Molten glass is got");
        }
        else
        {
            _glassMatController.SetMatColor(_glassMat, other);
        }
    }
}
