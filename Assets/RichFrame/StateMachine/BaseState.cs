using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public bool IsActive
    { get; protected set; }
    public Action<BaseState> onEnter;
    public Action<BaseState> onQuit;
    public abstract void EnterState();
    public abstract void QuitState();
    public abstract bool CanSwitch(BaseState state);

}
