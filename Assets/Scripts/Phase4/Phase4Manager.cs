using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase4Manager : MonoBehaviour
{

    public float delayToStart = 1;
    public float idleMaxTimer = 20;

    public int maxObjToWin = 8;

    public GameObject prefabPanel;
    public GameObject[] prefabCharacters;

    public AudioClip soundInstruction;
    public AudioClip soundEnding;
    public AudioClip[] soundCongrats;

    private GameManager scriptGameManager;
    private Report_Manager scriptReportManager;

    private AudioSource myAudioSource;
    private GameObject objPanel;
    private GameObject[] objCharacters;
    private ContactFilter2D filter;
    private Collider2D[] colliders;

    private float timerCount;
    private float idleTimerCount;

    private int overlapCount;

    private STATE phaseState;
    private enum STATE
    {
        enabling,
        instruction,
        isPlaying,
        congrats,
        ending,
    }

    private void OnEnable()
    {
        phaseState = STATE.enabling;

        timerCount = 0;
        overlapCount = 0;
        idleTimerCount = 0;

        filter = new ContactFilter2D();
        colliders = new Collider2D[prefabCharacters.Length];

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scriptReportManager = GameObject.Find("ReportManager").GetComponent<Report_Manager>();

        objCharacters = new GameObject[prefabCharacters.Length];

        for (int i = 0; i < prefabCharacters.Length; i++)
        {
            objCharacters[i] = Instantiate(prefabCharacters[i], prefabCharacters[i].transform.position, prefabCharacters[i].transform.rotation);
        }

        objPanel = Instantiate(prefabPanel, prefabPanel.transform.position, prefabPanel.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;
    }

    void Update()
    {

        timerCount += Time.deltaTime;
        idleTimerCount += Time.deltaTime;

        if (idleTimerCount >= idleMaxTimer)
        {
            phaseState = STATE.ending;
        }

        if (phaseState == STATE.enabling)
        {
            if (timerCount >= delayToStart)
            {
                myAudioSource.Play();
                phaseState = STATE.instruction;
            }
        }
        else if(phaseState == STATE.instruction)
        {
            if (!myAudioSource.isPlaying)
            {
                phaseState = STATE.isPlaying;
            }
        }
        else if (phaseState == STATE.congrats)
        {
            myAudioSource.Stop();
            myAudioSource.clip = soundEnding;
            myAudioSource.Play();

            phaseState = STATE.ending;
        }
        else if (phaseState == STATE.ending)
        {
            if (!myAudioSource.isPlaying)
            {
                scriptGameManager.nextPhase();
            }
        }
    }

    public void CheckObject()
    {
        if (phaseState == STATE.instruction || phaseState == STATE.enabling)
        {
            phaseState = STATE.isPlaying;
        }

        if (phaseState == STATE.isPlaying)
        {
            int currentOverlap = Physics2D.OverlapCollider(objPanel.GetComponent<Collider2D>(), filter, colliders);

            if (overlapCount != currentOverlap)
            {
                if (currentOverlap > overlapCount)
                {

                    if (currentOverlap >= maxObjToWin)
                    {
                        phaseState = STATE.congrats;
                    }
                    else
                    {
                        RandomSound();
                    }
                }
                overlapCount = currentOverlap;
            }
        }
    }

    private void RandomSound()
    {
        myAudioSource.Stop();
        myAudioSource.clip = soundCongrats[Random.Range(0, soundCongrats.Length)];
        myAudioSource.Play();
    }

    public void resetIdleTimer()
    {
        idleTimerCount = 0;
    }

    private void SendReport()
    {
        foreach (GameObject go in objCharacters)
        {
            scriptReportManager.phase4PosObjs.Add(go.transform.position);
        }
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 4");

        SendReport();

        myAudioSource.Stop();

        for (int i = 0; i < objCharacters.Length; i++)
        {
            Destroy(objCharacters[i]);
        }

        Destroy(objPanel);
    }

}
