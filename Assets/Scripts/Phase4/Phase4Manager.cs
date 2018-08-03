using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase4Manager : MonoBehaviour {

    public float idleMaxTimer = 20;

    public int overlapMax = 8;
    
    public GameObject prefabPanel;
    public GameObject[] prefabCharacter;

    public AudioClip soundEnding;
    public AudioClip[] sounds;

    private AudioSource myAudioSource;
    private GameManager scriptGameManager;
    private GameObject objPanel;
    private GameObject[] objCharacter;
    private ContactFilter2D filter;
    private Collider2D[] colliders;

    private float timerCount;

    private int overlapCount;

    private STATE phaseState;
    private enum STATE
    {
        isPlaying,
        congrats,
        ending,
    }

    void Start () {

        phaseState = STATE.isPlaying;

        timerCount = 0;
        overlapCount = 0;

        filter = new ContactFilter2D();
        colliders = new Collider2D[prefabCharacter.Length];

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = sounds[0];

        objCharacter = new GameObject[prefabCharacter.Length];

        for (int i = 0; i < prefabCharacter.Length; i++)
        {
            objCharacter[i] = Instantiate(prefabCharacter[i], prefabCharacter[i].transform.position, prefabCharacter[i].transform.rotation);
        }

        objPanel = Instantiate(prefabPanel, prefabPanel.transform.position, prefabPanel.transform.rotation);
    }
	
	void Update () {

        timerCount += Time.deltaTime;

        if (phaseState == STATE.isPlaying)
        {
            int currentOverlap = Physics2D.OverlapCollider(objPanel.GetComponent<Collider2D>(), filter, colliders);

            Debug.Log("currentOverlap = " + currentOverlap);
            if (overlapCount != currentOverlap)
            {
                if (currentOverlap > overlapCount)
                {
                    
                    if (currentOverlap >= overlapMax)
                    {
                        phaseState = STATE.congrats;
                    }
                    else
                    {
                        randomSound();
                    }
                }
                overlapCount = currentOverlap;

                Debug.Log("overlapCount = " + overlapCount);
            }

            if(timerCount >= idleMaxTimer)
            {
                phaseState = STATE.ending;
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

    private void randomSound()
    {
        myAudioSource.Stop();
        myAudioSource.clip = sounds[Random.Range(0, sounds.Length)];
        myAudioSource.Play();
    }

    public void resetIdleTimer()
    {
        timerCount = 0;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 4");
        myAudioSource.Stop();
        for (int i = 0; i < objCharacter.Length; i++)
        {
            Destroy(objCharacter[i]);
        }

        Destroy(objPanel);
    }
}
