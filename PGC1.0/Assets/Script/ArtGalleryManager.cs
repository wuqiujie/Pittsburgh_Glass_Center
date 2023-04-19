using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtGalleryManager : MonoBehaviour
{

    private GameObject glass;
    private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
        glass = GameObject.FindGameObjectWithTag("glass");
        glass.transform.position = new Vector3(4f, 1.95f, -7.48199987f);


        camera = GameObject.FindGameObjectWithTag("camera");
        camera.transform.position = new Vector3(4.19700003f, 0.852999985f, -6.52699995f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
