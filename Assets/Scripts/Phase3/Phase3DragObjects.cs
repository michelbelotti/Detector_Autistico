using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3DragObjects : MonoBehaviour {

    void Start () {
        
    }
	
    public void drag()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3 (point.x, point.y, 0f);
    }
}
