using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fase0_manager : MonoBehaviour {

    public float loopInterval = 3;
    public bool debug = false;
    public Text debugText;

    private float timeCount;

    AudioSource myAudioSource;

    void Start () {
        timeCount = 0;
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.Play();

        if (!debug)
        {
            debugText.enabled = false;
        }
        else
        {
            debugText.enabled = true;
        }
    }
	
	void Update () {
        if(!myAudioSource.isPlaying)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= loopInterval)
            {
                timeCount = 0;
            
                myAudioSource.Stop();
                myAudioSource.Play();
            }
        }

        debugUpdate();
    }

    void debugUpdate()
    {
        debugText.text = "Timer = " + timeCount.ToString() + "\n";
        //debugText.text += ;
    }    
}

