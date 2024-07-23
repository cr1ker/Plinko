using System;
using UnityEngine;

namespace CORE.AUDIO
{
    [Serializable]
    public struct SoundClip
    {
        [SerializeField] private AudioTypes _audioType;
        [SerializeField] private AudioSource _audioSource;

        public AudioTypes AudioType => _audioType;
        public AudioSource AudioSource => _audioSource;
    }
}