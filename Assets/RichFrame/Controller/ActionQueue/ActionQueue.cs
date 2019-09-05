using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 方法执行队列
/// </summary>
public class ActionQueue : MonoBehaviour
{
    event Action onComplete;
    List<OneAction> actions = new List<OneAction>();
    public static ActionQueue InitOneActionQueue()
    {
        return new GameObject().AddComponent<ActionQueue>();
    }

    public ActionQueue AddAction(Action action, Func<bool> IsCompleted)
    {
        actions.Add(new OneAction(action, IsCompleted));
        return this;
    }
    /// <summary>
    /// 绑定当完成时回调函数
    /// </summary>
    /// <param name="callback">回调方法</param>
    /// <returns></returns>
    public ActionQueue BindCallback(Action callback)
    {
        onComplete += callback;
        return this;
    }

    public ActionQueue StartQueue()
    {
        StartCoroutine(StartQueueAsync());
        return this;
    }

    IEnumerator StartQueueAsync()
    {
        if (actions.Count > 0)
        {
            actions[0].action();
        }
        while (actions.Count > 0)
        {
            if (actions[0].IsCompleted())
            {
                actions.RemoveAt(0);
                if (actions.Count > 0)
                {
                    actions[0].action();
                }
                else
                {
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
        if (onComplete != null)
        {
            onComplete();
        }
        Destroy(gameObject);
    }

    class OneAction
    {
        public Action action;
        public Func<bool> IsCompleted;
        public OneAction(Action action, Func<bool> IsCompleted)
        {
            this.action = action;
            this.IsCompleted = IsCompleted;
        }
    }
}
