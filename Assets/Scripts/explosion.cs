﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy( gameObject , 0.17f );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
