using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 对象池
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> where T : Object
{
    List<T> activedObjects = new List<T>();
    List<T> objectsPool = new List<T>();
    T prefab;
    UnityAction<T> onTakeOutOneObject;
    UnityAction<T> onPutInOneObject;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefab">用于实例化的预设体</param>
    /// <param name="onTakeOutOneObj">拿出一个实例的回调</param>
    /// <param name="onPutInOneObj">放入一个实例的回调</param>
    public ObjectPool(T prefab, UnityAction<T> onTakeOutOneObj = null, UnityAction<T> onPutInOneObj = null)
    {
        this.prefab = prefab;
        this.onTakeOutOneObject = onTakeOutOneObj;
        this.onPutInOneObject = onPutInOneObj;
    }
    /// <summary>
    /// 拿出一个对象
    /// </summary>
    /// <returns></returns>
    public T TakeOutOneObject()
    {
        return TakeOutOneObject(null);
    }
    /// <summary>
    /// 拿出一个对象并设置父物体
    /// </summary>
    /// <param name="parent">父物体</param>
    /// <returns></returns>
    public T TakeOutOneObject(Transform parent)
    {
        T obj;
        if (objectsPool.Count > 0)
        {
            obj = objectsPool[0];
            objectsPool.RemoveAt(0);
        }
        else
        {
            obj = InstantiateObject(parent);
        }
        activedObjects.Add(obj);
        if (onTakeOutOneObject != null)
        {
            onTakeOutOneObject(obj);
        }
        return obj;
    }
    public void PutInObject(T obj)
    {
        if (activedObjects.Contains(obj))
        {
            activedObjects.Remove(obj);
            objectsPool.Add(obj);
            if (onPutInOneObject != null)
            {
                onPutInOneObject(obj);
            }
        }
    }
    public void ClearAllActivedObjects()
    {
        while (activedObjects.Count > 0)
        {
            PutInObject(activedObjects[0]);
        }
    }
    public T[] GetActivedObjects()
    {
        return activedObjects.ToArray();
    }
    T InstantiateObject(Transform parent)
    {
        if (parent != null)
        {
            return Object.Instantiate(prefab, parent);
        }
        return Object.Instantiate(prefab);
    }
}
