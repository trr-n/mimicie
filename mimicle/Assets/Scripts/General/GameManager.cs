using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Self.Utility;

namespace Self
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField]
        int startWave = 0;

        [SerializeField]
        Scroll scroll;

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

        // static string password = "mimicle";
        // /// <summary>
        // /// セーブ用パスワード
        // /// </summary>
        // public static string Password => password;

        // static string path = null;
        // /// <summary>
        // /// セーブファイルのパス
        // /// </summary>
        // public static string Path => path is null ? "Not yet" : path;

        static (string password, string path) saveFile = (password: "mimicle", null);
        /// <summary>
        /// セーブデータの設定s
        /// </summary>
        public static (string password, string path) SaveFile => (saveFile.password, saveFile.path is null ? "Not yet" : saveFile.path);

        void Start()
        {
            Score.StartTimer();
            Ctrlable = true;
            Scrollable = true;
            IsDead = false;
            sceneReloadPanel.color = Colour.transparent;

            Physics2D.gravity = Vector3.forward * 9.81f;
            App.SetFPS(60);
            App.SetCursorStatus(CursorAppearance.Invisible, CursorRangeOfMotion.Fixed);
        }

        void Update()
        {
            if (Feed.Pressed(Values.Key.Stop))
            {
                Time.timeScale = 0;
                Ctrlable = false;
                Scrollable = false;
            }

            else if (Feed.Released(Values.Key.Stop))
            {
                Time.timeScale = 1;
                Ctrlable = true;
                Scrollable = true;
            }
        }

        /// <summary>
        /// プレイヤーの死亡処理
        /// 複数回実行禁止
        /// </summary>
        public void PlayerIsDeath()
        {
            StartCoroutine(PlayerIsDeadCoroutine());
        }

        IEnumerator PlayerIsDeadCoroutine()
        {
            Score.ResetTimer();

            // TODO シーンリセット演出追加,スコアは表示しない
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
        /// ! ファイル名を現在時刻にしてるから **複数回実行禁止**
        /// </summary>
        public void End()
        {
            Ctrlable = false;
            Scrollable = false;
            Time.timeScale = 0;
            Score.StopTimer();

            ResultData data = new ResultData
            {
                time = 1,
                score = 1
            };

            saveFile.path = Application.dataPath + "/" + Temps.Raw2 + ".sav";
            // saveFile.path = Application.persistentDataPath + "/" + Temps.Raw2 + ".sav";
            Save.Write(data, saveFile.password, saveFile.path);
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
