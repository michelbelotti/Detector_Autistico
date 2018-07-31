using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] phases;
    public float buttonDelay = 3;

    private int currentPhase;
    private float buttonTimer;
    private bool buttonClicked;

    void Start () {
        currentPhase = 0;
        buttonTimer = 0;
        buttonClicked = false;
        phases[currentPhase].SetActive(true);
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
}
