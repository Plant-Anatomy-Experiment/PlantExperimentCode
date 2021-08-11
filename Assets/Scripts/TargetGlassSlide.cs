using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGlassSlide : MonoBehaviour
{
    public GameObject WaterDrop;
    public GameObject OnionSlice;
    public GameObject CoverGlass;
    public GameObject IodineDrop;
    public GameObject IodineOnGlass;

    public InteractiveObject glassSlide;
    public InteractiveObject iodinePoint;
    public InteractiveObject blotterPoint;
    
    public void DropWater()
    {
        //Debug.Log("DropWater");
        WaterDrop.SetActive(true);
    }

    public void PutOnionSlice()
    {
        //Debug.Log("PutOnionSlice");
        OnionSlice.SetActive(true);
    }

    public void PutCoverGlass()
    {
        //Debug.Log("PutCoverGlass");
        CoverGlass.SetActive(true);
        WaterDrop.SetActive(false);
        glassSlide.enabled = false;
        iodinePoint.enabled = true;
    }
    
    public void DropIodine()
    {
        IodineDrop.SetActive(true);
        iodinePoint.enabled = false;
        blotterPoint.enabled = true;
    }
    
    public void PutOnBlotter()
    {
        IodineDrop.SetActive(false);
        IodineOnGlass.SetActive(true);
        blotterPoint.enabled = false;
        glassSlide.enabled = true;
    }
}
