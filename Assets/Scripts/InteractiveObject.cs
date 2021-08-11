using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameManager.SelectStatus selectStatus;
    public Material originalMat;
    public Material selectedMat;
    public MeshRenderer[] areaMesh;
    
    public bool mouseOver;
    // Start is called before the first frame update
    void Start()
    {
        mouseOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseOver)
        {
            GameManager.instance.selectStatus = selectStatus;
            foreach (var mesh in areaMesh)
            {
                mesh.material = selectedMat;
            }
        }

    }
    
    void OnMouseOver()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        GameManager.instance.selectStatus = GameManager.SelectStatus.Empty;
        foreach (var mesh in areaMesh)
        {
            mesh.material = originalMat;
        }
    }

    private void OnEnable()
    {
        mouseOver = false;
        foreach (var mesh in areaMesh)
        {
            mesh.material = originalMat;
        }
    }
}
