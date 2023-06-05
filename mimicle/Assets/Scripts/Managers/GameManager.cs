using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        Text debugT;

        public GameObject menuPanel;

        [SerializeField]
        /// <summary>
        /// ゲーム内の時間を止める
        /// </summary>
        KeyCode STOP = KeyCode.Backspace;

        [SerializeField]
        KeyCode reload = KeyCode.LeftShift;
        /// <summary>
        /// リロードキー@Player
        /// </summary>
        public KeyCode Reload => reload;

        [SerializeField]
        KeyCode fire = KeyCode.Space;
        /// <summary>
        /// 発砲キー@Player
        /// </summary>
        public KeyCode Fire => fire;

        [SerializeField]
        Scroll scroll;

        /// <summary>プレイヤーのコントロール(移動など)</summary>
        public bool PlayerCtrlable { get; set; }

        /// <summary>背景のスクロール</summary>
        public bool BGScrollable { get; set; }

        /// <summary>
        /// Escメニューを開いている間 True
        /// </summary>
        public bool IsOpenMenu { get; set; }

        bool passing = true;
        /// <summary>
        /// 時間が止まっていなかったら True
        /// </summary>
        public bool IsSpent => passing;

        Speaker speaker;

        void Start()
        {
            PlayerCtrlable = true;

            BGScrollable = true;

            Physics2D.gravity = Vector3.forward * 9.81f;

            speaker = GetComponent<Speaker>();
        }

        void Update()
        {
            passing = Time.timeScale == 1;

            debugT.text =
                "Scrollable: " + BGScrollable.newline() +
                "Controllable: " + PlayerCtrlable.newline() +
                "isOpenMenu: " + IsOpenMenu.newline() +
                "isSpent: " + passing.newline();

            if (input.Pressed(STOP))
            {
                Time.timeScale = 0;

                PlayerCtrlable = false;

                BGScrollable = false;
            }

            else if (input.Up(STOP))
            {
                Time.timeScale = 1;

                PlayerCtrlable = true;

                BGScrollable = true;
            }
        }

        public void Pause()
        {
            menuPanel.SetActive(true);

            PlayerCtrlable = false;

            BGScrollable = false;

            IsOpenMenu = true;

            Time.timeScale = 0;
        }

        public void Restart()
        {
            menuPanel.SetActive(false);

            PlayerCtrlable = true;

            BGScrollable = true;

            IsOpenMenu = false;

            Time.timeScale = 1;
        }
    }
}
