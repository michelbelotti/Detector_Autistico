﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Report_Manage : MonoBehaviour {

    public Button next;

    private GameManager scriptGameManager;

    public GameObject reportPanel;

    //Fase 4 Prefabs
    public GameObject[] prefabCharacters;
    private GameObject[] objCharacters;
    public GameObject prefabPanel;
    private GameObject objPanel;
    [HideInInspector]
    public List<Vector3> positionsObjs;

    //Panel Report Fase 2
    public Text phase2Total;
    public Text phase2average;
    public Text phase2latency;

    //Panel Report Fase 6
    public Text phase6Total;

    //Panel Report Phase 7
    public Text phase7Total;
    public Text phase7average;
    public Text phase7latency;




    // Use this for initialization
    void Start () {
        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Instanciando objs Fase 4
        objCharacters = new GameObject[prefabCharacters.Length];
        for (int i = 0; i < prefabCharacters.Length; i++)
        {
            objCharacters[i] = Instantiate(prefabCharacters[i], positionsObjs[i], prefabCharacters[i].transform.rotation);
            objCharacters[i].GetComponent<Phase4DragObjects>().enabled = false;
        }
        objPanel = Instantiate(prefabPanel, prefabPanel.transform.position, prefabPanel.transform.rotation);

        next.gameObject.SetActive(true);

        reportPanel.SetActive(false);

    }

    public void nextReport()
    {
        for (int i = 0; i < prefabCharacters.Length; i++)
        {
            objCharacters[i].SetActive(false);
        }
        objPanel.SetActive(false);

        next.gameObject.SetActive(false);

        reportPanel.SetActive(true);


        //mostrando dados das outras fases
    }

    public void backReport() {
        for (int i = 0; i < prefabCharacters.Length; i++)
        {
            objCharacters[i].SetActive(true);
        }
        objPanel.SetActive(true);

        next.gameObject.SetActive(true);

        reportPanel.SetActive(false);

    }

    void OnDisable()
    {
        Debug.Log("OnDisable Report");
        for (int i = 0; i < objCharacters.Length; i++)
        {
            Destroy(objCharacters[i]);
        }

        next.gameObject.SetActive(false);

        reportPanel.SetActive(false);
    }

}
