using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShowAllReports : MonoBehaviour {

    public GameObject menu;

    public GameObject reportList;

    public GameObject buttonBackToList;
    public GameObject buttonDeleteReport;

    public GameObject reportSinglePainel;
    private Report_Single_Creation reportSingleCriation;

    private string reportPath;

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
        reportPath = path;
        reportList.SetActive(false);
        reportSinglePainel.SetActive(true);
        buttonBackToList.SetActive(true);
        buttonDeleteReport.SetActive(true);
        reportSingleCriation.loadSave(reportPath);
    }

    public void backToManu()
    {
        buttonBackToList.SetActive(false);
        buttonDeleteReport.SetActive(false);

        reportSinglePainel.SetActive(false);
        reportList.SetActive(false);
        gameObject.SetActive(false);

        menu.SetActive(true);
    }

    public void deleteReport()
    {
        buttonBackToList.SetActive(false);
        buttonDeleteReport.SetActive(false);

        reportSinglePainel.SetActive(false);
        reportList.SetActive(false);
        gameObject.SetActive(false);

        menu.SetActive(true);

        File.Delete(reportPath);
    }

    public void backToList()
    {
        reportSinglePainel.SetActive(false);
        reportList.SetActive(true);
    }

}
