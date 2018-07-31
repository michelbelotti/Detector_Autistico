using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase0CharacterClicked : MonoBehaviour {

    private GameObject objManager;
    private Phase0Manager phase0Msg;

    private bool active;

    private void Start()
    {
        active = true;
        objManager = GameObject.Find("Phase_0_Manager");
        phase0Msg = objManager.GetComponent<Phase0Manager>();
    }

    public void clickedEvent()
    {
        if(active)
        {
            phase0Msg.changeState();
            active = false;
        }
    }
}
