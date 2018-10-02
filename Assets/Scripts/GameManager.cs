using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject[] phases;
    public GameObject menu;
    public float buttonDelay = 3;

    private int currentPhase;
    private float buttonTimer;
    private bool buttonClicked;

    private Report_Manager scriptReportManager;

    void Start () {

        scriptReportManager = GameObject.Find("ReportManager").GetComponent<Report_Manager>();

        currentPhase = 0;
        buttonTimer = 0;
        buttonClicked = false;
        menu.SetActive(true);
    }
	
	void Update () {
        if (buttonClicked)
        {
            buttonTimer += Time.deltaTime;
        }
	}

    public void nextPhase()
    {
        phases[currentPhase].SetActive(false);
        Debug.Log("Phase " + phases[currentPhase].name + " Ended");

        currentPhase++;

        if (currentPhase < phases.Length)
        {
            phases[currentPhase].SetActive(true);
            Debug.Log("Phase " + phases[currentPhase].name + " Started");
        }
        else
        {
            currentPhase = phases.Length - 1;
            scriptReportManager.PreReport();
            Debug.Log("Game Ended!");
        }
        
    }
    
    public void ButtonDown()
    {
        buttonTimer = 0;
        buttonClicked = true;
    }

    public void ButtonUp()
    {
        buttonClicked = false;
        if (buttonTimer >= buttonDelay)
        {
            nextPhase();
        }
    }
    
    public void setPhaseTrue() {
        phases[currentPhase].SetActive(true);
        menu.SetActive(false);
    }

    public void finishGame()
    {
        currentPhase = 0;
        menu.SetActive(true);
    }

}
