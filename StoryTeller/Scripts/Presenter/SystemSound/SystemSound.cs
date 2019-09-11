using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller.Presentation

{
    public class SystemSound : MonoBehaviour, ISystemSound
    {
        [SerializeField] AudioSource clickSoundSource = null;

        public void PlayClickSound()
        {
            clickSoundSource.Play();
        }
    }
}
