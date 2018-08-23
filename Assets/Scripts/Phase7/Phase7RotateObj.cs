using UnityEngine;
using System.Collections;

public class Phase7RotateObj : MonoBehaviour
{

    public float rotationSpeed = 10.0F;
    public float lerpSpeed = 1.0F;

    private Vector3 theSpeed;
    private Vector3 avgSpeed;
    private bool isDragging = false;
    //private Vector3 targetSpeedX;

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        isDragging = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && isDragging)
        {
            Debug.Log("GetMouseButton");
            theSpeed = new Vector3(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0F);
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

        //transform.Rotate(Camera.main.transform.up * theSpeed.x * rotationSpeed, Space.World);
        //transform.Rotate(Camera.main.transform.right * theSpeed.y * rotationSpeed, Space.World);
        transform.Rotate(Camera.main.transform.forward * (theSpeed.x * -1) * rotationSpeed, Space.World);
        transform.Rotate(Camera.main.transform.forward * theSpeed.y * rotationSpeed, Space.World);
    }
}