using UnityEngine;
using UnityEngine.UI;
using MyGame.Utils;
using static MyGame.Utils.Sys;

namespace MyGame
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
