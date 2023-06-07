using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;
using static Mimical.GameManager.Key;

namespace Mimical
{
    public class Speaker : MonoBehaviour
    {
        [SerializeField]
        AudioClip[] musics;

        float amount = 0.02f;

        float _volume = 0.4f;

        float preVolume = 0f;

        int pressCounter = 0;

        string nowTitle = "";

        int nowIndex = 0;

        AudioSource speaker;

        void Start()
        {
            speaker = GetComponent<AudioSource>();

            nowIndex = random.ice(musics);

            speaker.clip = musics[nowIndex];

            speaker.loop = true;

            speaker.Play();
        }

        void _Update()
        {
        }

        string VText(float _percentage) => $"おんりょう{_percentage}%";

        public void VCtrl(Text volumeT)
        {
            if (input.Down(VUp))
            {
                _volume += amount;

                "vup".show();
            }

            else if (input.Down(VDown))
            {
                _volume -= amount;

                "vdown".show();
            }

            speaker.volume = _volume;

            volumeT.text = VText(numeric.Percent(_volume));
        }

        public void VMute(Text volumeT)
        {
            if (pressCounter == 0)
            {
                pressCounter++;

                preVolume = _volume;

                _volume = 0;
            }

            else if (pressCounter == 1)
            {
                pressCounter = 0;

                _volume = preVolume;
            }

            volumeT.text = VText(numeric.Percent(_volume));
        }

        public void Change(Text songT)
        {
            nowIndex++;

            if (nowIndex >= musics.Length)
            {
                nowIndex = 0;
            }

            speaker.clip = musics[nowIndex];

            speaker.Play();

            songT.text = musics[nowIndex].name;
        }
    }
}
