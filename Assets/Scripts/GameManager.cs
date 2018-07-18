using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] phases;

    private int currentPhase;

    // Use this for initialization
    void Start () {
        currentPhase = 0;
        phases[currentPhase].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextPhase()
    {
        phases[currentPhase].SetActive(false);
        phases[++currentPhase].SetActive(true);
    }
}
