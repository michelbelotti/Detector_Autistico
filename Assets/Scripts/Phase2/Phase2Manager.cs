using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Manager : MonoBehaviour
{

    public float delayToStart = 1;
    public float idleTime = 5;
    public float phaseTime = 30;

    public AudioClip soundInstruction;
    public AudioClip soundToRepeat;

    public GameObject prefabCharacter;

    private GameManager scriptGameManager;
    private Report_Manager scriptReportManager;

    private AudioSource myAudioSource;
    private GameObject objCharacter;
    private float totalTimeCount;
    private float idleTimeCount;
    private float timeCount;

    private STATE phaseState;
    private enum STATE
    {
        enabling,
        instruction,
        firstInput,
        touchCounter,
        endPhase,
    }

    private List<float> touchLatency;

    private void OnEnable()
    {
        phaseState = STATE.enabling;

        totalTimeCount = 0;
        idleTimeCount = 0;
        timeCount = 0;

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scriptReportManager = GameObject.Find("ReportManager").GetComponent<Report_Manager>();


        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;

        objCharacter = Instantiate(prefabCharacter, transform.position, transform.rotation);

        touchLatency = new List<float>();
    }

    void Update()
    {

        if (phaseState == STATE.enabling)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= delayToStart)
            {
                myAudioSource.Play();
                phaseState = STATE.instruction;
            }
        }
        else if (phaseState == STATE.instruction)
        {
            if (!myAudioSource.isPlaying)
            {
                myAudioSource.clip = soundToRepeat;
                phaseState = STATE.firstInput;
            }
        }
        else if (phaseState == STATE.touchCounter)
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

            idleTimeCount = 0;
        }
        else if (phaseState == STATE.touchCounter)
        {
            touchLatency.Add(idleTimeCount);

            idleTimeCount = 0;

            myAudioSource.Stop();
            myAudioSource.Play();
        }
    }

    private void SendReport()
    {
        scriptReportManager.phase2Total = touchLatency.Count;

        float avg = 0;
        foreach (float t in touchLatency)
        {
            avg += t;
            scriptReportManager.phase2latency.Add(t);
        }

        scriptReportManager.phase2average = avg / touchLatency.Count;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 2");

        SendReport();

        myAudioSource.Stop();
        Destroy(objCharacter);
    }
}
