using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShowAllReports : MonoBehaviour {

    //referencia para instanciar objs
    public GameObject painelReport;
    public GameObject reportPrefab;

    //caminho para pasta
    private string filePath;

    private GameObject reportInstance;

    //vetor com todos os reports
    private Report_Save[] reportDatas;
    

    // Use this for initialization
    void Start () {
        
        // cria vetor com todos os reports em reportDatas[];
        filePath = Application.persistentDataPath;
        int i = 0;
        reportDatas = new Report_Save[System.IO.Directory.GetFiles(filePath).Length];
        foreach (string fileName in System.IO.Directory.GetFiles(filePath))
        {
            string dataReport = File.ReadAllText(fileName);
            reportDatas[i++] = JsonUtility.FromJson<Report_Save>(dataReport);
        }

        reportInstance = Instantiate(reportPrefab, painelReport.transform);
        

    }

    void OnEnable() {
        
    }
	
	// Update is called once per frame
	void Update () {


    }
    
    public void ShowReports()
    {
        /*
        fileName = Application.persistentDataPath;
        foreach (string file in System.IO.Directory.GetFiles(fileName))
        {
            print("!!!!AQUI: " + file);
        }
        */
    }
    

}
