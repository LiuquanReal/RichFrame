using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 事件监听容器
/// </summary>
public class ListenerWrap
{
    public string name;
    public bool IsEmpty
    { get; protected set; }
    /// <summary>
    /// 所有监听方法
    /// </summary>
    List<EventAction> listenerList;
    public ListenerWrap(string name,EventAction action)
    {
        listenerList = new List<EventAction>();
        IsEmpty = true;
        if (action != null)
        {
            listenerList.Add(action);
            IsEmpty = false;
        }
    }
    /// <summary>
    /// 执行监听
    /// </summary>
    /// <param name="args"></param>
    public void Execute(params object[] args)
    {
        foreach (var item in listenerList)
        {
            item(args);
        }
    }
    /// <summary>
    /// 添加一个监听方法
    /// </summary>
    /// <param name="action"></param>
    public void Add(EventAction action)
    {
        if (action != null)
        {
            if (!listenerList.Contains(action))
            {
                listenerList.Add(action);
            }
            IsEmpty = false;
        }
    }
    /// <summary>
    /// 移除一个监听方法
    /// </summary>
    /// <param name="action"></param>
    public void Remove(EventAction action)
    {
        if (!listenerList.Contains(action))
        {
            listenerList.Remove(action);
        }
        if (listenerList.Count <= 0 )
        {
            IsEmpty = true;
        }
    }
}
