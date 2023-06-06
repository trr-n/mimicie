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
        Text todayT;

        [SerializeField]
        Text systemT;

        [SerializeField]
        Text volumeT;

        [SerializeField]
        Speaker speaker;

        [SerializeField]
        GameManager manager;

        string[] info;

        void Update() => Show();

        void Start() => info = new string[] { OS(), CPU(), GPU(), RAM().ToString() };

        void Show()
        {
            speaker.VMute(volumeT);

            if (input.Down(KeyCode.Escape))
            {
                if (!manager.menuPanel.isActive(Active.Hierarchy))
                {
                    todayT.text = "きょうは" + time.Date();

                    systemT.text =
                        "すぺっく".newline() +
                        "OS: " + info[0].newline() +
                        "CPU: " + info[1].newline() +
                        "GPU: " + info[2].newline() +
                        "RAM: " + info[3] + "GB";

                    manager.Pause();
                }

                else manager.Restart();
            }

            if (volumeT.IsActive())
                speaker.VChange(volumeT);
        }
    }
}
