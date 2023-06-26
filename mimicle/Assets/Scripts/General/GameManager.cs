using UnityEngine;
using UnityEngine.UI;
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
        Text debugT;
        [SerializeField]
        Scroll scroll;
        [SerializeField]
        Slain slain;
        [SerializeField]
        WaveData wdata;

        public bool PlayerCtrlable { get; set; }
        public bool BackGroundScrollable { get; set; }
        public bool IsOpeningMenu { get; set; }

        void Start()
        {
            spawner.ActivateWave(startWave);
            PlayerCtrlable = true;
            BackGroundScrollable = true;
            Physics2D.gravity = Vector3.forward * 9.81f;
        }

        void Update()
        {
            debugT.text = "slain count: " + slain.Count;
            if (Mynput.Pressed(Values.Key.Stop))
            {
                Time.timeScale = 0;
                PlayerCtrlable = false;
                BackGroundScrollable = false;
            }
            else if (Mynput.Released(Values.Key.Stop))
            {
                Time.timeScale = 1;
                PlayerCtrlable = true;
                BackGroundScrollable = true;
            }
        }

        public void Pause()
        {
            menuPanel.SetActive(true);
            PlayerCtrlable = false;
            BackGroundScrollable = false;
            IsOpeningMenu = true;
            Time.timeScale = 0;
        }

        public void Restart()
        {
            menuPanel.SetActive(false);
            PlayerCtrlable = true;
            BackGroundScrollable = true;
            IsOpeningMenu = false;
            Time.timeScale = 1;
        }
    }
}
