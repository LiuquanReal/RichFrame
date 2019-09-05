using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public List<ProgressControl> progressQueue = new List<ProgressControl>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddProgress(ProgressControl progress)
    {
        progressQueue.Add(progress);
    }

    public void StartProgress()
    {
        StartCoroutine(StartProgressAsync());
    }

    IEnumerator StartProgressAsync()
    {
        if (progressQueue.Count > 0)
        {
            progressQueue[0].StartProgress();
        }
        while (progressQueue.Count > 0)
        {
            if (progressQueue[0].IsCompleted)
            {
                progressQueue.RemoveAt(0);
                if (progressQueue.Count > 0)
                {
                    progressQueue[0].StartProgress();
                }
                else
                {
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
