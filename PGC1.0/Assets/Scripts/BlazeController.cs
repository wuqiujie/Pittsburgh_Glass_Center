using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeController : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private float RotationTimeToFacePlayer;
    [SerializeField] private float MoveTime;

    [Header("Game Objects")]
    [SerializeField] private GameObject dialogueBubble;
    [SerializeField] private GameObject dialogueContent;
    [SerializeField] private GameObject readyButton;
    [SerializeField] private GameObject blaze_model;

    private Vector3[] TargetPositions;
    private List<string[]> Scripts;
    private List<AudioClip[]> Scripts_Sound;

    [Header("Move Positions")]
    public Vector3 PipePos;
    public Vector3 FurnacePos;
    public Vector3 WaterPos;
    public Vector3 ColorPos;
    public Vector3 GloryPos;
    public Vector3 BenchPos;
    public Vector3 BatPos;

    [SerializeField] private string[] Instruction_Summary;

    [Header("Welcome")]
    [SerializeField] private string[] s_Welcome;
    [SerializeField] private AudioClip[] ac_Welcome;
    [SerializeField] private string s_Start;
    [SerializeField] private AudioClip ac_Start;

    [Header("Pipe")]
    [SerializeField] private string[] s_Pipe;
    [SerializeField] private AudioClip[] ac_Pipe;

    [Header("Furnace")]
    [SerializeField] private string[] s_Furnace;
    [SerializeField] private AudioClip[] ac_Furnace;

    [Header("Water")]
    [SerializeField] private string[] s_Water;
    [SerializeField] private AudioClip[] ac_Water;

    [Header("Color")]
    [SerializeField] private string[] s_Color;
    [SerializeField] private AudioClip[] ac_Color;

    [Header("Glory")]
    [SerializeField] private string[] s_Glory;
    [SerializeField] private AudioClip[] ac_Glory;
    [SerializeField] private string[] s_123;
    [SerializeField] private AudioClip[] ac_123;

    [Header("Bench")]
    [SerializeField] private string[] s_Bench;
    [SerializeField] private AudioClip[] ac_Bench;
    [SerializeField] private string[] s_Blow;
    [SerializeField] private AudioClip[] ac_Blow;

    [Header("Bat")]
    [SerializeField] private string[] s_Bat;
    [SerializeField] private AudioClip[] ac_Bat;
    [SerializeField] private string[] s_Handle;
    [SerializeField] private AudioClip[] ac_Handle;

    [Header("Feedback")]
    [SerializeField] private string[] s_Feedback;
    [SerializeField] private AudioClip[] ac_Feedback;

    private int current_pos_index = 0;
    private AudioSource audioSource;
    private Animator animator;
    private bool isCurrentActionFinished = false;
    private bool isSpeakFinished = false;
  

    // Start is called before the first frame update
    void Start()
    {
        TargetPositions = new Vector3[8];
        Scripts = new List<string[]>();
        Scripts_Sound = new List<AudioClip[]>();
        audioSource = GetComponent<AudioSource>();
        setTargetPos();
        setScripts();
        StartCoroutine(Move());
        animator = blaze_model.GetComponent<Animator>();
    }

    private void setTargetPos()
    {
        Vector3 PlayerPos = Camera.main.transform.position;
        Vector3 BlazeStartPos = new Vector3(PlayerPos.x, 0, PlayerPos.z - 1);
        TargetPositions[0] = BlazeStartPos;
        TargetPositions[1] = PipePos;
        TargetPositions[2] = FurnacePos;
        TargetPositions[3] = WaterPos;
        TargetPositions[4] = ColorPos;
        TargetPositions[5] = GloryPos;
        TargetPositions[6] = BenchPos;
        TargetPositions[7] = BatPos;
    }
    private void setScripts()
    {
        Scripts.Add(s_Welcome);
        Scripts.Add(s_Pipe);
        Scripts.Add(s_Furnace);
        Scripts.Add(s_Water);
        Scripts.Add(s_Color);
        Scripts.Add(s_Glory);
        Scripts.Add(s_Bench);
        Scripts.Add(s_Bat);
        Scripts_Sound.Add(ac_Welcome);
        Scripts_Sound.Add(ac_Pipe);
        Scripts_Sound.Add(ac_Furnace);
        Scripts_Sound.Add(ac_Water);
        Scripts_Sound.Add(ac_Color);
        Scripts_Sound.Add(ac_Glory);
        Scripts_Sound.Add(ac_Bench);
        Scripts_Sound.Add(ac_Bat);
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
        Vector3 targetPos = TargetPositions[current_pos_index];
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
        // in the bat state, rotate to face forward
        if (current_pos_index == 7)
            targetRot = new Quaternion(0, 0, 0, 0);

        StartCoroutine(RotateToFace(targetRot));

        StartCoroutine(Speak());
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

    // Blaze starts a new dialogue
    public IEnumerator Speak()
    {
        string[] scripts = Scripts[current_pos_index];
        AudioClip[] voices = Scripts_Sound[current_pos_index];
        
        yield return new WaitForSeconds(RotationTimeToFacePlayer);
        
        dialogueBubble.SetActive(true);
        isSpeakFinished = false;
        animator.SetTrigger("StartSpeak");
        for(int i = 0; i < scripts.Length; i++)
        {
            dialogueContent.GetComponent<TMPro.TextMeshPro>().text = scripts[i];
            audioSource.PlayOneShot(voices[i]);
            float duration = voices[i].length;
            yield return new WaitForSeconds(duration + 1);
        }
        animator.SetTrigger("EndSpeak");

        // current action finished
        if (isCurrentActionFinished)
            StartCoroutine(SpeakFeedback());

        // dialogueBubble.SetActive(false);
        isSpeakFinished = true;
        dialogueContent.GetComponent<TMPro.TextMeshPro>().text = Instruction_Summary[current_pos_index];

        // if in the welcoming stage
        if (current_pos_index == 0)
        {
            dialogueBubble.SetActive(false);
            readyButton.SetActive(true);
        }
    }

    public void SetActionFinished()
    {
        isCurrentActionFinished = true;
        if (isSpeakFinished)
            StartCoroutine(SpeakFeedback());
    }

    public void ReadyToStart()
    {
        audioSource.PlayOneShot(ac_Start);
        MoveToNextStage();
        readyButton.SetActive(false);
    }

    public void Speak123()
    {
        StartCoroutine(SpeakNext(s_123, ac_123));
    }

    public void SpeakBlow()
    {
        StartCoroutine(SpeakNext(s_Blow, ac_Blow));
    }

    public void SpeakHandle()
    {
        StartCoroutine(SpeakNext(s_Handle, ac_Handle));
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

    private IEnumerator SpeakFeedback()
    {
        animator.SetTrigger("StartCheer");
        AudioClip randomFeedback = ac_Feedback[Random.Range(0, ac_Feedback.Length)];
        audioSource.PlayOneShot(randomFeedback);
        float duration = randomFeedback.length;
        yield return new WaitForSeconds(duration + 1);
        animator.SetTrigger("EndCheer");
        MoveToNextStage();
    }

    private void MoveToNextStage()
    {
        current_pos_index++;
        StartCoroutine(Move());
        isCurrentActionFinished = false;
    }

}
