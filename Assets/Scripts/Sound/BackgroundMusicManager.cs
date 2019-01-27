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
    }

    public class BackgroundMusicManager : SingletonMonoBehaviour<BackgroundMusicManager>
    {
        public AudioSource AudioSource;
        public AudioClip DefaultMusic;

        public MusicOverride[] MusicOverrideArray;
        private Dictionary<string, AudioClip> p_musicOverrideDictionary;
        private Dictionary<string, AudioClip> _musicOverratedDictionary
        {
            get
            {
                if (p_musicOverrideDictionary == null)
                {
                    p_musicOverrideDictionary = new Dictionary<string, AudioClip>();
                    if (MusicOverrideArray != null)
                    {
                        foreach (MusicOverride data in MusicOverrideArray)
                        {
                            p_musicOverrideDictionary[data.Scene] = data.AudioClip;
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

        public void CheckOverride(string scene)
        {
            if (_musicOverratedDictionary.ContainsKey(scene))
            {
                AudioClip audioClip = _musicOverratedDictionary[scene];
                AudioSource.Stop();
                AudioSource.clip = audioClip;
                AudioSource.Play();
            }
            else if (AudioSource.clip != DefaultMusic)
            {
                AudioSource.Stop();
                AudioSource.clip = DefaultMusic;
                AudioSource.Play();
            }
        }
    }
}
