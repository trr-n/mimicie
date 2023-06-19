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
        RaycastHit2D hit;

        float thickness = 0.075f;

        Vector3[] notHit = new Vector3[2];
        Vector3[] hitting = new Vector3[2];

        void Start()
        {
            player = GameObject.FindGameObjectWithTag(constant.Player)
                .GetComponent<Player>();
            line = GetComponent<LineRenderer>();
            // point = GameObject.FindGameObjectWithTag(constant.Player);
            point = GameObject.Find("gun");
            line.startWidth = thickness;
            line.endWidth = thickness;
        }

        void Update()
        {
            hit = player.Hit;
            notHit[0] = point.transform.position;
            notHit[1] = new(20.48f, point.transform.position.y);
            hitting[0] = notHit[0];
            // hitting[1] = hit.point;
            hitting[1] = new(hit.point.x, point.transform.position.y);
            line.SetPositions(hit.collider ? hitting : notHit);
            print(Mathf.DeltaAngle(notHit[1].y, hitting[1].y));
        }
    }
}
