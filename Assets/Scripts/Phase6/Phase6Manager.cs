using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase6Manager : MonoBehaviour
{

    public float delayToStart = 1;

    [HideInInspector]
    public int tries;
    public int totalTries = 10;

    public AudioClip soundInstruction;
    public AudioClip soundCongrats;

    public GameObject prefabDragable;
    public GameObject prefabTargetLeft;
    public GameObject prefabTargetRight;
    public GameObject prefabIndicator;

    private GameManager scriptGameManager;
    private Report_Manager scriptReportManager;

    private GameObject objDragable;
    private GameObject objTargetLeft;
    private GameObject objTargetRight;
    private GameObject objIndicator;

    private AudioSource myAudioSource;

    private ContactFilter2D filter;
    private Collider2D[] colliders;

    private float timeCount;

    private STATE phaseState;
    private enum STATE
    {
        enabling,
        instruction,
        isPlaying,
        resetingPos,
        endingPhase,
    }

    private void OnEnable()
    {
        phaseState = STATE.enabling;

        tries = 0;
        timeCount = 0;

        filter = new ContactFilter2D();
        colliders = new Collider2D[2];

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scriptReportManager = GameObject.Find("ReportManager").GetComponent<Report_Manager>();

        objDragable = Instantiate(prefabDragable, prefabDragable.transform.position, prefabDragable.transform.rotation);
        objTargetLeft = Instantiate(prefabTargetLeft, prefabTargetLeft.transform.position, prefabTargetLeft.transform.rotation);
        objTargetRight = Instantiate(prefabTargetRight, prefabTargetRight.transform.position, prefabTargetRight.transform.rotation);
        objIndicator = Instantiate(prefabIndicator, prefabIndicator.transform.position, prefabIndicator.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;
    }

    void Update()
    {

        if (tries >= totalTries)
        {
            phaseState = STATE.endingPhase;
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
        else if (phaseState == STATE.resetingPos)
        {
            objDragable.transform.position = prefabDragable.transform.position;
            phaseState = STATE.isPlaying;
        }
        else if (phaseState == STATE.endingPhase)
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
            tries++;

            int overlapRight = Physics2D.OverlapCollider(objTargetRight.GetComponent<Collider2D>(), filter, colliders);

            if (overlapRight >= 2)
            {
                myAudioSource.Stop();
                myAudioSource.clip = soundCongrats;
                myAudioSource.Play();
                phaseState = STATE.endingPhase;
            }
            else
            {
                phaseState = STATE.resetingPos;
            }
        }
    }

    private void SendReport()
    {
        scriptReportManager.phase6TotalTries = tries;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 6");

        SendReport();

        myAudioSource.Stop();

        Destroy(objDragable);
        Destroy(objTargetLeft);
        Destroy(objTargetRight);
        Destroy(objIndicator);
    }
}
