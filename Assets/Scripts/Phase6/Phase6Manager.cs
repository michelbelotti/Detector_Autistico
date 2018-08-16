using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase6Manager : MonoBehaviour {

    public float delayToStart = 1;

    [HideInInspector]
    public int tries;
    public int totalTries = 10;

    public GameObject prefabDragable;
    public GameObject prefabTargetLeft;
    public GameObject prefabTargetRight;
    
    public AudioClip soundCongrats;

    private float totalTimeCount;

    private GameManager scriptGameManager;

    private GameObject objDragable;
    private GameObject objTargetLeft;
    private GameObject objTargetRight;

    private AudioSource myAudioSource;

    private ContactFilter2D filter;
    private Collider2D[] colliders;

    private STATE phaseState;
    private enum STATE
    {
        isPlaying,
        resetingPos,
        endingPhase,
    }

    void Start () {

        tries = 0;
        totalTimeCount = 0;

        filter = new ContactFilter2D();
        colliders = new Collider2D[2];

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        objDragable = Instantiate(prefabDragable, prefabDragable.transform.position, prefabDragable.transform.rotation);
        objTargetLeft = Instantiate(prefabTargetLeft, prefabTargetLeft.transform.position, prefabTargetLeft.transform.rotation);
        objTargetRight = Instantiate(prefabTargetRight, prefabTargetRight.transform.position, prefabTargetRight.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundCongrats;

        phaseState = STATE.isPlaying;
    }

    void Update()
    {
        totalTimeCount += Time.deltaTime;

        if (tries >= totalTries)
        {
            phaseState = STATE.endingPhase;
        }


        if (phaseState == STATE.isPlaying)
        {

        }
        else if (phaseState == STATE.resetingPos)
        {
            objDragable.transform.position = prefabDragable.transform.position;
            phaseState = STATE.isPlaying;
        }
        else if (phaseState == STATE.endingPhase)
        {
            if(!myAudioSource.isPlaying)
            {
                scriptGameManager.nextPhase();
            }
        }
    }
    
    public void ObjectRelease()
    {
        tries++;

        int overlapLeft = Physics2D.OverlapCollider(objTargetLeft.GetComponent<Collider2D>(), filter, colliders);
        int overlapRight = Physics2D.OverlapCollider(objTargetRight.GetComponent<Collider2D>(), filter, colliders);

        if(overlapLeft >= 2)
        {
            phaseState = STATE.resetingPos;
        }

        if (overlapRight >= 2)
        {
            myAudioSource.Play();
            phaseState = STATE.endingPhase;
        }

    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 6");

        myAudioSource.Stop();

        Destroy(objDragable);
        Destroy(objTargetLeft);
        Destroy(objTargetRight);
    }
}
