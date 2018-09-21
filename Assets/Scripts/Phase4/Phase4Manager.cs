using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase4Manager : MonoBehaviour {

    public float delayToStart = 1;
    public float idleMaxTimer = 20;

    public int maxObjToWin = 8;
    
    public GameObject prefabPanel;
    public GameObject[] prefabCharacters;

    public AudioClip soundInstruction;
    public AudioClip soundEnding;
    public AudioClip[] soundCongrats;

    //Relatório Variaveis
    public GameObject objReport;
    private Report_Manager rm;

    private AudioSource myAudioSource;
    private GameManager scriptGameManager;
    private GameObject objPanel;
    private GameObject[] objCharacters;
    private ContactFilter2D filter;
    private Collider2D[] colliders;

    private float timerCount;

    private int overlapCount;

    private STATE phaseState;
    private enum STATE
    {
        instruction,
        isPlaying,
        congrats,
        ending,
    }

    IEnumerator Start () {

        phaseState = STATE.instruction;

        timerCount = 0;
        overlapCount = 0;

        filter = new ContactFilter2D();
        colliders = new Collider2D[prefabCharacters.Length];

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        rm = objReport.GetComponent<Report_Manager>();

    objCharacters = new GameObject[prefabCharacters.Length];

        for (int i = 0; i < prefabCharacters.Length; i++)
        {
            objCharacters[i] = Instantiate(prefabCharacters[i], prefabCharacters[i].transform.position, prefabCharacters[i].transform.rotation);
        }

        objPanel = Instantiate(prefabPanel, prefabPanel.transform.position, prefabPanel.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;

        yield return new WaitForSeconds(delayToStart);

        myAudioSource.Play();

        yield return new WaitForSeconds(myAudioSource.clip.length);

        phaseState = STATE.isPlaying;
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
                    
                    if (currentOverlap >= maxObjToWin)
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
        myAudioSource.clip = soundCongrats[Random.Range(0, soundCongrats.Length)];
        myAudioSource.Play();
    }

    public void resetIdleTimer()
    {
        timerCount = 0;
    }

    public void finalPositionsObjs()
    {
        foreach (GameObject go in objCharacters)
        {
            rm.phase4PosObjs.Add(go.transform.position);
        }
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 4");

        myAudioSource.Stop();

        //envia posicao dos objs instanciados para relatorio
        finalPositionsObjs();

        for (int i = 0; i < objCharacters.Length; i++)
        {
            Destroy(objCharacters[i]);
        }

        Destroy(objPanel);
    }


}
