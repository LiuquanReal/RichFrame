using System.Collections;
using UnityEngine;

public class ActionQueueTest : MonoBehaviour
{
    void Start()
    {
        ActionQueue.InitOneActionQueue().
            AddAction(CheckResources()).
            AddAction(DownloadResources()).
            AddAction(LoadGameObjects, () => loadCompleted).
            AddAction(Initialize).
            BindCallback(StartGame).
            StartQueue();
    }

    public IEnumerator CheckResources()
    {
        Debug.Log("开始检查资源...");
        yield return new WaitForSeconds(1);
        Debug.Log("检查资源完毕！");
    }

    IEnumerator DownloadResources()
    {
        Debug.Log("开始下载资源...");
        yield return new WaitForSeconds(1);
        Debug.Log("资源下载完毕！");
    }
    bool loadCompleted = false;
    void LoadGameObjects()
    {
        Debug.Log("加载游戏物体...");
        Invoke("LoadCompleted", 1);
    }

    void LoadCompleted()
    {
        loadCompleted = true;
    }

    void Initialize()
    {
        Debug.Log("初始化");
    }

    void StartGame()
    {
        Debug.Log("开始游戏！");
    }
}
