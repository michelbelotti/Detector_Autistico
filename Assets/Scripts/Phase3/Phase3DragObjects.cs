using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3DragObjects : MonoBehaviour
{

    private Phase3Manager scriptPhaseManager;

    void Start()
    {
        scriptPhaseManager = GameObject.Find("Phase_3_Manager").GetComponent<Phase3Manager>();
    }

    public void drag()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(point.x, point.y, 0f);
    }

    public void drop()
    {
        scriptPhaseManager.CheckObject();
    }
}