using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    private GameManager scriptGameManager;

	// Use this for initialization
	void Start () {
        scriptGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startClicked() {
        scriptGameManager.setPhaseTrue();
    }

    public void creditClicked() {

    }

    public void exitClicked() {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit ();
        #endif
    }

    void OnDisable()
    {
        Debug.Log("Disable Menu");
    }
}
