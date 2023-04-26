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

    public AudioClip containerSound;
    public AudioClip glassOffSound;
    public AudioClip magneticSound;
    public AudioClip waterSound;

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


    public void playWater()
    {
        audioSource.PlayOneShot(waterSound, 0.1F);
    }

    public void playContainer()
    {
        audioSource.PlayOneShot(containerSound, 1.0F);
    }
    public void playGlassOff()
    {
        audioSource.PlayOneShot(glassOffSound, 1.0F);
    }
    public void playMagnetic()
    {
        audioSource.PlayOneShot(magneticSound, 1.0F);
    }

}
