using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Phase0Manager : MonoBehaviour {

    public float loopInterval = 3;
    public GameObject prefabCharacter;
    public AudioClip[] sounds;
    
    //[HideInInspector]public int endPhase;

    private AudioSource myAudioSource;
    private GameObject objCharacter;
    private GameObject objGameManager;
    private GameManager scriptGameManager;
    private float timeCount;

    private STATE phaseState;
    private enum STATE
    {
        repeat,
        ending,
        finish,
    }

    void OnEnable()
    {

        objGameManager = GameObject.Find("GameManager");
        scriptGameManager = objGameManager.GetComponent<GameManager>();

        objCharacter = Instantiate(prefabCharacter, transform.position, transform.rotation);

        phaseState = STATE.repeat;
        timeCount = 0;

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = sounds[0];
        myAudioSource.Play();

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

                    myAudioSource.Stop();
                    myAudioSource.Play();
                }
            }
        }
        else if (phaseState == STATE.ending)
        {
            myAudioSource.Stop();
            myAudioSource.clip = sounds[1];
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

