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
        const int ShuriCount = 5;
        GameObject player;
        Player p;
        float shuriRZ;
        const float ShuriSpan = 0.2f;
        const float ShuriRange = 30f;
        HP hp;
        Vector3 FxOfs => new(0, -1.5f, 0);
        bool fxflag = false;
        (float MakeSmoke, float Teleport) timing = (0.2f, 0.7f);

        void Start()
        {
            audio = GetComponent<AudioSource>();
            player = GameObject.FindGameObjectWithTag(Constant.Player);
            p = player.GetComponent<Player>();
            hp = GetComponent<HP>();
            base.Start(hp);
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
            if (sw_shuri.sf >= 2)
            {
                StartCoroutine(Throw());
                sw_shuri.Restart();
            }
        }

        IEnumerator Throw()
        {
            shuriRZ = 345f;
            for (int i = 0; i < ShuriCount; i++)
            {
                shuriken.Instance(transform.position, Quaternion.Euler(0, 0, shuriRZ));
                shuriRZ += ShuriRange / ShuriCount;
                yield return new WaitForSeconds(ShuriSpan);
            }
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
            if (sw_move.sf >= timing.MakeSmoke && fxflag)
            {
                audio.PlayOneShot(se_doron);
                fx_smoke.Instance(transform.position + FxOfs, Quaternion.identity);
                fxflag = false;
            }
            if (sw_move.sf >= timing.Teleport && isNinning)
            {
                transform.position = new(Rnd.randint(((int)Numeric.Round(player.transform.position.x + 2, 0)), 8), y: Rnd.randfloat(-4, 4), 1);
                sw_move.Reset();
                isNinning = false;
            }
        }
    }
}
