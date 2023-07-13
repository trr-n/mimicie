using UnityEngine;
using UnionEngine.Extend;

namespace UnionEngine
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField]
        int startWave = 0;
        [SerializeField]
        EnemySpawner spawner;
        public GameObject menuPanel;
        [SerializeField]
        Scroll scroll;
        [SerializeField]
        WaveData wdata;

        public bool Ctrlable { get; set; }
        public bool BGScroll { get; set; }
        public bool IsOpeningMenu { get; set; }
        public bool IsDead { get; set; }
        public static ScoreAWave[] wave = new ScoreAWave[3];

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
            Score.StopTimerFinal();
            Time.timeScale = 0;
        }

        public void OpenMenu()
        {
            menuPanel.SetActive(true);
            Score.StopTimer();
            Ctrlable = false;
            BGScroll = false;
            IsOpeningMenu = true;
            Time.timeScale = 0;
        }

        public void CloseMenu()
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
