using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Report_Clicked : MonoBehaviour {

    private Report_Save data;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Report_Save ReportClicked()
    {
        return data;
    }

    public void loadReport(Report_Save dt)
    {
        data = new Report_Save(dt);
    }
}
