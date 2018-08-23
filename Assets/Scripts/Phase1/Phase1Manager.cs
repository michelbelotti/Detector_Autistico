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
        instruction,
        repeat,
    }

    IEnumerator Start() {
        phaseState = STATE.instruction;

        objCharacter = Instantiate(prefabCharacter, transform.position, transform.rotation);

        timeCount = 0;

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;

        yield return new WaitForSeconds(delayToStart);

        myAudioSource.Play();

        yield return new WaitForSeconds(myAudioSource.clip.length);

        myAudioSource.clip = soundToRepeat;

        phaseState = STATE.repeat;

    }
	
	void Update () {

        timeCount += Time.deltaTime;

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
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 1");
        myAudioSource.Stop();
        Destroy(objCharacter);
    }
}
