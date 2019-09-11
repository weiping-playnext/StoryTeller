using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller.Presentation
{
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        public IAssetSource assetSource;
        Dictionary<string, AudioSource> soundAudioSources = new Dictionary<string, AudioSource>();
        Dictionary<string, AudioSource> bgmAudioSources = new Dictionary<string, AudioSource>();

        string currentSound;
        string currentBgm;

        public void AddSoundAudioSource(string name, string asset)
        {
            var clip = assetSource.GetAsset<AudioClip>(asset);
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            soundAudioSources[name] = audioSource;
        }

        public void AddBgmAudioSource(string name, string asset)
        {
            var clip = assetSource.GetAsset<AudioClip>(asset);
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            soundAudioSources[name] = audioSource;
        }

        public void PlaySound(string name)
        {   
            currentSound = name;
            var source = soundAudioSources[name];
            source.Play();
        }

        public void StopSound()
        {
            if (currentSound != null)
            {
                soundAudioSources[currentSound].Stop();
            }
            currentSound = null;
        }

        public void PlayBgm(string name, int fade)
        {
            Debug.LogWarning("Sorry, fade attribute doesn't work (not implemented):"+ fade);
            StopBgm(fade);

            currentBgm = name;
            var source = soundAudioSources[name];
            source.loop = true;
            source.Play();
        }

        public void StopBgm(int fade)
        {   
            Debug.LogWarning("Sorry, fade attribute doesn't work (not implemented):" + fade);
            if (!string.IsNullOrEmpty(currentBgm))
            {
                soundAudioSources[currentBgm].Stop();
            }
            currentBgm = null;
        }
    }
}

