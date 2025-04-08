using System;
using System.Collections.Generic;
using UnityEngine;

namespace CWLib
{
    public class SoundManager
    {
        private readonly AudioSource[] _audioSources = new AudioSource[(int)BaseDefine.Sound.Max];
        private readonly Dictionary<string, AudioClip> _audioClips = new();

        private GameObject _soundRoot;

        public void Init()
        {
            if (_soundRoot == null)
            {
                _soundRoot = GameObject.Find("@SoundRoot");
                if (_soundRoot == null)
                {
                    _soundRoot = new GameObject { name = "@SoundRoot" };

                    var soundTypeNames = Enum.GetNames(typeof(BaseDefine.Sound));
                    for (var count = 0; count < soundTypeNames.Length - 1; count++)
                    {
                        var go = new GameObject { name = soundTypeNames[count] };
                        _audioSources[count] = go.AddComponent<AudioSource>();
                        go.transform.parent = _soundRoot.transform;
                    }

                    _audioSources[(int)BaseDefine.Sound.Bgm].loop = true;
                    _audioSources[(int)BaseDefine.Sound.SubBgm].loop = true;
                }
            }
        }

        public void Clear()
        {
            foreach (var audioSource in _audioSources)
                audioSource.Stop();
            _audioClips.Clear();
        }

        public void Play(BaseDefine.Sound type, string key, float pitch = 1.0f, float volume = 1.0f)
        {
            var audioSource = _audioSources[(int)type];

            LoadAudioClip(key, audioClip =>
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.volume = volume;
                audioSource.clip = audioClip;
                audioSource.Play();
            });
        }

        public void Stop(BaseDefine.Sound type)
        {
            var audioSource = _audioSources[(int)type];
            audioSource.Stop();
        }

        private void LoadAudioClip(string key, Action<AudioClip> callback)
        {
            AudioClip audioClip = null;
            if (_audioClips.TryGetValue(key, out audioClip))
            {
                callback?.Invoke(audioClip);
                return;
            }

            audioClip = Managers.Resource.Load<AudioClip>(key);

            if (!_audioClips.ContainsKey(key))
                _audioClips.Add(key, audioClip);

            callback?.Invoke(audioClip);
        }
    }
}