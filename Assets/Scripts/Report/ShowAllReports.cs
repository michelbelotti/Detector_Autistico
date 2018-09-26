using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShowAllReports : MonoBehaviour {

    public GameObject menu;

    public GameObject reportList;

    public GameObject reportSinglePainel;
    private Report_Single_Creation reportSingleCriation;

    // Use this for initialization
    void OnEnable () {

        reportList.SetActive(true);
        reportSingleCriation = reportSinglePainel.GetComponent<Report_Single_Creation>();
        reportSinglePainel.SetActive(false);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update () {


    }

    public void reportClicked(string path)
    {
        reportList.SetActive(false);
        reportSinglePainel.SetActive(true);
        reportSingleCriation.loadSave(path);
    }

    public void backToManu()
    {
        menu.SetActive(true);
        gameObject.SetActive(false);   
    }

}
