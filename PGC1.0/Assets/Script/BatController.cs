using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BatController : MonoBehaviour
{

    public GameObject pipe;
    public GameObject dropGlass;

   // private InstructionController _instructionController;
    private GameManager _gameManager;
    private SoundManager _soundManager;
    private BlazeController _blazeController;
    public GameObject endButton;
    [SerializeField] private GameObject blaze_model;
    private Animator animator;



   /* public enum BatState
    {
        NerverBat,
        BatStart,
        adjustPipe,
        hit,
        BatEnd
    }
    public BatState currentState;
    */
    void Start()
    {

       // _instructionController = FindObjectOfType<InstructionController>();
        _gameManager = FindObjectOfType<GameManager>();
        _soundManager = FindObjectOfType<SoundManager>();
        _blazeController = FindObjectOfType<BlazeController>();
        animator = blaze_model.GetComponent<Animator>();
        //currentState = BatState.NerverBat;
    }

    private void Update()
    {
        if (_gameManager.currentState == GameManager.GameState.BatStart)
        {
            _soundManager.playMagnetic();
            animator.SetTrigger("StartBat");
            _gameManager.SetState(GameManager.GameState.BatAdjust);
        }
        if(_gameManager.currentState == GameManager.GameState.BatAdjust)
        {
            adjustPipeRotation();
        }
    }


    public void adjustPipeRotation()
    {
       
        pipe.transform.position = new Vector3(10.25f, 2.63f, 3.25f);
        pipe.transform.rotation = Quaternion.Euler(-180, 0, 0);
       // _gameManager.SetState(GameManager.GameState.BatHit);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pipe" 
            && _gameManager.currentState != GameManager.GameState.BatEnd)
        {
         //   _instructionController.SetTextContent("Hit");
            _soundManager.playBatSound();
           

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "pipe")
        {
           // _instructionController.SetTextContent("Finish using bat");
            StartCoroutine(LerpPosition(dropGlass, dropGlass.transform.localPosition, new Vector3(0.0f, 1.733f, 0.0f), 3));
            _gameManager.SetState(GameManager.GameState.BatEnd);
            _blazeController.SpeakHandle();
            animator.SetTrigger("EndBat");
        }


    }

    private IEnumerator LerpPosition(GameObject gameObject, Vector3 startPos, Vector3 targetPos, float duration)
    {
        _soundManager.playGlassOff();
        float time = 0;
        while (time < duration)
        {
            gameObject.transform.localPosition = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.localPosition = targetPos;
        _soundManager.playContainer();
        endButton.SetActive(true);
    }
}