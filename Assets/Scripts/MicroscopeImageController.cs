using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroscopeImageController : MonoBehaviour
{
    public int lensStatus; // 0 = 低倍镜 1 = 中倍镜 2 = 高倍镜
    public GameObject LowPowerLensImage;
    public GameObject MidPowerLensImage;
    public GameObject HighPowerLensImage;

    private void Start()
    {
        lensStatus = 0;
        LowPowerLensImage.SetActive(true);
        MidPowerLensImage.SetActive(false);
        HighPowerLensImage.SetActive(false);
    }

    public void ChangeLens()
    {
        if (lensStatus == 0)
        {
            lensStatus = 1;
            LowPowerLensImage.SetActive(false);
            MidPowerLensImage.SetActive(true);
            HighPowerLensImage.SetActive(false);
        }
        else if (lensStatus == 1)
        {
            lensStatus = 2;
            LowPowerLensImage.SetActive(false);
            MidPowerLensImage.SetActive(false);
            HighPowerLensImage.SetActive(true);
        }
        else if (lensStatus == 2)
        {
            lensStatus = 0;
            LowPowerLensImage.SetActive(true);
            MidPowerLensImage.SetActive(false);
            HighPowerLensImage.SetActive(false);
        }
    }
}
