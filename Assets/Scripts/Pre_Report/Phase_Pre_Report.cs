using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_Pre_Report : MonoBehaviour {

    public GameObject report;
    
    private GameManager scriptGameManager;
    private Report_Manage scriptReportManager;

    // Use this for initialization
    void Start () {
        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scriptReportManager = report.GetComponent<Report_Manage>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void buttonClicked() {
        //report.SetActive(true);
        //scriptGameManager.nextPhase();

        scriptReportManager.CloseReport();
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase Pre Report");
    }

}
