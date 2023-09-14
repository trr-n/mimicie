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

        /// <summary>
        /// volumeのあたい 0-1
        /// </summary>
        float _volume = 0.5f;

        /// <summary>
        /// ミュート前の音量保存用
        /// </summary>
        float preVolume = 0f;

        /// <summary>
        /// 流している音楽の番号
        /// </summary>
        int playingIndex = 0;

        /// <summary>
        /// ぼたんおしたかいすう
        /// </summary>
        int pressCounter = 0;

        string title;
        /// <summary>
        /// 曲名
        /// </summary>
        public string Title => title;

        void Awake()
        {
            speaker = GetComponent<AudioSource>();
            speaker.clip = musics[playingIndex = Rand.Choice(musics)];
            speaker.loop = true;
            speaker.Play();

            title = speaker.clip.name;
        }

        /// <summary>
        /// 音量表示用テキスト
        /// </summary>
        string VText(float _percentage) => $"おんりょう{_percentage}%";

        /// <summary>
        /// 音量調整
        /// </summary>
        public void SpeakerVolumeControl(Text volumeT)
        {
            if (Inputs.Down(Constant.VUp)) { _volume += amount; }
            else if (Inputs.Down(Constant.VDown)) { _volume -= amount; }

            speaker.volume = _volume;
            volumeT.text = VText(Numeric.Percent(_volume));
        }

        /// <summary>
        /// ミュートする
        /// </summary>
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

        /// <summary>
        /// 曲変更
        /// </summary>
        public void Change(Text songT)
        {
            playingIndex++;

            if (playingIndex >= musics.Length) { playingIndex = 0; }

            speaker.clip = musics[playingIndex];
            speaker.Play();

            songT.text = musics[playingIndex].name;
        }
    }
}
