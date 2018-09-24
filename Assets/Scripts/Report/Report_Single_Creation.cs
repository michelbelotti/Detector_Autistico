using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Report_Single_Creation : MonoBehaviour {

    private Report_Save rs;

    //Fase 2
    public GameObject phase2Painel;
    public Text totalPhase2;
    public Text averagePhase2;
    public Text latencyPhase2;

    private void OnEnable()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadSave(string path)
    {
        string dataReport = File.ReadAllText(path);
        rs = JsonUtility.FromJson<Report_Save>(dataReport);
        totalPhase2.text = "Total: " + rs.phase2Total;
        averagePhase2.text = "Média: " + string.Format("{0:#0.00}", rs.phase2average);
        latencyPhase2.text = "Latência dos clicks: ";
        foreach(float f in rs.phase2latency)
        {
            latencyPhase2.text += "" + string.Format("{0:#0.00}", f) + "; ";
        }

        phase2Painel.SetActive(true);
    }
}
