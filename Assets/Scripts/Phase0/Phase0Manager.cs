using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Phase0Manager : MonoBehaviour {

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
        instruction,
        repeat,
        ending,
        finish,
    }

    IEnumerator Start()
    {

        phaseState = STATE.instruction;

        timeCount = 0;

        objGameManager = GameObject.Find("GameManager");
        scriptGameManager = objGameManager.GetComponent<GameManager>();

        objCharacter = Instantiate(prefabCharacter, transform.position, transform.rotation);
                
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;

        yield return new WaitForSeconds(delayToStart);

        myAudioSource.Play();

        yield return new WaitForSeconds(myAudioSource.clip.length);

        if (phaseState == STATE.instruction)
        {
            phaseState = STATE.repeat;
        }
    }
	
	void Update () {

        if (phaseState == STATE.repeat)
        {
            if (!myAudioSource.isPlaying)
            {
                timeCount += Time.deltaTime;

                if (timeCount >= loopInterval)
                {
                    timeCount = 0;

                    myAudioSource.Play();
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
        else if(phaseState == STATE.finish)
        {
            if (!myAudioSource.isPlaying)
            {
                scriptGameManager.nextPhase();
            }
        }
        
    }

    public void changeState()
    {
        phaseState = STATE.ending;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 0");
        myAudioSource.Stop();
        Destroy(objCharacter);
    }
}

