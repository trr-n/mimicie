using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mimical.Test
{
    public class Tests : MonoBehaviour
    {
        [SerializeField]
        Text t;

        [SerializeField]
        PlayerMovement player;

        void Start()
        {

        }

        void Update()
        {
            t.text = player.SelfPos.ToString();
        }
    }
}
