using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class MobSpawner : MonoBehaviour
    {
        Wave wave;

        void Start()
        {
            wave = GetComponent<Wave>();
        }
    }
}
