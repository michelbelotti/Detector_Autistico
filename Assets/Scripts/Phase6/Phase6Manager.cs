using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase6Manager : MonoBehaviour {

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

    private float totalTimeCount;

    private GameManager scriptGameManager;

    private GameObject objDragable;
    private GameObject objTargetLeft;
    private GameObject objTargetRight;
    private GameObject objIndicator;

    private AudioSource myAudioSource;

    private ContactFilter2D filter;
    private Collider2D[] colliders;

    private STATE phaseState;
    private enum STATE
    {
        instruction,
        isPlaying,
        resetingPos,
        endingPhase,
    }

    IEnumerator Start () {

        phaseState = STATE.instruction;

        tries = 0;
        totalTimeCount = 0;

        filter = new ContactFilter2D();
        colliders = new Collider2D[2];

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        objDragable = Instantiate(prefabDragable, prefabDragable.transform.position, prefabDragable.transform.rotation);
        objTargetLeft = Instantiate(prefabTargetLeft, prefabTargetLeft.transform.position, prefabTargetLeft.transform.rotation);
        objTargetRight = Instantiate(prefabTargetRight, prefabTargetRight.transform.position, prefabTargetRight.transform.rotation);
        objIndicator = Instantiate(prefabIndicator, prefabIndicator.transform.position, prefabIndicator.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;

        yield return new WaitForSeconds(delayToStart);

        myAudioSource.Play();

        yield return new WaitForSeconds(myAudioSource.clip.length);

        if (phaseState == STATE.instruction)
        {
            phaseState = STATE.isPlaying;
        }

    }

    void Update()
    {
        totalTimeCount += Time.deltaTime;

        if (tries >= totalTries)
        {
            phaseState = STATE.endingPhase;
        }

        if (phaseState == STATE.resetingPos)
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

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 6");

        myAudioSource.Stop();

        Destroy(objDragable);
        Destroy(objTargetLeft);
        Destroy(objTargetRight);
        Destroy(objIndicator);
    }
}
