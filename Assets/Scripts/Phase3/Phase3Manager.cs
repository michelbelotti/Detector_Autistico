using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3Manager : MonoBehaviour {

    public float delayToStart = 1;
    public float idleTime = 5;
    public float phaseTime = 30;

    public GameObject prefabPanel;
    public GameObject[] prefabCharacters;

    public AudioClip soundInstruction;
    public AudioClip[] soundsCongrats;

    private AudioSource myAudioSource;
    private GameObject objPanel;
    private GameObject[] objCharacters;
    private Vector3[] objPositions;
    private float totalTimeCount;

    private ContactFilter2D filter;
    private Collider2D[] colliders;

    private int overlapCount;
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

        overlapCount = 0;
        timeCount = 0;

        filter = new ContactFilter2D();
        colliders = new Collider2D[prefabCharacters.Length];

        objCharacters = new GameObject[prefabCharacters.Length];
        objPositions = new Vector3[prefabCharacters.Length];

        for (int i = 0; i < prefabCharacters.Length; i++)
        {
            objCharacters[i] = Instantiate(prefabCharacters[i], prefabCharacters[i].transform.position, prefabCharacters[i].transform.rotation);
            objPositions[i] = prefabCharacters[i].transform.position;
        }

        objPanel = Instantiate(prefabPanel, prefabPanel.transform.position, prefabPanel.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;
    }

    private void Update () {

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

    public void CheckObject()
    {
        if(phaseState == STATE.instruction || phaseState == STATE.enabling)
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
                    RandomSound();
                }
                overlapCount = currentOverlap;
            }
        }
    }

    private void RandomSound()
    {
        myAudioSource.Stop();
        myAudioSource.clip = soundsCongrats[Random.Range(0, soundsCongrats.Length)];
        myAudioSource.Play();
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable Phase 3");

        myAudioSource.Stop();

        for (int i = 0; i < objCharacters.Length; i++)
        {
            Destroy(objCharacters[i]);
        }

        Destroy(objPanel);
    }

}
