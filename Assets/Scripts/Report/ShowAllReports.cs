using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAllReports : MonoBehaviour {

    public Text repotsNames;

    private string fileName;

    // Use this for initialization
    void Start () {
        repotsNames.text = "";
        fileName = Application.persistentDataPath;
        foreach (string file in System.IO.Directory.GetFiles(fileName))
        {
            file.Remove(30);
            repotsNames.text += file + "\n\n";
        }

    }

    void OnEnable() {
        
    }
	
	// Update is called once per frame
	void Update () {


    }

    public void ShowReports()
    {
        fileName = Application.persistentDataPath;
        foreach (string file in System.IO.Directory.GetFiles(fileName))
        {
            print("!!!!AQUI: " + file);
        }
    }

}
