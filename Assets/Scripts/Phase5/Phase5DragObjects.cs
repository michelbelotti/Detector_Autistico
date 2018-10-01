using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase5DragObjects : MonoBehaviour
{

    private Phase5Manager scriptPhaseManager;

    void Start()
    {
        scriptPhaseManager = GameObject.Find("Phase_5_Manager").GetComponent<Phase5Manager>();
    }

    public void drag()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(point.x, point.y, 0f);
    }

    public void release()
    {
        scriptPhaseManager.CheckObject();
    }
}
