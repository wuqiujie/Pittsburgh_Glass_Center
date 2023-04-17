using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public GameObject glass;
    public GameObject camera;
    public void changeScene()
    {
        SceneManager.LoadScene("ArtGallery_Scene", LoadSceneMode.Additive);
    }
    private void Awake()
    {
        DontDestroyOnLoad(glass);
        DontDestroyOnLoad(camera);
    }


}
