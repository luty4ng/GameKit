using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit;
namespace GameKit
{
    public class AudioManager : SingletonBase<AudioManager>
    {
        private AudioSource BGM = null;
        public float BGMVolume = 1;
        public float soundVolume = 1;
        private GameObject soundObj = null;
        private List<AudioSource> soundList = new List<AudioSource>();

        public AudioManager()
        {
            MonoManager.instance.AddUpdateListener(Update);
        }

        private void Update()
        {
            for (int i = soundList.Count - 1; i >= 0; --i)
            {
                if (!soundList[i].isPlaying)
                {
                    this.StopSound(soundList[i]);
                }
            }
        }
        public void PlayBGM(string name)
        {
            if (BGM == null)
            {
                GameObject obj = new GameObject();
                obj.name = "BGM";
                BGM = obj.AddComponent<AudioSource>();
            }

            ResourceManager.instance.LoadAsync<AudioClip>("Audio/BGM/" + name, (clip) =>
            {
                BGM.clip = clip;
                BGM.loop = true;
                BGM.volume = BGMVolume;
                BGM.Play();
            });
        }

        public void ChangeBGMVolume(float v)
        {
            BGMVolume = v;
            if (BGM == null)
                return;
            BGM.volume = BGMVolume;
        }
        public void PauseBGM()
        {
            if (BGM == null)
                return;
            BGM.Pause();
        }
        public void StopBGM()
        {
            if (BGM == null)
                return;
            BGM.Stop();
        }

        public void PlaySound(string name, UnityAction<AudioSource> callback)
        {
            if (soundObj == null)
            {
                soundObj = new GameObject();
                soundObj.name = "Sound";
            }
            ResourceManager.instance.LoadAsync<AudioClip>("Audio/Sound/" + name, (clip) =>
            {
                AudioSource source = soundObj.AddComponent<AudioSource>();
                source.clip = clip;
                source.volume = soundVolume;
                source.Play();
                soundList.Add(source);
                if (callback != null)
                    callback(source);
            });
        }

        public void ChangeSoundVolume(float v)
        {
            soundVolume = v;
            for (int i = 0; i < soundList.Count; ++i)
                soundList[i].volume = v;
        }
        public void StopSound(AudioSource source)
        {
            if (soundList.Contains(source))
            {
                soundList.Remove(source);
                source.Stop();
                GameObject.Destroy(source);
            }
        }

        public List<AudioSource> GetSoundList()
        {
            return soundList;
        }
    }

}
