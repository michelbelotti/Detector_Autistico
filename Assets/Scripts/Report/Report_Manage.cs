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


    //Panel Report Phases
    public GameObject panelPhase2;
    public GameObject panelPhase6;
    public GameObject panelPhase7;


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

        panelPhase2.SetActive(false);
        panelPhase6.SetActive(false);
        panelPhase7.SetActive(false);

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

        panelPhase2.SetActive(true);
        panelPhase6.SetActive(true);
        panelPhase7.SetActive(true);

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

        panelPhase2.SetActive(false);
        panelPhase6.SetActive(false);
        panelPhase7.SetActive(false);
    }

}
