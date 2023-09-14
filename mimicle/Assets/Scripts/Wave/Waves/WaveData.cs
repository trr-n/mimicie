using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public enum Activate { First, Second, Third }

    public class WaveData : MonoBehaviour
    {
        /// <summary>
        /// 開始ウェーブ
        /// </summary>
        public Activate waves = Activate.First;

        [SerializeField]
        GameObject[] waveObjs;

        [SerializeField]
        WaveUI waveUI;

        int max = 0;
        /// <summary>
        ///  最大ウェーブ数
        /// </summary>
        public int Max => max;

        int current4ui = 0;
        /// <summary>
        /// アクティブウェーブ(UI表示用)
        /// </summary>
        public int Current4UI => current4ui;

        /// <summary>
        /// アクティブウェーブ
        /// </summary>
        public int CurrentActive => current4ui - 1;

        /// <summary>
        /// ゲームクリアしたらtrue
        /// </summary>
        public bool IsDone { get; set; }

        /// <summary>
        /// アクティブウェーブとwaveが同じならtrue
        /// </summary>
        public bool IsActiveWave(int wave) => wave == CurrentActive;

        void Start()
        {
            max = waveObjs.Length;
            current4ui = (int)waves;

            ActivateWave((int)waves);
            waveUI.Start();
        }

        /// <summary>
        /// 次のウェーブに進む
        /// </summary>
        public void Next()
        {
            current4ui = ((int)waves) + 1;
            waveObjs[current4ui - 1].SetActive(true);

            for (int index = 0; index < waveObjs.Length; index++)
            {
                if (current4ui - 1 != index)
                {
                    waveObjs[index].SetActive(false);
                }
            }
        }

        /// <summary>
        /// ウェーブnをアクティブにする
        /// </summary>
        /// <param name="n"></param>
        public void ActivateWave(int n)
        {
            waveObjs[n].SetActive(true);
            current4ui = n + 1;
            waveUI.UpdateUI();

            for (ushort index = 0; index < waveObjs.Length; index++)
            {
                if (index != n)
                {
                    waveObjs[index].SetActive(false);
                }
            }
        }
    }
}
