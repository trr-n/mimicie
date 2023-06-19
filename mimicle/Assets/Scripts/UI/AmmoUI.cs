using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mimical
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField]
        Ammo ammo;

        [SerializeField]
        Text ammoT;

        [SerializeField]

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ammoT.text = ammo.Remain.ToString();
        }
    }
}
