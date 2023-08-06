using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        Image sceneReloadPanel;

        public GameObject menuPanel;

        /// <summary>
        /// プレイヤー制御用フラグ
        /// </summary>
        public bool Ctrlable { get; set; }

        /// <summary>
        /// 背景スクロール制御用フラグ
        /// </summary>
        public bool Scrollable { get; set; }

        /// <summary>
        /// メニューパネル(esc)が開いてたらTrue
        /// </summary>
        public bool IsOpeningMenu { get; set; }

        /// <summary>
        /// 死んだらTrue
        /// </summary>
        public bool IsDead { get; set; }

        bool isEnd = false;
        /// <summary>
        /// クリアしたらtrue
        /// </summary>
        public bool IsEnd => isEnd;

        void Start()
        {
            Score.StartTimer();

            Ctrlable = true;
            Scrollable = true;
            IsDead = false;
            sceneReloadPanel.color = Colour.Transparent;

            App.SetFPS(FrameRate.Medium);
            App.SetCursorStatus(CursorAppearance.Invisible, CursorRangeOfMotion.Fixed);
            App.SetGravity(0 * Coordinate.Z);
        }

        /// <summary>
        /// プレイヤーの死亡処理
        /// 複数回実行禁止
        /// </summary>
        public void PlayerIsDeath() => StartCoroutine(PlayerDeadCoroutine());

        IEnumerator PlayerDeadCoroutine()
        {
            Score.ResetTimer();

            float fadeAlpha = 0f;
            float alphaIncAmount = 0.02f;

            while (fadeAlpha >= 1)
            {
                fadeAlpha += alphaIncAmount * Time.unscaledDeltaTime;
                yield return null;
            }
        }

        /// <summary>
        /// クリア処理<br/>
        /// </summary>
        public void End()
        {
            Ctrlable = false;
            Scrollable = false;

            App.SetCursorStatus(CursorAppearance.Visible, CursorRangeOfMotion.Limitless);
            isEnd = true;
            Time.timeScale = 0;
            Score.StopTimer();
        }

        public void OpenMenuPanel()
        {
            menuPanel.SetActive(true);
            Score.StopTimer();
            Ctrlable = false;
            Scrollable = false;
            IsOpeningMenu = true;
            Time.timeScale = 0;
        }

        public void CloseMenuPanel()
        {
            menuPanel.SetActive(false);
            Score.StartTimer();
            Ctrlable = true;
            Scrollable = true;
            IsOpeningMenu = false;
            Time.timeScale = 1;
        }
    }
}
