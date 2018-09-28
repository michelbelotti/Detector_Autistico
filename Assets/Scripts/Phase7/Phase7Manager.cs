using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase7Manager : MonoBehaviour
{

    public float delayToStart = 1;
    public float totalTimer = 60;

    public GameObject prefabCharacter;

    public AudioClip soundInstruction;

    [HideInInspector]
    public bool firstInput;

    private GameManager scriptGameManager;
    private Report_Manager scriptReportManager;

    private List<float> touchLatency;

    private float totalTimeCount;
    private float lastClickTime;

    private GameObject objCharacter;

    private AudioSource myAudioSource;

    private STATE phaseState;
    private enum STATE
    {
        instruction,
        isPlaying,
    }

    IEnumerator Start()
    {

        firstInput = false;

        totalTimeCount = 0;

        touchLatency = new List<float>();
        lastClickTime = 0f;

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scriptReportManager = GameObject.Find("ReportManager").GetComponent<Report_Manager>();

        objCharacter = Instantiate(prefabCharacter, prefabCharacter.transform.position, prefabCharacter.transform.rotation);

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.clip = soundInstruction;

        phaseState = STATE.instruction;

        yield return new WaitForSeconds(delayToStart);

        myAudioSource.Play();

        yield return new WaitForSeconds(myAudioSource.clip.length);

        phaseState = STATE.isPlaying;
    }

    void Update()
    {

        totalTimeCount += Time.deltaTime;

        if (phaseState == STATE.isPlaying)
        {
            if (firstInput)
            {
                Debug.Log("First Input");

                totalTimeCount += Time.deltaTime;

                if (totalTimeCount >= totalTimer)
                {
                    scriptGameManager.nextPhase();
                }
            }
        }
    }

    public void addValueOnList()
    {
        touchLatency.Add(totalTimeCount - lastClickTime);
        lastClickTime = totalTimeCount;
    }

    private void SendReport()
    {
        scriptReportManager.phase7Total = touchLatency.Count;

        float avg = 0;
        foreach (float t in touchLatency)
        {
            avg += t;
            scriptReportManager.phase7latency.Add(t);
        }
        scriptReportManager.phase7average = avg / touchLatency.Count;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase 7");

        SendReport();

        myAudioSource.Stop();

        Destroy(objCharacter);
    }
}
