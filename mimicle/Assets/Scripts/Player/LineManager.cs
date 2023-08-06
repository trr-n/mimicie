using UnityEngine;
using Self.Utils;

namespace Self.Game
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
            player = Gobject.GetWithTag<Player>(Constant.Player);
            point = GameObject.Find("gun");

            line = GetComponent<LineRenderer>();
            line.startWidth = line.endWidth = 0.075f;
        }

        void Update()
        {
            Vector3[] notHit = new[] { point.transform.position, new(20.48f, point.transform.position.y) },
                hitting = new[] { notHit[0], new(player.Hit.point.x, point.transform.position.y) };
            line.SetPositions(player.Hit.collider ? hitting : notHit);

            var playerObj = player.Hit.collider.gameObject;

            if (player.Hit.collider != null && playerObj.TryGetComponent<SpriteRenderer>(out var hitSr))
            {
                reticle.SetActive(true);
                reticle.transform.position = player.Hit.point + new Vector2(hitSr.bounds.size.x / 2, 0);
                return;
            }

            reticle.SetActive(false);
        }
    }
}
