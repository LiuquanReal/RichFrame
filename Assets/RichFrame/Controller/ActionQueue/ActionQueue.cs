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
    /// <summary>
    /// 添加一个方法到队列
    /// </summary>
    /// <param name="startAction">开始时执行的方法</param>
    /// <param name="IsCompleted">判断该节点是否完成</param>
    /// <returns></returns>
    public ActionQueue AddAction(Action startAction, Func<bool> IsCompleted)
    {
        actions.Add(new OneAction(startAction, IsCompleted));
        return this;
    }
    /// <summary>
    /// 添加一个协程方法到队列
    /// </summary>
    /// <param name="enumerator">一个协程</param>
    /// <returns></returns>
    public ActionQueue AddAction(IEnumerator enumerator)
    {
        actions.Add(new OneAction(enumerator));
        return this;
    }
    /// <summary>
    /// 添加一个方法到队列
    /// </summary>
    /// <param name="action">一个方法</param>
    /// <returns></returns>
    public ActionQueue AddAction(Action action)
    {
        actions.Add(new OneAction(action));
        return this;
    }

    /// <summary>
    /// 绑定执行完毕回调
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    public ActionQueue BindCallback(Action callback)
    {
        onComplete += callback;
        return this;
    }
    /// <summary>
    /// 开始执行队列
    /// </summary>
    /// <returns></returns>
    public ActionQueue StartQueue()
    {
        StartCoroutine(StartQueueAsync());
        return this;
    }

    IEnumerator StartQueueAsync()
    {
        if (actions.Count > 0)
        {
            if (actions[0].startAction != null)
            {
                actions[0].startAction();
            }
        }
        while (actions.Count > 0)
        {
            yield return actions[0].enumerator;
            actions.RemoveAt(0);
            if (actions.Count > 0)
            {
                if (actions[0].startAction != null)
                {
                    actions[0].startAction();
                }
            }
            else
            {
                break;
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
        public Action startAction;
        public IEnumerator enumerator;
        public OneAction(Action startAction, Func<bool> IsCompleted)
        {
            this.startAction = startAction;
            //如果没用协程，自己创建一个协程
            enumerator = new CustomEnumerator(IsCompleted);
        }

        public OneAction(IEnumerator enumerator, Action action = null)
        {
            this.startAction = action;
            this.enumerator = enumerator;
        }

        public OneAction(Action action)
        {
            this.startAction = action;
            this.enumerator = null;
        }

        /// <summary>
        /// 自定义的协程
        /// </summary>
        class CustomEnumerator : IEnumerator
        {
            public object Current => null;
            Func<bool> IsCompleted;
            public CustomEnumerator(Func<bool> IsCompleted)
            {
                this.IsCompleted = IsCompleted;
            }
            public bool MoveNext()
            {
                return !IsCompleted();
            }

            public void Reset()
            {
            }
        }
    }
}
