using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyInteractiveController : MonoBehaviour
{
    public InteractiveObject[] lilyPetals;
    public InteractiveObject[] lilyStamens;

    public InteractiveObject lilyPistil;
    
    public InteractiveObject wholeLily;
    public Collider wholeLilyCollider;
    
    
    public void ObserveWholeLily()
    {
        wholeLily.enabled = false;
        wholeLilyCollider.enabled = false;
        foreach (var petal in lilyPetals)
        {
            petal.enabled = true;
        }
    }

    public void TakeLilyPetal()
    {
        foreach (var petal in lilyPetals)
        {
            if (petal.mouseOver == true)
                petal.gameObject.SetActive(false);
            else
                petal.enabled = false;
        }
        foreach (var stamen in lilyStamens)
        {
            stamen.enabled = true;
        }
    }
    
    public void TakeLilyStamen()
    {
        foreach (var stamen in lilyStamens)
        {
            if (stamen.mouseOver == true)
                stamen.gameObject.SetActive(false);
            else
                stamen.enabled = false;
        }

        lilyPistil.enabled = true;
    }
    
    public void TakeLilyPistil()
    {
        lilyPistil.gameObject.SetActive(false);
    }
}