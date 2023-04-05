using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update


    public AudioClip batSound;
    public AudioClip colorSound;
    public AudioClip blowSound;
    public AudioClip moltenSound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void playBatSound()
    {
        audioSource.PlayOneShot(batSound, 1.0F);
    }

    public void playColorSound()
    {
        audioSource.PlayOneShot(colorSound, 1.0F);
    }
    public void playBlowSound()
    {
        audioSource.PlayOneShot(blowSound, 1.0F);

    }
    public void playMoltenGlass()
    {
        audioSource.PlayOneShot(moltenSound, 1.0F);
    }
}
