using System.Collections;
using UnityEngine;
using Self.Utils;

namespace Self
{
    public class Ninja : Enemy
    {
        [SerializeField]
        GameObject shurikenObj;

        [SerializeField]
        GameObject smokeEffect, deadEffect;

        [SerializeField]
        AudioClip doronSound;

        /// <summary>
        /// ninjaのHP
        /// </summary>
        HP ninjaHP;

        AudioSource speaker;

        /// <summary>
        /// にんにんしてたらTrue
        /// </summary>
        bool isNinning = false;

        /// <summary>
        /// 移動するまで計測するストップウォッチ
        /// </summary>
        Stopwatch moveStopwatch = new();

        GameObject playerObj;
        Player p;

        /// <summary>
        /// 回転Z座標
        /// </summary>
        float shurikenRotationZ;

        /// <summary>
        /// 手裏剣生成たち
        /// </summary>
        (int Count, float Span, float Range, int RZ, float Offset, Stopwatch stopwatch) shuriken = (
            Count: 5, Span: 2f, Range: 30f, RZ: 345, Offset: 1.5f, stopwatch: new(true));

        /// <summary>
        /// エフェクトの生成座標調整
        /// </summary>
        Vector3 EffectPosOffset => new(0, -1.5f, 0);

        /// <summary>
        /// エフェクト生成フラグ
        /// </summary>
        bool fxflag = false;

        /// <summary>
        /// タイミングs
        /// </summary>
        (float Smoke, float Teleport) timing = (0.2f, 0.7f);

        void Start()
        {
            speaker = GetComponent<AudioSource>();

            playerObj = GameObject.FindGameObjectWithTag(Constant.Player);
            p = playerObj.GetComponent<Player>();

            ninjaHP = GetComponent<HP>();
            ninjaHP.SetMax();

            shuriken.Range = 2 * (360 - shuriken.RZ);
        }

        void Update()
        {
            Move();
            ThrowShuriken();

            if (ninjaHP.IsZero)
            {
                // TODO
                // deadEffect.Generate(transform.position);
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 手裏剣なげ処理
        /// </summary>
        void ThrowShuriken()
        {
            if (shuriken.stopwatch.sf >= shuriken.Span)
            {
                StartCoroutine(Throw());
                shuriken.stopwatch.Restart();
            }
        }

        /// <summary>
        /// 手裏剣shuriken.Count連投
        /// </summary>
        IEnumerator Throw()
        {
            yield return null;

            shurikenRotationZ = Vector3.Angle(-transform.right, playerObj.transform.position - transform.position) - shuriken.Range / 2;
            for (int throwCount = 0; throwCount < shuriken.Count; throwCount++)
            {
                float spawnRotationZ = playerObj.transform.position.y > transform.position.y ? -shurikenRotationZ : shurikenRotationZ;
                shurikenObj.Generate(transform.position, Quaternion.Euler(0, 0, spawnRotationZ));

                shurikenRotationZ += shuriken.Range / shuriken.Count + shuriken.Offset;
            }
        }

        protected override void Move()
        {
            if (!p.NotNinnin)
            {
                moveStopwatch.Restart();
                isNinning = true;
                fxflag = true;
            }

            if (moveStopwatch.sf >= timing.Smoke && fxflag)
            {
                speaker.PlayOneShot(doronSound);
                smokeEffect.Generate(transform.position + EffectPosOffset);
                fxflag = false;
            }

            if (moveStopwatch.sf >= timing.Teleport && isNinning)
            {
                var x = Rnd.Int(((int)Numeric.Round(playerObj.transform.position.x + 2, 0)), 8);
                transform.position = new(x, Rnd.Float(-4, 4), 1);
                moveStopwatch.Reset();
                isNinning = false;
            }
        }
    }
}
