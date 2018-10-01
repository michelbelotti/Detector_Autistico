using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour {

    public InputField name;

    private Report_Manager scriptReportManager;
    private GameManager gm;

    private void OnEnable()
    {
        scriptReportManager = GameObject.Find("ReportManager").GetComponent<Report_Manager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        name.text = "";
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void nextPhase()
    {
        scriptReportManager.childName = name.text;
        gm.nextPhase();

        Debug.Log("OnDisable InputField Name Painel");
    }
}
