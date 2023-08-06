using System.Collections;
using UnityEngine;
using Self.Utils;

namespace Self.Game
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
        readonly Stopwatch moveStopwatch = new();

        GameObject playerObj;
        Player player;

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

            player = Gobject.GetWithTag<Player>(Constant.Player);
            playerObj = player.gameObject;

            ninjaHP = GetComponent<HP>();
            ninjaHP.Reset();

            shuriken.Range = 2 * (360 - shuriken.RZ);
        }

        void Update()
        {
            if (Time.timeScale == 0)
            {
                return;
            }

            Move();
            ThrowShuriken();

            if (ninjaHP.IsZero)
            {
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

            shurikenRotationZ = Vector3.Angle(
                -transform.right, playerObj.transform.position - transform.position) - shuriken.Range / 2;
            for (ushort throwCount = 0; throwCount < shuriken.Count; throwCount++)
            {
                float spawnRotationZ = playerObj.transform.position.y > transform.position.y ?
                    -shurikenRotationZ : shurikenRotationZ;

                shurikenObj.Generate(transform.position, Quaternion.Euler(0, 0, spawnRotationZ));

                shurikenRotationZ += shuriken.Range / shuriken.Count + shuriken.Offset;
            }
        }

        protected override void Move()
        {
            if (!player.NotNinnin)
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
                int x = Rand.Int((int)Numeric.Round(playerObj.transform.position.x + 2, 0), 8);
                transform.position = new(x, Rand.Float(-4, 4), 1);
                moveStopwatch.Reset();
                isNinning = false;
            }
        }
    }
}
