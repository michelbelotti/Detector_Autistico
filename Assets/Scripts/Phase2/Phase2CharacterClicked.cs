using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2CharacterClicked : MonoBehaviour {

    private GameObject objManager;
    private Phase2Manager phase2Msg;

    // Use this for initialization
    void Start () {
        objManager = GameObject.Find("Phase_2_Manager");
        phase2Msg = objManager.GetComponent<Phase2Manager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clickedEvent()
    {
        phase2Msg.CharacterClicked();
    }
}
