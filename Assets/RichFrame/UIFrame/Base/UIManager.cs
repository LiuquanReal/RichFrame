using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Dictionary<string, UIForm> uiForms = new Dictionary<string, UIForm>();
    List<UIForm> showingForms = new List<UIForm>();

    /// <summary>
    /// 注册一个UI面板
    /// </summary>
    /// <param name="formName"></param>
    /// <param name="uiForm"></param>
    public void RegisterUIForm(string formName,UIForm uiForm)
    {
        uiForms.Add(formName, uiForm);
    }

    /// <summary>
    /// 显示UI
    /// </summary>
    /// <param name="formName"></param>
    public void ShowForm(string formName)
    {
        UIForm f;
        if (uiForms.TryGetValue(formName,out f))
        {
            f.Show();
            switch (f.showType)
            {
                case UIForm.ShowType.Normal:
                    break;
                case UIForm.ShowType.Popup:
                    break;
                case UIForm.ShowType.HideOther:
                    HideAll();
                    break;
                default:
                    break;
            }
            showingForms.Add(f);
        }
    }
    /// <summary>
    /// 隐藏UI
    /// </summary>
    /// <param name="formName"></param>
    public void HideForm(string formName)
    {
        UIForm f;
        if (uiForms.TryGetValue(formName, out f))
        {
            switch (f.showType)
            {
                case UIForm.ShowType.Normal:
                    break;
                case UIForm.ShowType.Popup:
                    break;
                case UIForm.ShowType.HideOther:
                    ShowAll();
                    break;
                default:
                    break;
            }
            showingForms.Remove(f);
            f.Hide();
        }
    }

    void HideAll()
    {
        foreach (var item in showingForms)
        {
            item.gameObject.SetActive(false);
        }
    }

    void ShowAll()
    {
        foreach (var item in showingForms)
        {
            item.gameObject.SetActive(true);
        }
    }

}
