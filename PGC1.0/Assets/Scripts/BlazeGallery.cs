using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeGallery : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private float RotationTimeToFacePlayer;
    [SerializeField] private float MoveTime;

    [Header("Game Objects")]
    [SerializeField] private GameObject dialogueBubble;
    [SerializeField] private GameObject dialogueContent;
    [SerializeField] private GameObject blaze_model;


    [Header("Scripts")]
    [SerializeField] private string[] s_Congrat;
    [SerializeField] private AudioClip[] ac_Congrat;

    private AudioSource audioSource;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = blaze_model.GetComponent<Animator>();
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Move Blaze from A to B
    public IEnumerator Move()
    {
        dialogueBubble.SetActive(false);

        // Rotate to face target pos before movement;
        Vector3 targetPos = new Vector3(4.02f, 1, -8.33f);
        Vector3 lookDir = gameObject.transform.position - targetPos;
        lookDir.y = 0; // rotate at Y axis
        Quaternion targetRot = Quaternion.LookRotation(lookDir);
        StartCoroutine(RotateToFace(targetRot));

        // Movement
        float time = 0;
        float duration = MoveTime;
        Vector3 startPos = gameObject.transform.position;

        while (time < duration)
        {
            gameObject.transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = targetPos;


        // Rotate to face player after finishing movement;
        lookDir = gameObject.transform.position - Camera.main.transform.position;
        lookDir.y = 0; // rotate at Y axis
        targetRot = Quaternion.LookRotation(lookDir);
        StartCoroutine(RotateToFace(targetRot));

        StartCoroutine(SpeakNext(s_Congrat, ac_Congrat));
    }

    // Blaze Rotation
    private IEnumerator RotateToFace(Quaternion targetRot)
    {
        float time = 0;

        Quaternion startRot = gameObject.transform.rotation;

        while (time < RotationTimeToFacePlayer)
        {
            gameObject.transform.rotation = Quaternion.Lerp(startRot, targetRot, time / RotationTimeToFacePlayer);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.rotation = targetRot;
    }

    private IEnumerator SpeakNext(string[] scripts, AudioClip[] voices)
    {
        dialogueBubble.SetActive(true);
        animator.SetTrigger("StartSpeak");
        for (int i = 0; i < scripts.Length; i++)
        {
            dialogueContent.GetComponent<TMPro.TextMeshPro>().text = scripts[i];
            audioSource.PlayOneShot(voices[i]);
            float duration = voices[i].length;
            yield return new WaitForSeconds(duration + 1);
        }
        animator.SetTrigger("EndSpeak");
        dialogueBubble.SetActive(false);
    }
}
