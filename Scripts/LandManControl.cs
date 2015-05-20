using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandManControl : Animals
{

    // Use this for initialization
    protected override void Start()
    {
        startX = 270;
        startY = 154;
        type = 1;
        base.Start();
    }

}
