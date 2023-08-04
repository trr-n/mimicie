using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public sealed class GameManager : MonoBehaviour
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

        static (string password, string path) save = (password: "mimicle", null);
        /// <summary>
        /// セーブデータの設定s
        /// </summary>
        public static (string password, string path) SaveFile => (save.password, save.path is null ? "Not yet" : save.path);

        void Start()
        {
            Score.StartTimer();
            Ctrlable = true;
            Scrollable = true;
            IsDead = false;
            sceneReloadPanel.color = Colour.transparent;

            Physics2D.gravity = Vector3.forward * 9.81f;
            App.SetFPS(FrameRate.Medium);
            App.SetCursorStatus(CursorAppearance.Invisible, CursorRangeOfMotion.Fixed);
        }

        /// <summary>
        /// プレイヤーの死亡処理
        /// 複数回実行禁止
        /// </summary>
        public void PlayerIsDeath() => StartCoroutine(PlayerIsDeadCoroutine());

        IEnumerator PlayerIsDeadCoroutine()
        {
            Score.ResetTimer();

            float fadeAlpha = 0f;
            float alphaIncAmount = 0.02f;
            while (fadeAlpha >= 1)
            {
                yield return null;

                fadeAlpha += alphaIncAmount * Time.unscaledDeltaTime;
            }
        }

        /// <summary>
        /// クリア処理<br/>
        /// </summary>
        public void End()
        {
            App.SetCursorStatus(CursorAppearance.Visible, CursorRangeOfMotion.Limitless);
            Ctrlable = false;
            Scrollable = false;
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
