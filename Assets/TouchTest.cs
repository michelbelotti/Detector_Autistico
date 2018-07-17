using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour {

    Camera cam;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void touch()
    {
        Debug.Log("entrei");
        //Vector3 worldPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        //transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }
}
