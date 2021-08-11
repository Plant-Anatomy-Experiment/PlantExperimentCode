using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    public float cameraMoveSpeed = 5.0f;
    public float xRotation = 60.0f;
    
    public GameObject cameraGameObject;
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
        
        float mouseX = Input.GetAxis("Mouse X") * cameraMoveSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * cameraMoveSpeed;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, 30f, 90f);

        transform.Rotate(mouseX * Vector3.up);
        cameraGameObject.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(cameraControlled.fieldOfView < 40)
                cameraControlled.fieldOfView += 2;
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(cameraControlled.fieldOfView > 6)
                cameraControlled.fieldOfView -= 2;
        }
    }
}
