using UnityEngine;
using System.Collections;

public class Phase7RotateObj : MonoBehaviour
{

    public float rotationSpeed = 10.0F;
    public float lerpSpeed = 1.0F;

    private Vector3 theSpeed;
    private Vector3 avgSpeed;
    private bool isDragging = false;

    private Phase7Manager scriptPhase7;

    void Start()
    {
        scriptPhase7 = GameObject.Find("Phase_7_Manager").GetComponent<Phase7Manager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                scriptPhase7.addValueOnList();
            }
        }

        if (Input.touchCount > 0 && isDragging)
        {

            float pointer_x = Input.touches[0].deltaPosition.x;
            float pointer_y = Input.touches[0].deltaPosition.y;

            theSpeed = new Vector3(-pointer_x, pointer_y, 0.0F);
            avgSpeed = Vector3.Lerp(avgSpeed, theSpeed, Time.deltaTime * 5);
        }
        else
        {
            if (isDragging)
            {
                theSpeed = avgSpeed;
                isDragging = false;
            }
            float i = Time.deltaTime * lerpSpeed;
            theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, i);
        }

        transform.Rotate(Camera.main.transform.forward * (theSpeed.x * -1) * rotationSpeed, Space.World);
        transform.Rotate(Camera.main.transform.forward * theSpeed.y * rotationSpeed, Space.World);
    }
}