using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件管理器
/// </summary>
public class EventManager : Singleton<EventManager>
{
    public Dictionary<string, ListenerWrap> eventsDic = new Dictionary<string, ListenerWrap>();
    
    /// <summary>
    /// 注册一个事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="listener">监听方法</param>
    public void RegisterEvent(string eventName,EventAction listener)
    {
        if (listener == null)
            return;
        ListenerWrap listenerWrp;
        if (eventsDic.TryGetValue(eventName,out listenerWrp))
        {
            listenerWrp.Add(listener);
        }
        else
        {
            listenerWrp = new ListenerWrap(eventName, listener);
            eventsDic.Add(eventName, listenerWrp);
        }
    }
    /// <summary>
    /// 移除一个消息
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void RemoveEvent(string eventName,EventAction listener)
    {
        if (listener == null)
            return;
        ListenerWrap listenerWrp;
        if (eventsDic.TryGetValue(eventName, out listenerWrp))
        {
            listenerWrp.Remove(listener);
            if (listenerWrp.IsEmpty)
                eventsDic.Remove(eventName);
        }
    }

    /// <summary>
    /// 执行一个事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="args">参数</param>
    public void ExecuteEvent(string eventName,params object[] args)
    {
        ListenerWrap listenerWrp;
        if (eventsDic.TryGetValue(eventName, out listenerWrp))
        {
            listenerWrp.Execute(args);
        }
    }
}

public delegate void EventAction(object[] args);
