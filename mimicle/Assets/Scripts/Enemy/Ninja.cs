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
        GameObject smokeFX, deadFX;
        [SerializeField]
        AudioClip doronSE;

        new AudioSource audio;
        bool isNinning = false;
        Stopwatch shuriSW = new(true);
        Stopwatch moveSW = new();
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
            {
                deadFX.Instance(transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        void ThrowShuriken()
        {
            if (shuriSW.sf >= Shuri.Span)
            {
                StartCoroutine(Throw());
                shuriSW.Restart();
            }
        }

        IEnumerator Throw()
        {
            yield return null;
            shuriRZ = Vector3.Angle(-transform.right, player.transform.position - transform.position) - Shuri.Range / 2;
            for (int i = 0; i < Shuri.Count; i++)
            {
                var z = player.transform.position.y > transform.position.y ? -shuriRZ : shuriRZ;
                shuriken.Instance(transform.position, Quaternion.Euler(0, 0, z));
                shuriRZ += Shuri.Range / Shuri.Count + ofs;
            }
        }

        protected override void Move()
        {
            if (!p.NotNinnin)
            {
                moveSW.Restart();
                isNinning = true;
                fxflag = true;
            }

            if (moveSW.sf >= timing.Smoke && fxflag)
            {
                audio.PlayOneShot(doronSE);
                smokeFX.Instance(transform.position + FxOfs, Quaternion.identity);
                fxflag = false;
            }

            if (moveSW.sf >= timing.Teleport && isNinning)
            {
                var x = Rnd.randint(((int)Numeric.Round(player.transform.position.x + 2, 0)), 8);
                transform.position = new(x, Rnd.randfloat(-4, 4), 1);
                moveSW.Reset();
                isNinning = false;
            }
        }
    }
}
