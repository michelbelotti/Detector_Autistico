using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Report_List_Creation : MonoBehaviour {

    //referencia para instanciar objs
    public GameObject reportPainelList;
    public GameObject reportPrefab;

    private string nameFile;
    
    //caminho para pasta
    private string filePath;

    private GameObject reportInstance;
    private List<GameObject> reportListInstantiated = new List<GameObject>();

    private void OnEnable()
    {

        // cria vetor com todos os reports em reportDatas[];
        filePath = Application.persistentDataPath;
        int i = 0;
        foreach (string fileName in System.IO.Directory.GetFiles(filePath))
        {
            nameFile = fileName.Replace(filePath, "").Replace("\\", "").Replace(".json", "");
            reportInstance = Instantiate(reportPrefab, reportPainelList.transform);
            reportInstance.GetComponentInChildren<Text>().text = nameFile;
            reportInstance.GetComponent<Report_Clicked>().pathName = fileName;
            reportListInstantiated.Add(reportInstance);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        foreach (GameObject go in reportListInstantiated)
        {
            Destroy(go);
        }
    }
}
