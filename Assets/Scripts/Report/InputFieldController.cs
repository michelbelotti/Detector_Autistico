﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour {

    public InputField name;

    public GameObject reportGO;
    private Report_Manage reportScript;
    private GameManager gm;

    private void OnEnable()
    {
        reportScript = reportGO.GetComponent<Report_Manage>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void nextPhase()
    {
        reportScript.childName = name.text;
        gm.nextPhase();

        Debug.Log("OnDisable InputField Name Painel");
    }
}