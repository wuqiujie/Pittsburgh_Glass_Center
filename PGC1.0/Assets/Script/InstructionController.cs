using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionController : MonoBehaviour
{
    private GameObject _instruction;

    // Start is called before the first frame update
    void Start()
    {
        _instruction = GameObject.FindGameObjectWithTag("instruction");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextContent(string content)
    {
        _instruction.GetComponent<TMPro.TextMeshPro>().text = content;
    }
}
