using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Speaker : MonoBehaviour
    {
        [SerializeField]
        AudioClip bgm;

        float amount = 0.02f;

        float v = 0;
        public float V => v;

        AudioSource speaker;

        void Start()
        {
            speaker = GetComponent<AudioSource>();

            speaker.clip = bgm;

            speaker.loop = true;

            speaker.Play();
        }

        void Update()
        {
            speaker.volume = v;
        }

        public void VChange()
        {
            if (input.Down(KeyCode.UpArrow))
                v += amount;

            if (input.Down(KeyCode.DownArrow))
                v -= amount;
        }
    }
}
