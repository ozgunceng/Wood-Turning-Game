﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

 

    public void Update()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 5f; // Set this to be the distance you want the object to be placed in front of the camera.
        this.transform.position = Camera.main.ScreenToWorldPoint(temp);

    }
}
