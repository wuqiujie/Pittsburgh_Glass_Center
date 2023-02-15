using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassMatController : MonoBehaviour
{
    // private Material _mat;
    // private float _emissionGradient;
    // private Color _color1;
    // private Color _color2;

    [SerializeField] private Color Red;
    [SerializeField] private Color Blue;
    [SerializeField] private Color Yellow;
    [SerializeField] private Color Green;
    [SerializeField] private Color Purple;

    private InstructionController _instructionController;

    private int _colorIndex;

    public bool getColor = false;

    // Start is called before the first frame update
    void Start()
    {
        _colorIndex = 0;
        _instructionController = FindObjectOfType<InstructionController>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public Material SetMatColor(Material material, Collider other)
    {
        Color color = PickColor(other);

        if(color != Color.black)
        {
            _colorIndex++;
            if(_colorIndex == 2)
            {
                getColor = true;
            }
            Debug.Log(_colorIndex);
            switch (_colorIndex)
            {
                case 1:
                    material.SetColor("_Color1", color);
                    break;
                case 2:
                    material.SetColor("_Color2", color);
                    break;
                default:
                    break;
            }
        }
            
        return material;
    }

    public Color PickColor(Collider other)
    {

        if( _colorIndex < 2)
        {
            switch (other.tag)
            {
                case "red":
                    Debug.Log("pick reeedd");
                    _instructionController.SetTextContent("Red picked.");
                    return Red;
                case "blue":
                    Debug.Log("pick bluuue");
                    _instructionController.SetTextContent("Blue picked.");
                    return Blue;
                case "yellow":
                    Debug.Log("pick yellow");
                    _instructionController.SetTextContent("Yellow picked.");
                    return Yellow;
                case "green":
                    Debug.Log("pick greeeeen");
                    _instructionController.SetTextContent("Green picked.");
                    return Green;
                case "purple":
                    Debug.Log("pick purple");
                    _instructionController.SetTextContent("Purple picked.");
                    return Purple;
                default:
                    return Color.black;
            }
        }
        else
            return Color.black;
    }


}
