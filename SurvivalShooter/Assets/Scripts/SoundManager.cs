using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager m_instance;
    public static SoundManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<SoundManager>();
            return m_instance;
        }
    }

    public AudioClip bgm;
    public AudioClip[] effects;
    private Dictionary<string, AudioClip> effectsDic;
    public AudioSource bgmPlayer;
    public AudioSource effectsPlayer;
    public float bgmVolume = 1f;
    public float effectsVolume = 1f;
   // public bool soundsOn = true;

    private void Awake()
    {
        effectsDic = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in effects)
        {
            effectsDic.Add(clip.name, clip);
        }
        //if (soundsOn)
        bgmPlayer.PlayOneShot(bgm, bgmVolume);
    }

    public void PlayEffectSound(string clipName)
    {
        //if (soundsOn)
        effectsPlayer.PlayOneShot(effectsDic[clipName], effectsVolume);
    }

    public void SetBgmVolume(float volume)
    {
        bgmVolume = volume;
        bgmPlayer.volume = volume;
    }

    public void SetEffectsVolume(float volume)
    {
        effectsVolume = volume;
        effectsPlayer.volume = volume;
    }

    public void SoundsOn(bool on)
    {
        if (on)
        {
            bgmPlayer.volume = 0f;
            effectsPlayer.volume = 0f;
            return;
        }
        bgmPlayer.volume = bgmVolume;
        effectsPlayer.volume = effectsVolume;
    }
}
