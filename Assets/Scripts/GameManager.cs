using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject[] phases;
    public GameObject menu;
    public float buttonDelay = 3;

    public GameObject btnNextPhase;
    public GameObject btnJumpPhase;

    private int currentPhase;
    private float buttonTimer;
    private bool buttonClicked;

    void Start () {
        currentPhase = 0;
        buttonTimer = 0;
        buttonClicked = false;
        menu.SetActive(true);

        deactivateBtn();
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
        Debug.Log("Phase " + currentPhase + " Ended");

        currentPhase++;

        if (currentPhase < phases.Length)
        {
            phases[currentPhase].SetActive(true);
            Debug.Log("Phase " + currentPhase + " Start");
        }
        else
        {
            currentPhase = phases.Length - 1;
            Debug.Log("Game Ended!");
        }
        
    }
    
    public void jumpPhase()
    {
        phases[currentPhase].SetActive(false);
        Debug.Log("Phase " + currentPhase + " Ended");

        currentPhase += 2;

        if (currentPhase < phases.Length)
        {
            phases[currentPhase].SetActive(true);
            Debug.Log("Phase " + currentPhase + " Start");
        }
        else
        {
            currentPhase = phases.Length - 1;
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

    public void activeBtn()
    {
        btnNextPhase.SetActive(true);
        btnJumpPhase.SetActive(true);
    }

    public void deactivateBtn()
    {
        btnNextPhase.SetActive(false);
        btnJumpPhase.SetActive(false);
    }

    public void setPhaseTrue() {
        phases[currentPhase].SetActive(true);
        menu.SetActive(false);
    }

}
