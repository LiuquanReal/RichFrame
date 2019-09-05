using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> where T : MonoBehaviour
{
    List<T> activedObjects = new List<T>();
    List<T> objectsPool = new List<T>();
    T prefab;
    public ObjectPool(T prefab)
    {
        this.prefab = prefab;
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
        obj.gameObject.SetActive(true);
        activedObjects.Add(obj);
        return obj;
    }
    public void PutInObject(T obj)
    {
        if (activedObjects.Contains(obj))
        {
            activedObjects.Remove(obj);
            obj.gameObject.SetActive(false);
            objectsPool.Add(obj);
        }
    }
    public void ClearAllActivedObjects()
    {
        foreach (var item in activedObjects)
        {
            item.gameObject.SetActive(false);
        }
        objectsPool.AddRange(activedObjects);
        activedObjects.Clear();
    }
    public List<T> GetActivedObjects()
    {
        return activedObjects;
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
