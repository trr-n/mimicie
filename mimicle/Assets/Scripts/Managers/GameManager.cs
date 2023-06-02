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

        void Start()
        {
            PlayerCtrlable = true;
            BGScrollable = true;
        }

        void Update()
        {
            debugT.text =
                "Scrollable: " + BGScrollable.newline() +
                "Controllable: " + PlayerCtrlable;

            if (input.Pressed(STOP))
            {
                // Time.timeScale で全部止めれるから他のフラグいらんかも
                // Time.timeScale = 0;
                PlayerCtrlable = false;
                BGScrollable = false;
            }
            else if (input.Up(STOP))
            {
                // Time.timeScale = 1;
                PlayerCtrlable = true;
                BGScrollable = true;
            }
        }
    }
}
