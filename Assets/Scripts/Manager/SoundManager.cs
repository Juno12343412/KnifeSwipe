using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.Sound
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        // 오디오 클립을 저장할 변수
        public AudioClip[] clips;
        public Dictionary<string, AudioClip> clipsDictionary;

        // 오디오 플레이 변수
        AudioSource sfxPlayer;
        AudioSource bgmPlayer;

        private float sfxVolume = 1f;
        private float bgmVolume = 1f;

        void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            instance = this;
            //DontDestroyOnLoad(gameObject);
            instance = GetComponent<SoundManager>();

            sfxPlayer = GameObject.Find("EffectSound").GetComponent<AudioSource>();
            bgmPlayer = GameObject.Find("BGMSound").GetComponent<AudioSource>(); 

            clipsDictionary = new Dictionary<string, AudioClip>();
            foreach (AudioClip clip in clips)
            {
                clipsDictionary.Add(clip.name, clip);
            }
        }

        void Start()
        {         
            bgmPlayer.Play();
        }

        public void PlaySound(string clipname, float volume = 1f)
        {
            if (clipsDictionary.ContainsKey(clipname) == false)
            {
                return;
            }
            sfxPlayer.PlayOneShot(clipsDictionary[clipname], volume * sfxVolume);
        }

        public GameObject PlayLoopSound(string clipname)
        {
            if (clipsDictionary.ContainsKey(clipname) == false)
            {
                return null;
            }

            bgmPlayer.clip = clipsDictionary[clipname];
            bgmPlayer.volume = sfxVolume;
            bgmPlayer.loop = true;
            bgmPlayer.Play();

            return null;
        }

        public void StopBGM()
        {
            bgmPlayer.Stop();
        }

        public void StopSFX()
        {
            sfxPlayer.Stop();
        }

        public void SetSFX(float volume)
        {
            sfxPlayer.volume = volume;
            sfxVolume = volume;
        }

        public void SetBGM(float volume)
        {
            bgmPlayer.volume = volume;
        }
    }
}
