using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateController<T> : MonoBehaviour where T : BaseState
{
    public T currentState;
    public abstract void StartState(T state);
    public abstract void OnStateQuit(T state);
}
