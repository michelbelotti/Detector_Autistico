using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase5Manager : MonoBehaviour
{

    public float delayToStart = 1;

    public AudioClip soundInstruction;
    public AudioClip[] soundsCongrats;

    public GameObject prefabTarget;
    public GameObject prefabDragable;
    public GameObject prefabIndicator;

    private GameObject objTarget;
    private GameObject objDragable;
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
    }

    private void OnEnable()
    {
        phaseState = STATE.enabling;

        timeCount = 0;

        filter = new ContactFilter2D();
        colliders = new Collider2D[2];

        objTarget = Instantiate(prefabTarget, prefabTarget.transform.position, prefabTarget.transform.rotation);
        objDragable = Instantiate(prefabDragable, prefabDragable.transform.position, prefabDragable.transform.rotation);
        objIndicator = Instantiate(prefabIndicator, prefabIndicator.transform.position, prefabIndicator.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;
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
                phaseState = STATE.isPlaying;
            }
        }
        else if (phaseState == STATE.resetingPos)
        {
            if (!myAudioSource.isPlaying)
            {
                objDragable.transform.position = prefabDragable.transform.position;
                phaseState = STATE.isPlaying;
            }
        }
    }

    private void RandomSound()
    {
        myAudioSource.Stop();
        myAudioSource.clip = soundsCongrats[Random.Range(0, soundsCongrats.Length)];
        myAudioSource.Play();
    }

    public void CheckObject()
    {
        if (phaseState == STATE.instruction || phaseState == STATE.enabling)
        {
            phaseState = STATE.isPlaying;
        }

        if (phaseState == STATE.isPlaying)
        {
            int currentOverlap = Physics2D.OverlapCollider(objTarget.GetComponent<Collider2D>(), filter, colliders);

            Debug.Log("currentOverlap " + currentOverlap);

            if (currentOverlap >= 2)
            {
                RandomSound();
            }

            phaseState = STATE.resetingPos;
        }
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 5");

        myAudioSource.Stop();

        Destroy(objTarget);
        Destroy(objDragable);
        Destroy(objIndicator);
    }
}
