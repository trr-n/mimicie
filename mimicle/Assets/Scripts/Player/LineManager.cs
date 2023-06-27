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
            line.startWidth = line.endWidth = 0.075f;
        }

        void Update()
        {
            Vector3[] notHit = new[] { point.transform.position, new(20.48f, point.transform.position.y) },
                hitting = new[] { notHit[0], new(player.Hit.point.x, point.transform.position.y) };
            line.SetPositions(player.Hit.collider ? hitting : notHit);
        }
    }
}
