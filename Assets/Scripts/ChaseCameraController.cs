using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChaseCameraController : MonoBehaviour
{
    public Camera cameraControlled;

    private Transform startTransform;
    private Transform endTransform;
    private float startFOV;
    private float endFOV;

    public float wholeTime;
    private float currentTime;
    private bool beginChase;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (beginChase)
        {
            Debug.Log(currentTime.ToString());
            transform.position = Vector3.Lerp(startTransform.position, endTransform.position, currentTime / wholeTime);
            transform.rotation =
                Quaternion.Lerp(startTransform.rotation, endTransform.rotation, currentTime / wholeTime);
            cameraControlled.fieldOfView = startFOV * (1 - currentTime / wholeTime) + endFOV * currentTime / wholeTime;
            currentTime += Time.deltaTime;
            if (currentTime > wholeTime)
            {
                currentTime = wholeTime;
                beginChase = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void CameraChase(Transform startTransform, float startFOV, Transform endTransform, float endFOV)
    {
        if (beginChase) return;
        currentTime = 0;
        this.startTransform = startTransform;
        this.endTransform = endTransform;
        this.startFOV = startFOV;
        this.endFOV = endFOV;
        beginChase = true;
    }
}