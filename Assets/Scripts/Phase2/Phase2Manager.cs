﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Manager : MonoBehaviour {

    public float delayToStart = 1;
    public float idleTime = 5;
    public float phaseTime = 30;

    public AudioClip soundInstruction;
    public AudioClip soundToRepeat;

    public GameObject prefabCharacter;

    // Variaveis Relatório
    public GameObject objReport;
    private Report_Manage rm;

    private AudioSource myAudioSource;
    private GameObject objGameManager;
    private GameManager scriptGameManager;
    private GameObject objCharacter;
    private float totalTimeCount;
    private float idleTimeCount;

    private STATE phaseState;
    private enum STATE
    {
        instruction,
        firstInput,
        touchCounter,
        endPhase,
    }

    private List<float> touchLatency;

    IEnumerator Start()
    {

        phaseState = STATE.instruction;

        objGameManager = GameObject.Find("GameManager");
        scriptGameManager = objGameManager.GetComponent<GameManager>();

        rm = objReport.GetComponent<Report_Manage>();

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;

        objCharacter = Instantiate(prefabCharacter, transform.position, transform.rotation);

        touchLatency = new List<float>();

        totalTimeCount = 0;
        idleTimeCount = 0;

        yield return new WaitForSeconds(delayToStart);

        myAudioSource.Play();

        yield return new WaitForSeconds(myAudioSource.clip.length);

        myAudioSource.clip = soundToRepeat;
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

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 2");
        myAudioSource.Stop();
        Destroy(objCharacter);

        // envia informacoes para relatorio
        rm.phase2Total.text = "Total : " + touchLatency.Count;
        float average = 0;
        rm.phase2latency.text = "";
        foreach (float t in touchLatency)
        {
            average += t;
            rm.phase2latency.text += "" + System.Math.Round(t, 2) + "; ";
        }
        average = average / touchLatency.Count;
        rm.phase2average.text = "Latencia Média: " + System.Math.Round(average, 2);
    }
}
