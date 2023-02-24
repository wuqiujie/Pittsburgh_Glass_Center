using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class StrawController : MonoBehaviour
{
    public GameObject sphere;
    // public Material GlassMat;
    // [SerializeField] private Transform _mainCamera;
    private IEnumerator blowingBigger;
    private IEnumerator blowingSmaller;
    private GameObject moltenGlass;

    public GlassMatController glassMatController;

    private InstructionController _instructionController;
    //private PipeController _pipeController;

    // public GameObject pipe;

    // Start is called before the first frame update
    void Start()
    {
        moltenGlass = GameObject.FindGameObjectWithTag("glass");
        _instructionController = FindObjectOfType<InstructionController>();
       // _pipeController = FindObjectOfType<PipeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCamera")
        {
           // _pipeController.heating = true;

            if (blowingSmaller != null)
            {
                StopCoroutine(blowingSmaller);
                blowingSmaller = null;
            }
            _instructionController.SetTextContent("Blowing...");
            blowingBigger = LerpScale(sphere, sphere.transform.localScale, new Vector3(0.5f, 0.5f, 0.5f), 3);
            StartCoroutine(blowingBigger);
            float currentEmission = moltenGlass.GetComponent<MeshRenderer>().material.GetFloat("_EmissionGradient");
            StartCoroutine(glassMatController.LerpEmission(moltenGlass, currentEmission, 0.015f, 3));
        }   
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
          //  _pipeController.heating = false;
            if (blowingBigger != null)
            {
                StopCoroutine(blowingBigger);
                blowingBigger = null;
            }
            _instructionController.SetTextContent("Blow Finished.");
            StartCoroutine(LerpScale(sphere, sphere.transform.localScale, new Vector3(0.1f, 0.1f, 0.1f), 3));
            float currentEmission = moltenGlass.GetComponent<MeshRenderer>().material.GetFloat("_EmissionGradient");
            StartCoroutine(glassMatController.LerpEmission(moltenGlass, currentEmission, 0.37f, 3));
        }
    }

    private IEnumerator LerpScale(GameObject gameObject, Vector3 startScale, Vector3 targetScale, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            gameObject.transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.localScale = targetScale;
    }
    /*
    private IEnumerator LerpEmission(GameObject glass, float startVal, float endVal, float duration)
    {
        float time = 0;
        Material newMat = Instantiate(moltenGlass.GetComponent<MeshRenderer>().material);
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
    */
}
