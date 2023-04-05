using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{

    public GameObject pipe;
    public GameObject dropGlass;
    private InstructionController _instructionController;
    private GameManager _gameManager;
    private SoundManager _soundManager;

    public enum BatState
    {
        BatStart,
        adjustPipe,
        hit,
        BatEnd
    }
    public BatState currentState;
    void Start()
    {

        _instructionController = FindObjectOfType<InstructionController>();
        _gameManager = FindObjectOfType<GameManager>();
        _soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        if (_gameManager.currentState == GameManager.GameState.Bat)
        {
            currentState = BatState.adjustPipe;
        }
        if(currentState == BatState.adjustPipe)
        {
            adjustPipeRotation();
        }
        if(currentState == BatState.BatEnd)
        {
            //_gameManager.currentState == GameManager.GameState.BatEnd;
        }
    }


    public void adjustPipeRotation()
    {
        pipe.transform.position = new Vector3(10.25f, 2.63f, 3.25f);

        pipe.transform.rotation = Quaternion.Euler(-180, 0, 0);
        currentState = BatState.hit;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pipe")
        {
            _instructionController.SetTextContent("Hit");
            _soundManager.playBatSound();
            currentState = BatState.BatStart;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "pipe")
        {
            _instructionController.SetTextContent("Finish using bat");
            StartCoroutine(LerpScale(dropGlass, dropGlass.transform.localPosition, new Vector3(0.0f, 1.8f, 0.0f), 3));
            currentState = BatState.BatEnd;
        }


    }

    private IEnumerator LerpScale(GameObject gameObject, Vector3 startPos, Vector3 targetPos, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            gameObject.transform.localPosition = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.localPosition = targetPos;
    }
}