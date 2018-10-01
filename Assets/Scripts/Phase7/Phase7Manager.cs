using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase7Manager : MonoBehaviour
{

    public float delayToStart = 1;
    public float totalTimer = 60;

    public GameObject prefabCharacter;

    public AudioClip soundInstruction;

    [HideInInspector]
    public bool firstInput;

    private GameManager scriptGameManager;
    private Report_Manager scriptReportManager;
        
    private GameObject objCharacter;

    private AudioSource myAudioSource;

    private List<float> touchLatency;

    private float totalTimeCount;
    private float lastClickTime;
    private float timeCount;

    private STATE phaseState;
    private enum STATE
    {
        enabling,
        instruction,
        isPlaying,
    }

    private void OnEnable()
    {
        phaseState = STATE.enabling;

        firstInput = false;

        timeCount = 0;
        lastClickTime = 0f;
        totalTimeCount = 0;

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scriptReportManager = GameObject.Find("ReportManager").GetComponent<Report_Manager>();

        objCharacter = Instantiate(prefabCharacter, prefabCharacter.transform.position, prefabCharacter.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;

        touchLatency = new List<float>();
    }

    void Update()
    {

        totalTimeCount += Time.deltaTime;

        if (firstInput)
        {
            if (totalTimeCount >= totalTimer)
            {
                scriptGameManager.nextPhase();
            }
        }

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
                phaseState = STATE.isPlaying;
            }
        }
    }

    public void addValueOnList()
    {
        touchLatency.Add(totalTimeCount - lastClickTime);
        lastClickTime = totalTimeCount;

        firstInput = true;
    }

    private void SendReport()
    {
        scriptReportManager.phase7Total = touchLatency.Count;

        float avg = 0;
        foreach (float t in touchLatency)
        {
            avg += t;
            scriptReportManager.phase7latency.Add(t);
        }
        scriptReportManager.phase7average = avg / touchLatency.Count;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 7");

        SendReport();

        myAudioSource.Stop();

        Destroy(objCharacter);
    }
}
