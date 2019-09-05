using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgressControl
{
    public string name;
    public string description;
    public float Progress { get; protected set; }
    public bool IsCompleted { get; protected set; }
    protected UnityAction startAction;
    public ProgressControl()
    {
    }
    public ProgressControl(string name,string description, UnityAction startAction)
    {
        this.startAction = startAction;
        this.name = name;
        this.description = description;
    }

    public virtual void StartProgress()
    {
        startAction();
    }

    public virtual void Pause()
    {

    }

    protected void SetProgress(float p)
    {
        Progress = p;
        if (Progress >= 1)
        {
            IsCompleted = true;
        }
    }
}
