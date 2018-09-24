using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Manager : MonoBehaviour {

    public float delayToStart = 1;
    public float loopInterval = 3;
    
    public AudioClip soundInstruction;
    public AudioClip soundToRepeat;

    public GameObject prefabCharacter;

    private AudioSource myAudioSource;
    private GameObject objCharacter;

    private float timeCount;

    private STATE phaseState;
    private enum STATE
    {
        enabling,
        instruction,
        repeat,
    }

    private void OnEnable()
    {
        phaseState = STATE.enabling;

        timeCount = 0;

        objCharacter = Instantiate(prefabCharacter, transform.position, transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;
    }
	
	void Update () {

        if(phaseState == STATE.enabling)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= delayToStart)
            {
                myAudioSource.Play();
                phaseState = STATE.instruction;
            }
        }
        else if(phaseState == STATE.instruction)
        {
            if (!myAudioSource.isPlaying)
            {
                phaseState = STATE.repeat;
                myAudioSource.clip = soundToRepeat;
                timeCount = 0;
            }
        }
        else if (phaseState == STATE.repeat)
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
    }

    void OnDisable()
    {
        myAudioSource.Stop();
        Destroy(objCharacter);
    }
}
