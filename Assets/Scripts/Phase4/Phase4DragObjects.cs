using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase4DragObjects : MonoBehaviour
{
    private Phase4Manager scriptPhase4;

    void Start()
    {
        scriptPhase4 = GameObject.Find("Phase_4_Manager").GetComponent<Phase4Manager>();
    }

    public void drag()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(point.x, point.y, 0f);

        scriptPhase4.resetIdleTimer();
    }
}
