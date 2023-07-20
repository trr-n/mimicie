using System.Collections;
using UnityEngine;
using MyGame.Utils;

namespace MyGame
{
    public class Ninja : Enemy
    {
        [SerializeField]
        GameObject shuriken;

        [SerializeField]
        GameObject smokeFX, deadFX;

        [SerializeField]
        AudioClip doronSE;

        /// <summary>
        /// ninjaのHP
        /// </summary>
        HP hp;

        new AudioSource audio;

        /// <summary>
        /// にんにんしてたらTrue
        /// </summary>
        bool isNinning = false;

        /// <summary>
        /// 連射用ストップウォッチ
        /// </summary>
        Stopwatch shuriSW = new(true);

        /// <summary>
        /// 移動するまで計測するストップウォッチ
        /// </summary>
        Stopwatch moveSW = new();

        GameObject player;
        Player p;

        /// <summary>
        /// 回転Z座標
        /// </summary>
        float shuriRZ;

        /// <summary>
        /// 手裏剣生成たち
        /// </summary>
        (int Count, float Span, float Range, int RZ) shuri = (Count: 5, Span: 2f, Range: 30f, RZ: 345);

        /// <summary>
        /// 弾の上下の間隔
        /// </summary>
        const float ofs = 1.5f;

        /// <summary>
        /// エフェクトの生成座標補正
        /// </summary>
        Vector3 FxOfs => new(0, -1.5f, 0);

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
            audio = GetComponent<AudioSource>();

            player = GameObject.FindGameObjectWithTag(Constant.Player);
            p = player.GetComponent<Player>();

            hp = GetComponent<HP>();
            base.Start(hp);

            shuri.Range = 2 * (360 - shuri.RZ);
        }

        void Update()
        {
            Move();
            ThrowShuriken();

            if (hp.IsZero)
            {
                deadFX.Generate(transform.position);
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 手裏剣なげ処理
        /// </summary>
        void ThrowShuriken()
        {
            if (shuriSW.sf >= shuri.Span)
            {
                StartCoroutine(Throw());
                shuriSW.Restart();
            }
        }

        /// <summary>
        /// 投げ
        /// </summary>
        IEnumerator Throw()
        {
            yield return null;
            shuriRZ = Vector3.Angle(-transform.right, player.transform.position - transform.position) - shuri.Range / 2;
            for (int count = 0; count < shuri.Count; count++)
            {
                var z = player.transform.position.y > transform.position.y ? -shuriRZ : shuriRZ;
                shuriken.Generate(transform.position, Quaternion.Euler(0, 0, z));
                shuriRZ += shuri.Range / shuri.Count + ofs;
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
                smokeFX.Generate(transform.position + FxOfs);
                fxflag = false;
            }

            if (moveSW.sf >= timing.Teleport && isNinning)
            {
                var x = Rnd.Int(((int)Numeric.Round(player.transform.position.x + 2, 0)), 8);
                transform.position = new(x, Rnd.Float(-4, 4), 1);
                moveSW.Reset();
                isNinning = false;
            }
        }
    }
}
