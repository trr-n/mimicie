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
                Destroy(gameObject);
            }
            if (info.Compare(Constant.Bullet))
            {
                Destroy(info.gameObject);
                Destroy(gameObject);
            }
        }
    }
}