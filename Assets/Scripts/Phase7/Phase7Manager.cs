using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase7Manager : MonoBehaviour {

    public float delayToStart = 1;
    public float totalTimer = 60;
    
    public GameObject prefabCharacter;
    
    public AudioClip soundInstruction;

    [HideInInspector]
    public bool firstInput;

    private float totalTimeCount;

    private GameManager scriptGameManager;

    private GameObject objCharacter;

    private AudioSource myAudioSource;

    private STATE phaseState;
    private enum STATE
    {
        instruction,
        isPlaying,
    }

    IEnumerator Start () {

        firstInput = false;

        totalTimeCount = 0;

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        objCharacter = Instantiate(prefabCharacter, prefabCharacter.transform.position, prefabCharacter.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;

        phaseState = STATE.instruction;

        yield return new WaitForSeconds(delayToStart);

        myAudioSource.Play();

        yield return new WaitForSeconds(myAudioSource.clip.length);

        phaseState = STATE.isPlaying;
    }

    void Update()
    {

        if (phaseState == STATE.isPlaying)
        {
            if (firstInput)
            {
                Debug.Log("First Input");

                totalTimeCount += Time.deltaTime;

                if(totalTimeCount >= totalTimer)
                {
                    scriptGameManager.nextPhase();
                }
            }
        }
    }
    
     void OnDisable()
    {
        Debug.Log("OnDisable Phase 7");

        myAudioSource.Stop();

        Destroy(objCharacter);
    }
}
