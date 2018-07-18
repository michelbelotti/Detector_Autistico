using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Phase0Manager : MonoBehaviour {

    public float loopInterval = 3;
    public GameObject prefabCharacter;
    public AudioClip[] sounds;
    
    [HideInInspector]public int endPhase;

    private AudioSource myAudioSource;
    private GameObject objCharacter;
    private GameObject objGameManager;
    private GameManager scriptGameManager;
    private float timeCount;

    void Start () {

        objGameManager = GameObject.Find("GameManager");
        scriptGameManager = objGameManager.GetComponent<GameManager>();

        objCharacter = Instantiate(prefabCharacter, transform.position, transform.rotation);

        endPhase = 0;
        timeCount = 0;

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = sounds[0];
        myAudioSource.Play();

    }
	
	void Update () {

        if (endPhase == 0)
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
        else if (endPhase == 1)
        {
            myAudioSource.Stop();
            myAudioSource.clip = sounds[1];
            myAudioSource.Play();
            changeState();
        }
        else if(endPhase == 2)
        {
            if (!myAudioSource.isPlaying)
            {
                Destroy(objCharacter);
                changeState();
                scriptGameManager.nextPhase();
            }
        }
        
    }

    public void changeState()
    {
        endPhase++;
    }

}

