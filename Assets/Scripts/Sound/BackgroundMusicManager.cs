using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    [Serializable]
    public struct MusicOverride
    {
        public string Scene;
        public AudioClip AudioClip;
        public float Volume;
    }

    public class BackgroundMusicManager : SingletonMonoBehaviour<BackgroundMusicManager>
    {
        public AudioSource AudioSource;
        public AudioClip DefaultMusic;
        public float DefaultVolume;

        public MusicOverride[] MusicOverrideArray;
        private Dictionary<string, MusicOverride> p_musicOverrideDictionary;
        private Dictionary<string, MusicOverride> _musicOverratedDictionary
        {
            get
            {
                if (p_musicOverrideDictionary == null)
                {
                    p_musicOverrideDictionary = new Dictionary<string, MusicOverride>();
                    if (MusicOverrideArray != null)
                    {
                        foreach (MusicOverride data in MusicOverrideArray)
                        {
                            p_musicOverrideDictionary[data.Scene] = data;
                        }
                    }
                }

                return p_musicOverrideDictionary;
            }
        }

        private void Start()
        {
            AudioSource.Play();
        }

        public void SetMusic(AudioClip clip, float volume)
        {

            AudioSource.Stop();
            AudioSource.clip = clip;
            AudioSource.volume = volume;
            AudioSource.Play();
        }

        public void CheckOverride(string scene)
        {
            if (_musicOverratedDictionary.ContainsKey(scene))
            {
                MusicOverride audioClipData = _musicOverratedDictionary[scene];
                SetMusic(audioClipData.AudioClip, audioClipData.Volume);
            }
            else if (AudioSource.clip != DefaultMusic)
            {
                SetMusic(DefaultMusic, DefaultVolume);
            }
        }
    }
}
