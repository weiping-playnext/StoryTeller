using UnityEngine;

namespace StoryTeller
{
    public interface IAudioPlayer
    {
        void AddSoundAudioSource(string name, string asset);
        void AddBgmAudioSource(string name, string asset);
        void PlaySound(string name);
        void StopSound();
        void PlayBgm(string name, int fade);
        void StopBgm(int fade);
    }
}
