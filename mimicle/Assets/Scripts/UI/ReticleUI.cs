using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public class ReticleUI : MonoBehaviour
    {
        [SerializeField]
        Player player;

        void Update()
        {
            transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, player.Hit.point);
        }
    }
}