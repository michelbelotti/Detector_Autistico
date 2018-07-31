using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Manager : MonoBehaviour {

    public float initDelay = 3;
    public float instructionDelay = 3;
    public float loopInterval = 3;
    public GameObject prefabCharacter;
    public AudioClip[] sounds;
    
    private AudioSource myAudioSource;
    private GameObject objCharacter;
    private GameObject objGameManager;
    private GameManager scriptGameManager;

    private float timeCount;

    private STATE phaseState;
    private enum STATE
    {
        init,
        instruction,
        repeat,
        end,
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Phase 1 Start - Phase1Manager");

        phaseState = STATE.init;

        objGameManager = GameObject.Find("GameManager");
        scriptGameManager = objGameManager.GetComponent<GameManager>();

        objCharacter = Instantiate(prefabCharacter, transform.position, transform.rotation);

        timeCount = 0;

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = sounds[0];
    }
	
	// Update is called once per frame
	void Update () {

        if (!myAudioSource.isPlaying)
        {
            timeCount += Time.deltaTime;

            if (phaseState == STATE.init)
            {
                if (timeCount >= initDelay)
                {
                    timeCount = 0;

                    myAudioSource.Play();
                    phaseState = STATE.instruction;
                }
            }
            else if (phaseState == STATE.instruction)
            {
                if (timeCount >= instructionDelay)
                {
                    timeCount = 0;

                    myAudioSource.clip = sounds[1];
                    myAudioSource.Play();
                    phaseState = STATE.repeat;
                }
            }
            else if (phaseState == STATE.repeat)
            {
                if (timeCount >= loopInterval)
                {
                    timeCount = 0;

                    myAudioSource.Play();
                }
            }
        }
    }
}
