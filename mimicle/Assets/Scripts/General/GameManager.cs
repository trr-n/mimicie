using UnityEngine;
using MyGame.Utils;

namespace MyGame
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField]
        int startWave = 0;

        [SerializeField]
        EnemySpawner spawner;

        [SerializeField]
        Scroll scroll;

        [SerializeField]
        WaveData wdata;

        public GameObject menuPanel;

        public bool Ctrlable { get; set; }
        public bool BGScroll { get; set; }
        public bool IsOpeningMenu { get; set; }
        public bool IsDead { get; set; }

        void Start()
        {
            spawner.ActivateWave(startWave);
            Ctrlable = true;
            BGScroll = true;
            IsDead = false;
            Physics2D.gravity = Vector3.forward * 9.81f;
        }

        void Update()
        {
            if (Mynput.Pressed(Values.Key.Stop))
            {
                Time.timeScale = 0;
                Ctrlable = false;
                BGScroll = false;
            }

            else if (Mynput.Released(Values.Key.Stop))
            {
                Time.timeScale = 1;
                Ctrlable = true;
                BGScroll = true;
            }
        }

        public void PlayerIsDead()
        {
            Ctrlable = false;
            BGScroll = false;
            Time.timeScale = 0;

            Score.StopTimer();
            ScoreData data = new ScoreData
            {
                wave = wdata.Now,
                time = Score.Time,
                score = Score.Now
            };
            Save.Write(data, "goigoisu-", Application.dataPath + "/score.sav");
        }

        public void OpenMenuPanel()
        {
            menuPanel.SetActive(true);
            Score.StopTimer();
            Ctrlable = false;
            BGScroll = false;
            IsOpeningMenu = true;
            Time.timeScale = 0;
        }

        public void CloseMenuPanel()
        {
            menuPanel.SetActive(false);
            Score.StartTimer();
            Ctrlable = true;
            BGScroll = true;
            IsOpeningMenu = false;
            Time.timeScale = 1;
        }
    }
}
