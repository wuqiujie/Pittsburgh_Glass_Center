using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloryHoleController : MonoBehaviour
{

    public GameObject moltenGlass;
    public GlassMatController glassMatController;

    public void start()
    {
        float currentEmission = moltenGlass.GetComponent<MeshRenderer>().material.GetFloat("_EmissionGradient");
        StartCoroutine(glassMatController.LerpEmission(moltenGlass, currentEmission, 1f, 3));
    }

}
