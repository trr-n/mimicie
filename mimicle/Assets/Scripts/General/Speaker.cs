using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public class Speaker : MonoBehaviour
    {
        [SerializeField]
        AudioClip[] musics;

        AudioSource speaker;

        /// <summary>
        /// 音量増加量
        /// </summary>
        const float amount = 0.02f;

        float _volume = 0.5f; // 0-1
        float preVolume = 0f;
        int playingIndex = 0;
        int pressCounter = 0;

        string title;
        public string Title => title;

        void Awake()
        {
            speaker = GetComponent<AudioSource>();
            playingIndex = Rnd.Choice(musics);
            speaker.clip = musics[playingIndex];
            speaker.loop = true;
            speaker.Play();

            title = speaker.clip.name;
        }

        string VText(float _percentage) => $"おんりょう{_percentage}%";

        public void SpeakerVolumeControl(Text volumeT)
        {
            if (Inputs.Down(Constant.VUp))
            {
                _volume += amount;
            }

            else if (Inputs.Down(Constant.VDown))
            {
                _volume -= amount;
            }

            speaker.volume = _volume;
            volumeT.text = VText(Numeric.Percent(_volume));
        }

        public void MuteVolume(Text volumeT)
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

            volumeT.text = VText(Numeric.Percent(_volume));
        }

        public void Change(Text songT)
        {
            playingIndex++;

            if (playingIndex >= musics.Length)
            {
                playingIndex = 0;
            }

            speaker.clip = musics[playingIndex];
            speaker.Play();

            songT.text = musics[playingIndex].name;
        }
    }
}
