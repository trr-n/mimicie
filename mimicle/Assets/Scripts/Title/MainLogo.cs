using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public class MainLogo : MonoBehaviour
    {
        [SerializeField]
        Image fadingPanel;

        [SerializeField]
        Text pressT, clickT;

        [SerializeField]
        AudioClip[] presses, clicks;

        [SerializeField]
        Sprite[] logos = new Sprite[2];

        AudioSource speaker;

        /// <summary>
        /// 文字の色
        /// </summary>
        (Color inactive, Color active) colours = (
            inactive: new(0.311f, 0.196f, 0.157f, 1),
            active: new(1, 1, 0, 1)
        );

        /// <summary>
        /// ボタンが押されたらtrue
        /// </summary>
        bool isActivated = false;

        /// <summary>
        /// カーソルがロゴの上に乗ってたらtrue
        /// </summary>
        bool isMouseOverOnLogo = false;

        /// <summary>
        /// 
        /// </summary>
        bool timerFlag = true;

        /// <summary>
        /// クリックしてからシーンを遷移するまでの時間
        /// </summary>
        readonly float clickToTransition = 1;

        /// <summary>
        /// パネルの透明度
        /// </summary>
        float fadingPanelAlpha = 0;

        readonly (float PanelFade, float LogoRotate, float TextColorChange) speeds = (
            PanelFade: 0.008f,
            LogoRotate: 10,
            TextColorChange: 1
        );

        readonly Stopwatch transitionTimer = new();
        readonly Vector3 scale = new(2, 2, 2);

        SpriteRenderer sr;

        void Start()
        {
            Time.timeScale = 1;

            speaker = GetComponent<AudioSource>();

            pressT.color = clickT.color = colours.inactive;

            sr = GetComponent<SpriteRenderer>();
            sr.sprite = logos[0];
        }

        void Update()
        {
            Rotation();
            MouseOver();
            ChangeSprite();
        }

        /// <summary>
        /// 始めるか始めないかは、あなた次第。
        /// </summary>
        void ChangeSprite()
        {
            if (Inputs.Down(Constant.ChangeLogo))
            {
                if (sr.Compare(logos[0])) { sr.SetSprite(logos[1]); }
                else if (sr.Compare(logos[1])) { sr.SetSprite(logos[0]); }
            }
        }

        /// <summary>
        /// ロゴ回転
        /// </summary>
        void Rotation()
        {
            if (isMouseOverOnLogo)
            {
                // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 10 * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 10 * Time.deltaTime);
                return;
            }

            transform.Rotate(new(0, 0, speeds.LogoRotate * Time.deltaTime));
        }

        /// <summary>
        /// カーソルがロゴを触っていたら
        /// </summary>
        void MouseOver()
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // カーソルのいちに例のごとくレイをﾁｭｲｰﾝ
            var hit = Physics2D.Raycast(cursorPos, Vector2.up, 1);

            // ロゴにふれていたら
            if (hit && hit.collider.gameObject.name == Constant.Logo)
            {
                // 触れてるアピール
                isMouseOverOnLogo = true;

                // サイズを10%でかくする
                transform.localScale = Vector3.Lerp(transform.localScale, scale * 1.1f, 20 * Time.deltaTime);

                // 左指でポチッとされたら
                if (Inputs.Down(0))
                {
                    // ポチ音ならす
                    speaker.PlayOneShot(clicks.Choice3());

                    // テキストの色変える
                    ChangeTextsColor(clickT);
                }
            }

            // シャゲキンTV始まったら
            else if (Inputs.Down(Constant.Fire))
            {
                // 打鍵音を遊ぶ
                speaker.PlayOneShot(presses.Choice3());

                // もじいろあいちぇんじ
                ChangeTextsColor(pressT);
            }

            // カーソルがふれてなかったら
            if (!(hit && hit.collider.gameObject.name == Constant.Logo))
            {
                // 触れてないから流石に触れてるアピールはしない
                isMouseOverOnLogo = false;

                // 触れてるアピールはしないけど回転して存在をアピールする
                transform.localScale = Vector3.Lerp(transform.localScale, scale, 30 * Time.deltaTime);
            }

            // お洒落たら
            if (isActivated)
            {
                // 遷移するまで計測するやつスタート
                transitionTimer.Start();

                // 遷移あじまるあじまる
                if (transitionTimer.sf >= clickToTransition && timerFlag)
                {
                    // 色褪せてもらう
                    StartCoroutine(FadingOutPanel());

                    // OFF.
                    timerFlag = false;
                }
            }

            if (fadingPanel.color.a >= clickToTransition)
            {
                if (sr.Compare(logos[0]))
                {
                    MyScene.Load(Constant.Main);
                }

                else if (sr.Compare(logos[1]))
                {
                    Application.Quit();
                }
            }
        }

        void ChangeTextsColor(Text text)
        {
            text.color = Color.Lerp(colours.inactive, colours.active, speeds.TextColorChange);
            if (text.color.r >= colours.active.r)
            {
                isActivated = true;
            }
        }

        IEnumerator FadingOutPanel()
        {
            while (fadingPanelAlpha <= 1)
            {
                yield return null;

                fadingPanelAlpha = Mathf.Clamp01(fadingPanelAlpha);
                fadingPanel.color = new(0, 0, 0, fadingPanelAlpha += speeds.PanelFade);
            }
        }
    }
}
