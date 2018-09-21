using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Report_Manager : MonoBehaviour {

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
    
    public void CloseReport()
    {
        
        Report_Save data = new Report_Save();

        //Fase 2 data
        data.SetPhase2Data(phase2latency.Count);
        data.phase2Total = phase2Total;
        data.phase2average = phase2average;

        int i = 0;
        foreach(float t in phase2latency)
        {
            data.phase2latency[i++] = t;
        }

        //fase 4 data
        data.SetPhase4Data(phase4PosObjs.Count);

        i = 0;
        foreach (Vector3 v in phase4PosObjs)
        {
            data.phase4Positions[i, 0] = v.x;
            data.phase4Positions[i, 1] = v.y;
            data.phase4Positions[i, 2] = v.z;
            i++;
        }

        //fase 6
        data.phase6Tries = phase6TotalTries;

        //fase 7
        data.SetPhase7Data(phase7latency.Count);
        data.phase7Total = phase7Total;
        data.phase7average = phase7average;

        i = 0;
        foreach (float t in phase7latency)
        {
            data.phase7latency[i++] = t;
        }
        
        string json = JsonUtility.ToJson(data);
        string fileName = Application.persistentDataPath + "/" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".json";

        Debug.Log(fileName);
        File.WriteAllText(fileName, json);

    }
}
