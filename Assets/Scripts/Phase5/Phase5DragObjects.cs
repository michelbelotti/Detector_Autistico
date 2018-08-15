using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase5DragObjects : MonoBehaviour
{

    private Phase5Manager scriptPhase;

    void Start()
    {
        scriptPhase = GameObject.Find("Phase_5_Manager").GetComponent<Phase5Manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void drag()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(point.x, point.y, 0f);
    }

    public void release()
    {
        scriptPhase.ObjectRelease();
    }
}
