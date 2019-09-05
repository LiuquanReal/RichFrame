using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Dictionary<GameObject, AudioPlayCore> audios = new Dictionary<GameObject, AudioPlayCore>();

    public AudioPlayCore PlaySound(AudioClip clip, float delay = 0)
    {
        return PlaySound(gameObject, clip, delay);
    }

    public AudioPlayCore PlaySound(GameObject gameObj, AudioClip clip, float delay = 0)
    {
        AudioPlayCore core;
        if (!audios.TryGetValue(gameObj, out core))
        {
            core = new AudioPlayCore(gameObj, clip, delay);
            audios.Add(gameObj, core);
        }
        core.Play();
        return core;
    }

    public void PauseSound(GameObject gameObj)
    {
        AudioPlayCore core;
        if (!audios.TryGetValue(gameObj, out core))
        {
            return;
        }
        core.audioSource.Pause();
    }

    public void StopSound(GameObject gameObj)
    {
        AudioPlayCore core;
        if (!audios.TryGetValue(gameObj, out core))
        {
            return;
        }
        core.audioSource.Stop();
    }

    public void StopSoundAndRemove(GameObject gameObj)
    {
        AudioPlayCore core;
        if (!audios.TryGetValue(gameObj, out core))
        {
            return;
        }
        audios.Remove(gameObj);
        core.audioSource.Stop();
        core.Destroy();
    }
}
