using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionPart : MonoBehaviour
{
    public GameObject scratch;

    public InteractiveObject wholeOnionPart;
    public InteractiveObject onionSlice;
    
    public void CutWithKnife()
    {
        scratch.SetActive(true);
        wholeOnionPart.enabled = false;
        onionSlice.enabled = true;
    }
    
    public void TakeOnionSlice()
    {
        wholeOnionPart.enabled = true;
        onionSlice.enabled = false;
    }
}
