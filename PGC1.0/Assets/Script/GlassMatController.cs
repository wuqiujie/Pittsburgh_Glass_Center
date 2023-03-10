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
   


    // Start is called before the first frame update
    void Start()
    {
        _colorIndex = 0;
        _instructionController = FindObjectOfType<InstructionController>();
    }

   
    public Material SetMatColor(Material material, Collider other)
    {
        Color color = PickColor(other);

        if(color != Color.black)
        {
            _colorIndex++;
            Debug.Log(_colorIndex);
            switch (_colorIndex)
            {
                case 1:
                    material.SetColor("_Color1", color);
                    break;
                case 2:
                    material.SetColor("_Color2", color);
                   // SwitchState();
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

    public IEnumerator LerpEmission(GameObject glass, float startVal, float endVal, float duration)
    {
        float time = 0;
        Material newMat = Instantiate(glass.GetComponent<MeshRenderer>().material);
        glass.GetComponent<MeshRenderer>().material = newMat;
        while (time < duration)
        {
            float emissionVal = Mathf.Lerp(startVal, endVal, time / duration);
            newMat.SetFloat("_EmissionGradient", emissionVal);
            time += Time.deltaTime;
            yield return null;
        }

        newMat.SetFloat("_EmissionGradient", endVal);
    }

    public void reduceEmission(GameObject glass)
    {
        float newEmission= glass.GetComponent<MeshRenderer>().material.GetFloat("_EmissionGradient")-0.0001f;
        glass.GetComponent<MeshRenderer>().material.SetFloat("_EmissionGradient", newEmission);
    }


}
