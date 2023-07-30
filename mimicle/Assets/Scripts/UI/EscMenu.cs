using UnityEngine;
using UnityEngine.UI;
using Self.Utility;
using static Self.Utility.Sys;

namespace Self
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

        void Update()
        {
            speaker.SpeakerVolumeControl(volumeT);

            if (Feed.Down(Values.Key.Mute))
            {
                speaker.MuteVolume(volumeT);
            }

            if (Feed.Down(Values.Key.MChange))
            {
                speaker.Change(songT);
            }

            if (Feed.Down(KeyCode.Escape))
            {
                if (!manager.menuPanel.IsActive(Active.Hierarchy))
                {
                    todayT.text = "きょうは" + Temps.Date();
                    systemT.text =
                        "すぺっく" +
                        "\nOS: " + OS +
                        "\nCPU: " + CPU +
                        "\nGPU: " + GPU + " " + VRAM + "GB" +
                        "\nRAM: " + RAM + "GB";
                    manager.OpenMenuPanel();
                    return;
                }
                manager.CloseMenuPanel();
            }
        }
    }
}
