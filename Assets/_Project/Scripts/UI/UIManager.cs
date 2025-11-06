using System.Collections;
using System.Collections.Generic;
using Teo.AutoReference;
using UnityEngine;

public class UIManager : BaseUIManager<UIManager>
{
    [SerializeField, Get] private Canvas canvas;
    public Canvas Canvas => canvas;

    [SerializeField, GetInChildren, Name("MainCanvas")]
    private Transform parent;
    protected override string GetFolderPrefabs()
    {
        return "Assets/_Project/Prefabs/UI";
    }

    protected override Transform GetParent()
    {
        return parent;
    }
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion
}
