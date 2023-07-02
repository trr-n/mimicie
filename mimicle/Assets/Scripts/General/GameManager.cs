using UnityEngine;
using Mimical.Extend;

namespace Mimical
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
        Slain slain;
        [SerializeField]
        WaveData wdata;

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
            // TODO bg fading
        }

        public void OpenMenu()
        {
            menuPanel.SetActive(true);
            Ctrlable = false;
            BGScroll = false;
            IsOpeningMenu = true;
            Time.timeScale = 0;
        }

        public void CloseMenu()
        {
            menuPanel.SetActive(false);
            Ctrlable = true;
            BGScroll = true;
            IsOpeningMenu = false;
            Time.timeScale = 1;
        }
    }
}
