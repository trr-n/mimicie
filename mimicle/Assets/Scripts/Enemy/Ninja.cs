using System.Collections;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Ninja : Enemy
    {
        [SerializeField]
        GameObject shuriken;
        [SerializeField]
        GameObject fx_smoke;
        [SerializeField]
        AudioClip se_doron;

        new AudioSource audio;
        bool isNinning = false;
        Stopwatch sw_shuri = new(true);
        Stopwatch sw_move = new();
        GameObject player;
        Player p;
        float shuriRZ;
        (int Count, float Span, float Range, int RZ) Shuri = (Count: 5, Span: 2f, Range: 30f, RZ: 345);
        const float ofs = 1.5f;
        HP hp;
        Vector3 FxOfs => new(0, -1.5f, 0);
        bool fxflag = false;
        (float Smoke, float Teleport) timing = (0.2f, 0.7f);

        void Start()
        {
            audio = GetComponent<AudioSource>();
            player = GameObject.FindGameObjectWithTag(Constant.Player);
            p = player.GetComponent<Player>();
            hp = GetComponent<HP>();
            base.Start(hp);
            Shuri.Range = 2 * (360 - Shuri.RZ);
        }

        void Update()
        {
            Move();
            ThrowShuriken();
            if (hp.IsZero)
                Destroy(gameObject);
        }

        void ThrowShuriken()
        {
            if (sw_shuri.sf >= Shuri.Span)
            {
                StartCoroutine(Throw());
                sw_shuri.Restart();
            }
        }

        IEnumerator Throw()
        {
            shuriRZ = Vector3.Angle(-transform.right, player.transform.position - transform.position) - Shuri.Range / 2;
            for (int i = 0; i < Shuri.Count; i++)
            {
                shuriken.Instance(transform.position, Quaternion.Euler(
                    0, 0, player.transform.position.y > transform.position.y ? -shuriRZ : shuriRZ));
                shuriRZ += Shuri.Range / Shuri.Count + ofs;
            }
            yield return null;
        }

        protected override void Move()
        {
            // teleport
            if (!p.NotNinnin)
            {
                sw_move.Restart();
                isNinning = true;
                fxflag = true;
            }
            if (sw_move.sf >= timing.Smoke && fxflag)
            {
                audio.PlayOneShot(se_doron);
                fx_smoke.Instance(transform.position + FxOfs, Quaternion.identity);
                fxflag = false;
            }
            if (sw_move.sf >= timing.Teleport && isNinning)
            {
                transform.position = new(Rnd.randint(((int)Numeric.Round(player.transform.position.x + 2, 0)), 8), Rnd.randfloat(-4, 4), 1);
                sw_move.Reset();
                isNinning = false;
            }
        }
    }
}
