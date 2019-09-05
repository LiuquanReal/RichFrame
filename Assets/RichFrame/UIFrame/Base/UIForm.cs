using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIForm : MonoBehaviour
{
    public string formName;
    public bool hideOnStart = false;
    public bool setPosZero = false;
    public ShowType showType;
    
    protected virtual void Awake()
    {
        if (string.IsNullOrEmpty(formName))
        {
            formName = gameObject.name;
        }
        UIManager.Instance.RegisterUIForm(formName, this);
    }

    protected virtual void Start()
    {
        if (hideOnStart)
        {
            Close();
        }
        if (setPosZero)
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public void Show()
    {
        UIManager.Instance.ShowForm(formName);
    }

    public void Hide()
    {
        UIManager.Instance.HideForm(formName);
    }

    public virtual void Display()
    {
        if (showType == ShowType.Popup)
        {
            UIMask.Instance.ShowMask(transform);
        }
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        if (showType == ShowType.Popup)
        {
            UIMask.Instance.HideMask(transform);
        }
        gameObject.SetActive(false);
    }

    public enum ShowType {
        Normal,
        /// <summary>
        /// 显示在层级最顶层
        /// </summary>
        Popup,
        /// <summary>
        /// 隐藏其它
        /// </summary>
        HideOther
    }
}
