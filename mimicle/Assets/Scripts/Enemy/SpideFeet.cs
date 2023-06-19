using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class SpideFeet : MonoBehaviour
    {
        int hitCounter = 0;
        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(constant.Player))
            {
                info.Get<HP>().Damage(Values.Damage.Spide);
                gameObject.Remove();
            }
        }
    }
}