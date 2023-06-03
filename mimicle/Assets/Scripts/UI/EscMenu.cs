using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mimical.Extend;
using static Mimical.Extend.system;

namespace Mimical
{
    public class EscMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject menuPanel;

        [SerializeField]
        Text todayT;

        [SerializeField]
        Text systemT;

        [SerializeField]
        GameManager manager;

        string[] info;

        void Update()
        {
            Show();
        }

        void Start()
        {
            info = new string[] { OS(), CPU(), GPU(), RAM().ToString() };
        }

        void Show()
        {
            if (input.Down(KeyCode.Escape))
            {
                // メニューを開いた時の処理
                if (!menuPanel.IsActive())
                {
                    todayT.text = "きょうは" + time.Date();
                    systemT.text = "すぺっく".newline() +
                        "OS: " + info[0].newline() + "CPU: " + info[1].newline() +
                        "GPU: " + info[2].newline() + "RAM: " + info[3] + "GB";
                    menuPanel.SetActive(true);
                    manager.Pause();
                }
                else
                {
                    menuPanel.SetActive(false);
                    manager.Restart();
                }
            }
        }
    }
}
