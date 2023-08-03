using UnityEngine;
using UnityEngine.UI;
using Self.Utils;
using static Self.Utils.Sys;

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

            if (Inputs.Down(Constant.Key.Mute))
            {
                speaker.MuteVolume(volumeT);
            }

            if (Inputs.Down(Constant.Key.MChange))
            {
                speaker.Change(songT);
            }

            if (Inputs.Down(KeyCode.Escape))
            {
                if (manager.menuPanel.IsActive(Active.Hierarchy))
                {
                    manager.CloseMenuPanel();
                    return;
                }

                todayT.text = "きょうは" + Temps.Date();
                systemT.text =
                    "すぺっく" +
                    "\nOS: " + OS +
                    "\nCPU: " + CPU +
                    "\nGPU: " + GPU + " " + VRAM + "GB" +
                    "\nRAM: " + RAM + "GB";
                manager.OpenMenuPanel();

            }
        }
    }
}
