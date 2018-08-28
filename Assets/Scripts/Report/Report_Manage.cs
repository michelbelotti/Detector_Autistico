using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Report_Manage : MonoBehaviour {

    public GameObject GOphase2;
    public GameObject GOphase4;
    public GameObject GOphase6;
    public GameObject GOphase7;

    public Button next;
    public Button back;

    private GameManager scriptGameManager;
    private Phase2Manager phase2;
    //private Phase4Manager phase4;
    private Phase6Manager phase6;
    private Phase7Manager phase7;

    //Fase 4 Prefabs
    public GameObject[] prefabCharacters;
    private GameObject[] objCharacters;
    [HideInInspector]
    public List<Vector3> positionsObjs;
    public GameObject prefabPanel;
    private GameObject objPanel;


    // Use this for initialization
    void Start () {
        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        phase2 = GOphase2.GetComponent<Phase2Manager>();
        //phase4 = GOphase4.GetComponent<Phase4Manager>();
        phase6 = GOphase6.GetComponent<Phase6Manager>();
        phase7 = GOphase7.GetComponent<Phase7Manager>();

        //Instanciando objs Fase 4
        objCharacters = new GameObject[prefabCharacters.Length];
        for (int i = 0; i < prefabCharacters.Length; i++)
        {
            objCharacters[i] = Instantiate(prefabCharacters[i], positionsObjs[i], prefabCharacters[i].transform.rotation);
            objCharacters[i].GetComponent<Phase4DragObjects>().enabled = false;
        }
        objPanel = Instantiate(prefabPanel, prefabPanel.transform.position, prefabPanel.transform.rotation);

        next.gameObject.SetActive(true);
        back.gameObject.SetActive(false);

    }

    public string a = "1\n2\n3\n4\n5\n6\n7\n8zn9\n10!";

    void OnGUI()
    {        
        var aa = GUI.TextArea(new Rect(10, 30, 100, 100), a, 200);
        //GUI.Label(new Rect(10, 10, 100, 100), textArea, );
    }

    public void nextReport()
    {
        for (int i = 0; i < prefabCharacters.Length; i++)
        {
            objCharacters[i].SetActive(false);
        }
        objPanel.SetActive(false);

        next.gameObject.SetActive(false);
        back.gameObject.SetActive(true);

        //mostrando dados das outras fases
    }

    public void backReport() {
        for (int i = 0; i < prefabCharacters.Length; i++)
        {
            objCharacters[i].SetActive(true);
        }
        objPanel.SetActive(true);

        next.gameObject.SetActive(true);
        back.gameObject.SetActive(false);
    }

}
