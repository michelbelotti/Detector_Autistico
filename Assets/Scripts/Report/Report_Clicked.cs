using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Report_Clicked : MonoBehaviour {

    private Report_Save data;

    [HideInInspector]
    public string pathName;

    private ShowAllReports showReport;

    // Use this for initialization
    void Start () {
        showReport = GameObject.Find("ShowReport").GetComponent<ShowAllReports>() ;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReportClicked()
    {
        showReport.reportClicked(pathName);
    }

}
