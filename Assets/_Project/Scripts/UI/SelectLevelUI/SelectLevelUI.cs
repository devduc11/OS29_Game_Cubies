using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Teo.AutoReference;
using UnityEngine;

public class SelectLevelUI : BaseUI
{
    [SerializeField, GetInChildren, Name("ItemsLevel")]
    private RectTransform itemsLevel;
    [SerializeField, GetInChildren]
    private List<ItemLevel> itemLevels = new List<ItemLevel>();
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < itemLevels.Count; i++)
        {
            ItemLevel itemLevel = itemLevels[i];
            itemLevel.SetLevelText(i);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ShowItemsLevel();
        LoadData();
    }

    private void LoadData()
    {
        for (int i = 0; i < itemLevels.Count; i++)
        {
            ItemLevel itemLevel = itemLevels[i];
            DataUnlockLevel dataUnlockLevel = SaveManager.Instance.DataSave.ListDataUnlockLevel[i];
            itemLevel.SetUnlock(dataUnlockLevel.Unlock);
            itemLevel.SetSumText(dataUnlockLevel.SumBot);
            itemLevel.UpdateTimer(dataUnlockLevel.TimePlay);
        }
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
