using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Report_Single_Creation : MonoBehaviour {

    private Report_Save rs;

    //Vetor com todos os paineis fases 2,4,6 e 7
    public GameObject[] singlePainels;
    private int currentPhase;

    //botoes next e back para ativar e desativar
    public Button next;
    public Button back;

    //Fase 2 textos
    public Text totalPhase2;
    public Text averagePhase2;
    public Text latencyPhase2;

    //Fase 4 Posicoes
    //posicoes salvas
    public List<Vector3> phase4PosObjs;
    //prefabs
    public GameObject[] Phase4PrefabCharacters;
    public GameObject Phase4PrefabPainel;
    //instancias
    private GameObject[] objCharacters;
    private GameObject objPainel;

    //Fase 6 textos
    public Text phase6Tries;

    //Fase 7 textos
    public Text totalPhase7;
    public Text averagePhase7;
    public Text latencyPhase7;

    private void OnEnable()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadSave(string path)
    {
        string dataReport = File.ReadAllText(path);
        rs = JsonUtility.FromJson<Report_Save>(dataReport);

        // Fase 2 loading
        totalPhase2.text = "Total: " + rs.phase2Total;
        averagePhase2.text = "Média: " + string.Format("{0:#0.00}", rs.phase2average);
        latencyPhase2.text = "Latência dos clicks: ";
        foreach(float f in rs.phase2latency)
        {
            latencyPhase2.text += "" + string.Format("{0:#0.00}", f) + "; ";
        }

        print("AQUI!!!!!");

        //fase 4 loading
        /*
        for(int i=0; i< 8; i++)
        {
            for(int j=0; j<3; j++)
            {
                Debug.Log("i: " + i + "j: " + j + "rs: " + rs.phase4Positions[i,j]);
            }
        }
        */
        Debug.Log("AQUII: " + rs.phase4Positions);


        // Fase 6 loading
        phase6Tries.text = "Total de tentativas erradas: " + rs.phase6Tries;

        //Fase 7 loading
        totalPhase7.text = "Total: " + rs.phase7Total;
        averagePhase7.text = "Média: " + string.Format("{0:#0.00}", rs.phase7average);
        latencyPhase7.text = "Latência dos clicks: ";
        foreach (float f in rs.phase7latency)
        {
            latencyPhase7.text += "" + string.Format("{0:#0.00}", f) + "; ";
        }

        //Inicia painel fase 2
        currentPhase = 0;
        singlePainels[currentPhase].SetActive(true);
        back.gameObject.SetActive(false);
        next.gameObject.SetActive(true);
    }

    public void nextPhase()
    {
        singlePainels[currentPhase++].SetActive(false);
        singlePainels[currentPhase].SetActive(true);
        back.gameObject.SetActive(true);

        if (currentPhase >= singlePainels.Length - 1)
        {
            next.gameObject.SetActive(false);
        }
    }

    public void backPhase()
    {
        singlePainels[currentPhase--].SetActive(false);
        singlePainels[currentPhase].SetActive(true);
        next.gameObject.SetActive(true);

        if (currentPhase <= 0)
        {
            back.gameObject.SetActive(false);
        }
    }
}
