using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseMonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();
        ShowHomeUI();
    }

    private void ShowHomeUI()
    {
        UIManager.Instance.Show<HomeUI>();
    }
}
