using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public class LineManager : MonoBehaviour
    {
        LineRenderer line;
        GameObject point;
        Player player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag(Constant.Player).GetComponent<Player>();
            line = GetComponent<LineRenderer>();
            point = GameObject.Find("gun");
            var thickness = 0.075f;
            line.startWidth = thickness;
            line.endWidth = thickness;
        }

        void Update()
        {
            var notHit = new Vector3[] { point.transform.position, new(20.48f, point.transform.position.y) };
            var hitting = new Vector3[] { notHit[0], new(player.Hit.point.x, point.transform.position.y) };
            line.SetPositions(player.Hit.collider ? hitting : notHit);
        }
    }
}
