using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyGlassSlideInteractiveController : MonoBehaviour
{
    public GameObject WaterDrop;
    public GameObject PollenWaterDrop;
    public GameObject CoverGlass;
    public GameObject WaterOnGlass;

    public InteractiveObject dropPosition;
    public InteractiveObject coverGlassPosition;
    public InteractiveObject wholeGlassSlide;
    
    public void DropWater()
    {
        WaterDrop.SetActive(true);
    }

    public void PutStamenInWaterDrop()
    {
        WaterDrop.SetActive(false);
        PollenWaterDrop.SetActive(true);
        dropPosition.enabled = false;
        coverGlassPosition.enabled = true;
    }

    public void PutCoverGlass()
    {
        PollenWaterDrop.SetActive(false);
        WaterOnGlass.SetActive(true);
        CoverGlass.SetActive(true);
        coverGlassPosition.enabled = false;
        wholeGlassSlide.enabled = true;
    }
}
