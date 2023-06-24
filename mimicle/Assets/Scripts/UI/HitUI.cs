using UnityEngine;

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
