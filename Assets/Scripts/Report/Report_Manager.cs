using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Report_Manager : MonoBehaviour {

    public GameObject saveButton;
    public GameObject cancelButton;

    [HideInInspector]
    public string childName;

    //Data Report Phase 2
    [HideInInspector]
    public int phase2Total;
    [HideInInspector]
    public float phase2average;
    [HideInInspector]
    public List<float> phase2latency;

    //Data Report Phase 4
    [HideInInspector]
    public List<Vector3> phase4PosObjs;

    //Data Report Phase 6
    [HideInInspector]
    public int phase6TotalTries;
    
    //Data Report Phase 7
    [HideInInspector]
    public int phase7Total;
    [HideInInspector]
    public float phase7average;
    [HideInInspector]
    public List<float> phase7latency;

    private GameManager scriptGameManager;

    public void Start()
    {

        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        phase2latency = new List<float>();
        phase4PosObjs = new List<Vector3>();
        phase7latency = new List<float>();

        saveButton.SetActive(false);
        cancelButton.SetActive(false);
    }

    public void PreReport()
    {
        saveButton.SetActive(true);
        cancelButton.SetActive(true);
    }

    public void ButtonDown()
    {
        saveButton.SetActive(false);
        cancelButton.SetActive(false);
        SaveReport();
        scriptGameManager.finishGame();
    }

    public void cancelReport()
    {
        saveButton.SetActive(false);
        cancelButton.SetActive(false);
        scriptGameManager.finishGame();
    }

    public void SaveReport()
    {
        double rndNum;

        Report_Save data = new Report_Save();

        data.childName = childName;

        //Fase 2 data
        data.SetPhase2Data(phase2latency.Count);
        data.phase2Total = phase2Total;

        rndNum = System.Math.Round(phase2average, 2);
        data.phase2average = rndNum;

        int i = 0;
        foreach (float t in phase2latency)
        {
            rndNum = System.Math.Round(t, 2);
            data.phase2latency[i++] = rndNum;
        }

        //fase 4 data
        data.SetPhase4Data(phase4PosObjs.Count);

        i = 0;
        foreach (Vector3 v in phase4PosObjs)
        {
            data.phase4PosX[i] = v.x;
            data.phase4PosY[i] = v.y;
            i++;
        }

        //fase 6
        data.phase6Tries = phase6TotalTries;

        //fase 7
        data.SetPhase7Data(phase7latency.Count);
        data.phase7Total = phase7Total;

        rndNum = System.Math.Round(phase7average, 2);
        data.phase7average = rndNum;

        i = 0;
        foreach (float t in phase7latency)
        {
            rndNum = System.Math.Round(t, 2);
            data.phase7latency[i++] = rndNum;
        }
        
        string json = JsonUtility.ToJson(data);
        string fileName = Application.persistentDataPath + "/" + childName + "-" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".json";

        Debug.Log(fileName);
        File.WriteAllText(fileName, json);

        phase2latency.Clear();
        phase4PosObjs.Clear();
        phase7latency.Clear();
    }

}
