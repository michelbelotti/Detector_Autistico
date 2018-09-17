using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShowAllReports : MonoBehaviour {

    //referencia para instanciar objs
    public GameObject reportPainel;
    public GameObject reportPrefab;

    //caminho para pasta
    private string filePath;

    private GameObject reportInstance;

    private List<string> nameFiles = new List<string>();

    //vetor com todos os reports
    //private Report_Save[] reportDatas;

    // Use this for initialization
    void OnEnable () {
        
        // cria vetor com todos os reports em reportDatas[];
        filePath = Application.persistentDataPath;
        int i = 0;
        foreach (string fileName in System.IO.Directory.GetFiles(filePath))
        {
            nameFiles.Add(fileName.Replace(filePath, "").Replace("\\", "").Replace(".json",""));
            reportInstance = Instantiate(reportPrefab, reportPainel.transform);
            reportInstance.GetComponentInChildren<Text>().text = nameFiles[i++];
            reportInstance.GetComponent<Report_Clicked>().pathName = fileName;
        }

        //string dataReport = File.ReadAllText(fileName);
        //reportDatas[i++] = JsonUtility.FromJson<Report_Save>(dataReport);

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

    public void showReport(string path)
    {
        print("path: " + path);
        //mostrar um report
        //loading do path
        //desabilitar a lista
        //abilitar novo painel para 1 report
    }
    

}
