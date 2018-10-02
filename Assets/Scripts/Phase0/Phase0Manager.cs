using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Phase0Manager : MonoBehaviour
{

    public float delayToStart = 1;
    public float loopInterval = 3;

    public AudioClip soundInstruction;
    public AudioClip soundCongrats;

    public GameObject prefabCharacter;

    private AudioSource myAudioSource;
    private GameObject objCharacter;
    private GameObject objGameManager;
    private GameManager scriptGameManager;
    private float timeCount;

    private STATE phaseState;
    private enum STATE
    {
        enabling,
        instruction,
        repeat,
        ending,
        finish,
    }

    private void OnEnable()
    {
        phaseState = STATE.enabling;

        timeCount = 0;

        objGameManager = GameObject.Find("GameManager");
        scriptGameManager = objGameManager.GetComponent<GameManager>();

        objCharacter = Instantiate(prefabCharacter, prefabCharacter.transform.position, prefabCharacter.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;
    }

    void Update()
    {

        if (phaseState == STATE.enabling)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= delayToStart)
            {
                myAudioSource.Play();
                phaseState = STATE.instruction;
                timeCount = 0;
            }
        }
        else if (phaseState == STATE.instruction)
        {
            if (!myAudioSource.isPlaying)
            {
                phaseState = STATE.repeat;
            }
        }
        else if (phaseState == STATE.repeat)
        {
            if (!myAudioSource.isPlaying)
            {
                timeCount += Time.deltaTime;

                if (timeCount >= loopInterval)
                {
                    myAudioSource.Play();
                    timeCount = 0;
                }
            }
        }
        else if (phaseState == STATE.ending)
        {
            myAudioSource.Stop();
            myAudioSource.clip = soundCongrats;
            myAudioSource.Play();
            phaseState = STATE.finish;
        }
        else if (phaseState == STATE.finish)
        {
            if (!myAudioSource.isPlaying)
            {
                scriptGameManager.nextPhase();
            }
        }
    }

    public void changeState()
    {
        if (phaseState != STATE.finish)
        {
            phaseState = STATE.ending;
        }
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 0");

        myAudioSource.Stop();
        Destroy(objCharacter);
    }
}

