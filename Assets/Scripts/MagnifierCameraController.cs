using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MagnifierCameraController : MonoBehaviour
{
    public float cameraMoveSpeed = 5.0f;
    public float distance = 10.0f;
    public float xRotation = 0.0f;
    public float yRotation = 0.0f;
    public Transform centerPoint;

    public Camera cameraControlled;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 delta = new Vector3(Mathf.Sin(xRotation * Mathf.Deg2Rad) * Mathf.Cos(yRotation * Mathf.Deg2Rad),
            Mathf.Sin(yRotation * Mathf.Deg2Rad),
            Mathf.Cos(xRotation * Mathf.Deg2Rad) * Mathf.Cos(yRotation * Mathf.Deg2Rad)
        );
        transform.position = centerPoint.position + delta * distance;
        transform.forward = -delta;
        // Debug.Log("x:"+Input.GetAxis("Mouse X").ToString()+"   y:"+Input.GetAxis("Mouse Y").ToString());
        xRotation += Input.GetAxis("Mouse X") * 5.0f;
        if (xRotation > 180) xRotation -= 360;
        if (xRotation < -180) xRotation += 360;
        yRotation += Input.GetAxis("Mouse Y") * 5.0f;
        if (yRotation > 89.9f) yRotation = 89.9f;
        if (yRotation < -89.9f) yRotation = -89.9f;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (cameraControlled.fieldOfView < 40)
                cameraControlled.fieldOfView += 2;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (cameraControlled.fieldOfView > 4)
                cameraControlled.fieldOfView -= 2;
        }
    }
}