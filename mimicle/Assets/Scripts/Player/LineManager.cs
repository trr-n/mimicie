using UnityEngine;

namespace Mimical
{
    public class LineManager : MonoBehaviour
    {
        [SerializeField]
        GameObject reticle;

        LineRenderer line;
        GameObject point;
        Player player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag(Constant.Player).GetComponent<Player>();
            point = GameObject.Find("gun");
            line = GetComponent<LineRenderer>();
            line.startWidth = line.endWidth = 0.075f;
        }

        void Update()
        {
            Vector3[] notHit = new[] { point.transform.position, new(20.48f, point.transform.position.y) },
                hitting = new[] { notHit[0], new(player.Hit.point.x, point.transform.position.y) };
            line.SetPositions(player.Hit.collider ? hitting : notHit);
            // TODO player.hitがnull
            if (player.Hit.collider.gameObject.TryGetComponent<SpriteRenderer>(out var hitSr))
            {
                ;
            }
        }
    }
}
