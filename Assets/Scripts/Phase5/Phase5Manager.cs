using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase5Manager : MonoBehaviour {

    public float delayToStart = 1;

    [HideInInspector]
    public int tries;
    public int totalTries = 5;

    public GameObject prefabTarget;
    public GameObject prefabDragable;
    public GameObject prefabIndicator;

    public AudioClip soundInstruction;
    public AudioClip[] sounds;

    private float totalTimeCount;

    private GameManager scriptGameManager;

    private GameObject objTarget;
    private GameObject objDragable;
    private GameObject objIndicator;

    private AudioSource myAudioSource;

    private ContactFilter2D filter;
    private Collider2D[] colliders;

    private STATE phaseState;
    private enum STATE
    {
        firstCmd,
        isPlaying,
        resetingPos,
    }

    void Start () {

        tries = 0;
        totalTimeCount = 0;

        filter = new ContactFilter2D();
        colliders = new Collider2D[2];

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        objTarget = Instantiate(prefabTarget, prefabTarget.transform.position, prefabTarget.transform.rotation);
        objDragable = Instantiate(prefabDragable, prefabDragable.transform.position, prefabDragable.transform.rotation);
        objIndicator = Instantiate(prefabIndicator, prefabIndicator.transform.position, prefabIndicator.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;
    
    }

    void Update()
    {
        totalTimeCount += Time.deltaTime;

        if (tries >= totalTries)
        {
            scriptGameManager.activeBtn();
        }

        if (phaseState == STATE.firstCmd)
        {
            if (totalTimeCount > delayToStart)
            {
                myAudioSource.Play();
                phaseState = STATE.isPlaying;
            }
        }
        else if (phaseState == STATE.isPlaying)
        {

        }
        else if (phaseState == STATE.resetingPos)
        {
            if(!myAudioSource.isPlaying)
            {
                objDragable.transform.position = prefabDragable.transform.position;
                phaseState = STATE.isPlaying;
            }
        }
    }

    private void randomSound()
    {
        myAudioSource.Stop();
        myAudioSource.clip = sounds[Random.Range(0, sounds.Length)];
        myAudioSource.Play();
    }

    public void ObjectRelease()
    {
        tries++;

        int currentOverlap = Physics2D.OverlapCollider(objTarget.GetComponent<Collider2D>(), filter, colliders);

        Debug.Log("currentOverlap " + currentOverlap);

        if(currentOverlap >= 2)
        {
            randomSound();
            phaseState = STATE.resetingPos;
        }

    }
}
