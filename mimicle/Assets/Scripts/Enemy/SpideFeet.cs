using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class SpideFeet : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player))
            {
                info.Get<HP>().Damage(Values.Damage.Spide);
                gameObject.Remove();
            }
            if (info.Compare(Constant.Bullet))
            {
                info.gameObject.Remove();
                gameObject.Remove();
            }
        }
    }
}