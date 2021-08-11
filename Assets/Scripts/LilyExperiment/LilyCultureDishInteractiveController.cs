using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyCultureDishInteractiveController : MonoBehaviour
{
    public GameObject lilyPetal;
    public GameObject lilyStamen;
    public GameObject lilyPistil;

    public InteractiveObject wholeDish;
    public Collider wholeDishCollider;
    
    public InteractiveObject wholePistil;
    public Collider wholePistilCollider;
    
    public GameObject OvaryBeforeCut;
    public GameObject OvaryAfterCut;
    
    public void PutPetalOnDish()
    {
        lilyPetal.SetActive(true);
    }
    
    public void PutStamenOnDish()
    {
        lilyStamen.SetActive(true);
    }
    
    public void PutPistilOnDish()
    {
        lilyPistil.SetActive(true);
        wholeDish.enabled = false;
        wholeDishCollider.enabled = false;
    }
    
    public void TakeStamenFromDish()
    {
        lilyStamen.SetActive(false);
    }
    
    public void CutPistil()
    {
        OvaryBeforeCut.SetActive(false);
        OvaryAfterCut.SetActive(true);
        wholePistil.enabled = false;
        wholePistilCollider.enabled = false;
    }

    public void TakeOvaryFromDish()
    {
        OvaryAfterCut.SetActive(false);
    }
    
}
