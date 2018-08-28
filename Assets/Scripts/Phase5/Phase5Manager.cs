using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase5Manager : MonoBehaviour {

    public float delayToStart = 1;

    public AudioClip soundInstruction;
    public AudioClip[] soundsCongrats;

    public GameObject prefabTarget;
    public GameObject prefabDragable;
    public GameObject prefabIndicator;

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
        instruction,
        isPlaying,
        resetingPos,
    }

    IEnumerator Start () {

        phaseState = STATE.instruction;

        filter = new ContactFilter2D();
        colliders = new Collider2D[2];

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        objTarget = Instantiate(prefabTarget, prefabTarget.transform.position, prefabTarget.transform.rotation);
        objDragable = Instantiate(prefabDragable, prefabDragable.transform.position, prefabDragable.transform.rotation);
        objIndicator = Instantiate(prefabIndicator, prefabIndicator.transform.position, prefabIndicator.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;

        yield return new WaitForSeconds(delayToStart);

        myAudioSource.Play();

        yield return new WaitForSeconds(myAudioSource.clip.length);

        phaseState = STATE.isPlaying;
    }

    void Update()
    {
        if (phaseState == STATE.resetingPos)
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
        myAudioSource.clip = soundsCongrats[Random.Range(0, soundsCongrats.Length)];
        myAudioSource.Play();
    }

    public void ObjectRelease()
    {
        int currentOverlap = Physics2D.OverlapCollider(objTarget.GetComponent<Collider2D>(), filter, colliders);

        Debug.Log("currentOverlap " + currentOverlap);

        if(currentOverlap >= 2)
        {
            randomSound();
            
        }

        phaseState = STATE.resetingPos;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 5");

        myAudioSource.Stop();

        Destroy(objTarget);
        Destroy(objDragable);
        Destroy(objIndicator);

        //scriptGameManager.deactivateBtn();
    }
}
