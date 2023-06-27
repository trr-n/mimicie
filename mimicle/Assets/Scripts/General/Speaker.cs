using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
{
    public class Speaker : MonoBehaviour
    {
        [SerializeField]
        AudioClip[] musics;

        AudioSource speaker;
        const float amount = 0.02f;
        [Range(0, 1f)] float _volume = 0.5f;
        float preVolume = 0f;
        int playingIndex = 0;
        int pressCounter = 0;

        void Start()
        {
            speaker = GetComponent<AudioSource>();
            playingIndex = Rnd.ice(musics);
            speaker.clip = musics[playingIndex];
            speaker.loop = true;
            speaker.Play();
        }

        string VText(float _percentage) => $"おんりょう{_percentage}%";

        public void SpeakerVolumeControl(Text volumeT)
        {
            if (Mynput.Down(Values.Key.VUp))
                _volume += amount;
            else if (Mynput.Down(Values.Key.VDown))
                _volume -= amount;

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
                playingIndex = 0;
            speaker.clip = musics[playingIndex];
            speaker.Play();
            songT.text = musics[playingIndex].name;
        }
    }
}
