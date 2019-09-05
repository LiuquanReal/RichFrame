using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自定义动画
/// </summary>
public abstract class CustomAnimation : MonoBehaviour
{
    public bool playOnStart;
    public float playDelay;
    public float duration;

    protected virtual void Start()
    {
        if (playOnStart)
        {
            StartAnimation();
        }
    }

    public virtual void StartAnimation()
    {
        StartAnimation(0);
    }

    public virtual void StartAnimation(float delay)
    {
        playDelay = delay;
        if (playDelay > 0)
        {
            Invoke("Anim", playDelay);
        }
        else
        {
            Animation();
        }
    }

    protected abstract void Animation();
}
