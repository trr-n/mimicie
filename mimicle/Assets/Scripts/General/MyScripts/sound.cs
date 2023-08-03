using UnityEngine;

namespace Self.Utils
{
    public static class Sound
    {
        public static void Play(this AudioSource source, AudioClip clip)
        {
            source.PlayOneShot(clip);
        }
    }
}