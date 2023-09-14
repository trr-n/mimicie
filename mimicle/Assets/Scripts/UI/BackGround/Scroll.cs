using Self.Utils;
using UnityEngine;

namespace Self.Game
{
    public class Scroll : MonoBehaviour
    {
        [SerializeField]
        Color[] colors;

        [SerializeField]
        GameObject[] backgrounds;

        GameManager manager;

        /// <summary>
        /// スクロールの速度
        /// </summary>
        readonly float speed = 3;

        /// <summary>
        /// 背景オブジェクトのセイセイ座標
        /// </summary>
        Vector2 Spawn => new(20f, 0);

        // 背景を動かすか
        bool scrollable = false;

        void Awake()
        {
            if (MyScene.active == Constant.Main)
            {
                manager = Gobject.GetWithTag<GameManager>(Constant.Manager);
            }
        }

        void Start()
        {
            foreach (var i in backgrounds)
            {
                i.GetComponent<SpriteRenderer>().color = colors.Choice3();
            }
        }

        void Update()
        {
            // Mainシーン(ゲームのしーん)だったらマネジャー次第
            if (MyScene.active == Constant.Main) { scrollable = manager.Scrollable; }

            // Titleシーンなら問答無用で無賃労働
            else { scrollable = true; }

            // スクロース
            if (!scrollable) { return; }

            foreach (var i in backgrounds)
            {
                i.transform.Translate(Time.deltaTime * speed * Vector2.left);

                // 逃げ切ったと思わせといて
                if (i.transform.position.x <= -20f)
                {
                    // 引き戻して
                    i.transform.position = Spawn;

                    // 色をランダムに変える
                    i.GetComponent<SpriteRenderer>().color = colors.Choice3();
                }
            }
        }
    }
}
