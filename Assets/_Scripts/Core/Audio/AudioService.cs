using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CORE.AUDIO
{
    public class AudioService : MonoBehaviour
    {
        public static AudioService Singleton;
        [SerializeField] private SoundClip[] _soundClipsData;

        private Dictionary<AudioTypes, AudioSource> _soundClips;
        private float _globalVolume;

        #region MONO

        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(Singleton.gameObject);
            }
            
            Singleton = this;

            _soundClips = new Dictionary<AudioTypes, AudioSource>();
            var soundClipsCount = _soundClipsData.Length;
            for (int i = 0; i < soundClipsCount; i++)
            {
                SoundClip soundClip = _soundClipsData[i];
                _soundClips.TryAdd(soundClip.AudioType, soundClip.AudioSource);
            }

            _soundClipsData = null;
        }

        #endregion

        public void PlayAudioOnce(AudioTypes audioType, float minPitch = 1f, float maxPitch = 1f)
        {
            var isSuccess = _soundClips.TryGetValue(audioType, out AudioSource targetAudio);
            if (!isSuccess)
            {
                return;
            }
            
            if (minPitch < maxPitch)
            {
                targetAudio.pitch = Random.Range(minPitch, maxPitch);
            }
            targetAudio.Play();

            if (!targetAudio.isPlaying)
            {
            }
        }

        public void PlayAudioLong(AudioTypes audioType, float minPitch = 1f, float maxPitch = 1f)
        {
            var isSuccess = _soundClips.TryGetValue(audioType, out AudioSource targetAudio);
            if (!isSuccess)
            {
                return;
            }
            
            if (minPitch < maxPitch)
            {
                targetAudio.pitch = Random.Range(minPitch, maxPitch);
            }

            targetAudio.loop = true;
            targetAudio.Play();
        }

        public void StopAudio(AudioTypes audioType)
        {
            var isSuccess = _soundClips.TryGetValue(audioType, out AudioSource targetAudio);
            if (!isSuccess)
            {
                return;
            }
            
            targetAudio.Stop();
        }

        public void SetMuteStatus(bool isMuted)
        {
            foreach (var soundClip in _soundClips)
            {
                soundClip.Value.mute = isMuted;
            }
        }

        private void UpdateVolume()
        {
            foreach (var soundClip in _soundClips)
            {
                soundClip.Value.volume = _globalVolume;
            }
        }

        private async void DecreaseVolume(AudioTypes audioType, bool predicate)
        {
            var isSuccess = _soundClips.TryGetValue(audioType, out AudioSource targetAudio);
            if (!isSuccess)
            {
                return;
            }

            targetAudio.volume /= 2;

            await UniTask.WaitUntil(() => predicate);
            
            targetAudio.volume *= 2;
        }
    }
}
