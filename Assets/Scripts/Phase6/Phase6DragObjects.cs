using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase6DragObjects : MonoBehaviour
{

    private Phase6Manager scriptPhase;

    void Start()
    {
        scriptPhase = GameObject.Find("Phase_6_Manager").GetComponent<Phase6Manager>();
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
