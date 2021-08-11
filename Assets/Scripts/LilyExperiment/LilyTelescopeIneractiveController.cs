using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyTelescopeIneractiveController : MonoBehaviour
{
    public InteractiveObject telescopeBase;

    public InteractiveObject telescope;

    public GameObject glassSlide;

    public Collider BaseCollider;

    public Collider TelescopeCollider;

    public void PutGlassSlideOnTelescopeBase()
    {
        glassSlide.SetActive(true);
        telescopeBase.enabled = false;
        telescope.enabled = true;
        BaseCollider.enabled = false;
        TelescopeCollider.enabled = true;
    }
}
