using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public class Speaker : MonoBehaviour
    {
        [SerializeField]
        AudioClip bgm;

        new AudioSource audio;

        void Start()
        {
            audio = GetComponent<AudioSource>();
            audio.clip = bgm;
            audio.Play();
        }
    }
}
