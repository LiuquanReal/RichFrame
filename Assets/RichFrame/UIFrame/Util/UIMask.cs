using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMask : Singleton<UIMask>
{
    GameObject mask;

    public void ShowMask(Transform uiFormTrans)
    {
        if (mask == null)
        {
            mask = new GameObject("mask", typeof(Image), typeof(RectTransform));
            mask.transform.parent = uiFormTrans.GetComponentInParent<Canvas>().transform;
            mask.transform.localScale = Vector3.one;
            var r = mask.GetComponent<RectTransform>();
            mask.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
            r.anchorMax = Vector2.one;
            r.anchorMin = Vector2.zero;
            r.offsetMax = Vector2.zero;
            r.offsetMin = Vector2.zero;
        }
        else
        {
            mask.SetActive(true);
        }
        mask.transform.SetAsLastSibling();
        uiFormTrans.SetAsLastSibling();
    }

    public void HideMask(Transform uiFormTrans)
    {
        if (mask)
        {
            mask.gameObject.SetActive(false);
        }
    }
}
