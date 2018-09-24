using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_Pre_Report : MonoBehaviour {

    public GameObject report;
    
    private GameManager scriptGameManager;
    private Report_Manager scriptReportManager;

    // Use this for initialization
    void Start () {
        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scriptReportManager = report.GetComponent<Report_Manager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void buttonClicked() {
        //report.SetActive(true);
        //scriptGameManager.nextPhase();

        scriptReportManager.CloseReport();
        scriptGameManager.finishReport();
    }

    void OnDisable()
    {
        Debug.Log("OnDisable Phase Pre Report");
    }

}
