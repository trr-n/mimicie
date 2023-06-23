using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
{
    public class HitUI : MonoBehaviour
    {
        Player playerRay;

        // Text text;

        void Start()
        {
            playerRay = GameObject.FindGameObjectWithTag(Constant.Player)
                .GetComponent<Player>();
        }

        void _Update()
        {
        }
    }
}
