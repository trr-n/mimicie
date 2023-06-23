using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mimical.Extend;

using static Mimical.Extend.Sys;

namespace Mimical
{
    public class EscMenu : MonoBehaviour
    {
        [SerializeField]
        Text todayT;

        [SerializeField]
        Text systemT;

        [SerializeField]
        Text volumeT;

        [SerializeField]
        Text songT;

        [SerializeField]
        Speaker speaker;

        [SerializeField]
        GameManager manager;

        string[] info;

        void Start() => info = new string[] { OS(), CPU(), GPU(), RAM().ToString() };

        void Update() => Show();

        void Show()
        {
            speaker.VCtrl(volumeT);
            if (SelfInput.Down(Values.Key.Mute))
                speaker.VMute(volumeT);
            if (SelfInput.Down(Values.Key.MChange))
                speaker.Change(songT);
            if (SelfInput.Down(KeyCode.Escape))
            {
                if (!manager.menuPanel.IsActive(Active.Hierarchy))
                {
                    todayT.text = "きょうは" + time.Date();
                    systemT.text =
                        "すぺっく".newline() +
                        "OS: " + info[0].newline() +
                        "CPU: " + info[1].newline() +
                        "GPU: " + info[2].newline() +
                        "RAM: " + info[3] + "GB";
                    manager.Pause();
                    return;
                }
                manager.Restart();
            }
        }
    }
}
