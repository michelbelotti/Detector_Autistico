using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    public Text timerText;
    public bool debug = false;

    private float timerCount = 0;

	// Use this for initialization
	void Start () {
		if(!debug)
        {
            timerText.enabled = false;
        }
        else
        {
            timerText.enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        timerCount += Time.deltaTime;
        timerText.text = "Timer = " + timerCount.ToString();
	}
}
