using UnityEngine;

namespace UnionEngine
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

            if (player.Hit.collider is null)
            {
                reticle.SetActive(false);
            }

            if (player.Hit.collider is not null && player.Hit.collider.gameObject.TryGetComponent<SpriteRenderer>(out var hitSr))
            {
                reticle.SetActive(true);
                reticle.transform.position = player.Hit.point + new Vector2(hitSr.bounds.size.x / 2, 0);
            }
        }
    }
}
