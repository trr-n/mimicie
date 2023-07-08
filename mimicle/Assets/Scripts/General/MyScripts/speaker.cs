using UnityEngine;

namespace Mimical.Extend
{
    public static class Speaker
    {
        public static void Play(this AudioSource audio, AudioClip clip) { audio.clip = clip; audio.Play(); }
        public static void RandomPlay(this AudioSource audio, AudioClip[] clips) { audio.clip = Rnd.Choice3(clips); audio.Play(); }
    }
}