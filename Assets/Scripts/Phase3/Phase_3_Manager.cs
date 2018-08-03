using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_3_Manager : MonoBehaviour {

    public GameObject[] prefabCharacter;
    public float delayToStart = 2;
    public float idleTime = 5;
    public float phaseTime = 30;

    public int overlapMax = 2;
    
    [HideInInspector]
    public int tries;
    public int totalTries = 5;

    public AudioClip[] sounds;

    private AudioSource myAudioSource;
    private GameManager scriptGameManager;
    private GameObject[] objCharacter;
    private Vector3[] objPostions;
    private float totalTimeCount;

    ContactFilter2D filter;
    Collider2D[] colliders;

    private int overlapCount;

    private STATE phaseState;
    private enum STATE
    {
        firstCmd,
        isPlaying,
    }

    // Use this for initialization
    void Start () {

        tries = 0;

        filter = new ContactFilter2D();
        colliders = new Collider2D[prefabCharacter.Length];

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = sounds[0];

        objCharacter = new GameObject[prefabCharacter.Length];
        objPostions = new Vector3[prefabCharacter.Length];

        for (int i = 0; i < prefabCharacter.Length; i++)
        {
            objCharacter[i] = Instantiate(prefabCharacter[i], prefabCharacter[i].transform.position, prefabCharacter[i].transform.rotation);
            objPostions[i] = prefabCharacter[i].transform.position;
        }

        overlapCount = 0;

    }
	
	// Update is called once per frame
	void Update () {

        totalTimeCount += Time.deltaTime;
        if(phaseState == STATE.firstCmd)
        {
            if (totalTimeCount > delayToStart)
            {
                myAudioSource.Play();
                phaseState = STATE.isPlaying;
            }
        }
        

        int currentOverlap = Physics2D.OverlapCollider(objCharacter[2].GetComponent<Collider2D>(), filter, colliders);

        if (tries > totalTries)
        {
            scriptGameManager.activeBtn();
        }

        if (overlapCount != currentOverlap) {            
            if (currentOverlap > overlapCount)
            {
                myAudioSource.Stop();
                myAudioSource.clip = sounds[1];
                myAudioSource.Play();
            }
            overlapCount = currentOverlap;
        }
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 3");
        //myAudioSource.Stop();
        for (int i = 0; i < prefabCharacter.Length; i++)
        {
            Destroy(objCharacter[i]);
        }
        scriptGameManager.deactivateBtn();
    }

}
