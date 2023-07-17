using UnityEngine;
using UnityEngine.UI;
using Feather.Utils;
using static Feather.Utils.Sys;

namespace Feather
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

        void Start()
        {
            info = new string[] { OS(), CPU(), GPU(), RAM().ToString() };
        }

        void Update()
        {
            speaker.SpeakerVolumeControl(volumeT);
            if (Mynput.Down(Values.Key.Mute))
            {
                speaker.MuteVolume(volumeT);
            }
            if (Mynput.Down(Values.Key.MChange))
            {
                speaker.Change(songT);
            }

            if (Mynput.Down(KeyCode.Escape))
            {
                if (!manager.menuPanel.IsActive(Active.Hierarchy))
                {
                    todayT.text = "きょうは" + Temps.Date();
                    systemT.text =
                        "すぺっく".NewLine() +
                        "OS: " + info[0].NewLine() +
                        "CPU: " + info[1].NewLine() +
                        "GPU: " + info[2].NewLine() +
                        "RAM: " + info[3] + "GB";
                    manager.OpenMenuPanel();
                    return;
                }
                manager.CloseMenuPanel();
            }
        }
    }
}
