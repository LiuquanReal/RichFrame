using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayCore
{
    public AudioSource audioSource;
    public AudioClip clip;


    public AudioPlayCore(GameObject gameObj, AudioClip clip, float delay)
    {
        audioSource = gameObj.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = clip;
        this.clip = clip;
        Play(delay);
    }

    public void Play(float delay = 0)
    {
        audioSource.PlayDelayed(delay);
    }
    /// <summary>
    /// 循环
    /// </summary>
    /// <param name="loop"></param>
    /// <returns></returns>
    public AudioPlayCore SetLoop(bool loop)
    {
        audioSource.loop = loop;
        return this;
    }
    /// <summary>
    /// 3D空间
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public AudioPlayCore Set3DMode(bool b)
    {
        audioSource.spatialBlend = b ? 1 : 0;
        return this;
    }
    /// <summary>
    /// 音量
    /// </summary>
    /// <param name="volume"></param>
    /// <returns></returns>
    public AudioPlayCore SetVolume(float volume)
    {
        audioSource.volume = volume;
        return this;
    }
    /// <summary>
    /// 销毁
    /// </summary>
    public void Destroy()
    {
        Object.Destroy(audioSource);
    }
}
