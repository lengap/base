﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammoBar : MonoBehaviour
{


    public Slider slider;

    public void SetMaxAmmo(int ammo)
    {
        slider.maxValue = 100;
        slider.value = ammo;
    }

    public void SetAmmo(int ammo)
    {
        slider.value = ammo;
    }
}
