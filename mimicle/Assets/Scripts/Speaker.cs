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
        AudioClip bgm;

        float amount = 0.02f;

        float _volume = 0.4f;

        float preVolume = 0f;

        int pressCounter = 0;

        AudioSource speaker;

        void Start()
        {
            speaker = GetComponent<AudioSource>();

            speaker.clip = bgm;

            speaker.loop = true;

            speaker.Play();
        }

        void Update() => speaker.volume = _volume;

        string VText(float _percentage) => $"おんりょう{_percentage}%";

        public void VChange(Text text)
        {
            if (input.Down(KeyCode.UpArrow))
                _volume += amount;

            else if (input.Down(KeyCode.DownArrow))
                _volume -= amount;

            text.text = VText(numeric.Percent(_volume));
        }

        public void VMute(Text text)
        {
            pressCounter.show();

            if (input.Down(Mute))
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

                text.text = VText(numeric.Percent(_volume));
            }
        }
    }
}
