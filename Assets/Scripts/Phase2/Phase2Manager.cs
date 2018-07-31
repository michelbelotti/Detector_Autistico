using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Manager : MonoBehaviour {

    public GameObject prefabCharacter;
    public float delayToStart = 2;
    public float idleTime = 5;
    public float phaseTime = 30;

    public AudioClip[] sounds;

    private AudioSource myAudioSource;
    private GameObject objGameManager;
    private GameManager scriptGameManager;
    private GameObject character;
    private float totalTimeCount;
    private float idleTimeCount;

    private STATE phaseState;
    private enum STATE
    {
        firstCmd,
        firstInput,
        touchCounter,
        endPhase,
    }

    private List<float> touchLatency;

    IEnumerator Start()
    {
        

        objGameManager = GameObject.Find("GameManager");
        scriptGameManager = objGameManager.GetComponent<GameManager>();

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = sounds[0];

        character = Instantiate(prefabCharacter, transform.position, transform.rotation);

        touchLatency = new List<float>();

        phaseState = STATE.firstCmd;

        totalTimeCount = 0;
        idleTimeCount = 0;


        yield return new WaitForSeconds(delayToStart);

        myAudioSource.Play();

        yield return new WaitForSeconds(myAudioSource.clip.length);

        myAudioSource.clip = sounds[1];
        phaseState = STATE.firstInput;

    }

    void Update ()
    {
        if (phaseState == STATE.touchCounter)
        {
            totalTimeCount += Time.deltaTime;
            idleTimeCount += Time.deltaTime;

            if ((totalTimeCount >= phaseTime) || (idleTimeCount >= idleTime))
            {
                phaseState = STATE.endPhase;
            }
        }
        else if (phaseState == STATE.endPhase)
        {
            if (!myAudioSource.isPlaying)
            {
                foreach(float item in touchLatency)
                {
                    Debug.Log("touchLatency[" + touchLatency.IndexOf(item) + "] = " + item);
                }

                Destroy(character);
                scriptGameManager.nextPhase();
            }
        }

        
    }

    public void CharacterClicked()
    {
        if (phaseState == STATE.firstInput)
        {
            phaseState = STATE.touchCounter;
                      
            myAudioSource.Stop();
            myAudioSource.Play();
        }
        else if (phaseState == STATE.touchCounter)
        {
            touchLatency.Add(idleTimeCount);
            
            idleTimeCount = 0;

            myAudioSource.Stop();
            myAudioSource.Play();
        }
    }
}
