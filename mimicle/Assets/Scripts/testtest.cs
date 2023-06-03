using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical.Test
{
    public class testtest : MonoBehaviour
    {
        // void _Update()
        void Start()
        {
            ("now: " + time.Date().space() + time.Time()).show();
            // ("self: " + this.gameObject.isActive(Active.Self).space() +
            // "hie: " + gameObject.isActive(Active.Hierarchy)).show();
        }
    }
}