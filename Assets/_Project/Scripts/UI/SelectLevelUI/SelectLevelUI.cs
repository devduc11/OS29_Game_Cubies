using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Teo.AutoReference;
using UnityEngine;

public class SelectLevelUI : BaseUI
{
    [SerializeField, GetInChildren, Name("ItemsLevel")]
    private RectTransform itemsLevel;
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();
        ShowItemsLevel();
    }

    private void ShowItemsLevel()
    {
        foreach (RectTransform item in itemsLevel)
        {
            item.gameObject.SetActive(false);
        }

        Sequence seq = DOTween.Sequence();

        foreach (RectTransform item in itemsLevel)
        {
            item.gameObject.SetActive(true);
            item.localScale = Vector3.zero; // đảm bảo bắt đầu từ 0

            seq.Append(item.DOScale(Vector3.one, 0.1f)); // chạy nối tiếp
        }

    }
}
