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
        int indexUnlockLevel = SaveManager.Instance.DataSave.IndexUnlockLevel;
        if (indexUnlockLevel == 0)
        {
            CheckUnlockLevel();
        }
    }

    public void LodGame()
    {

    }

    public void CheckUnlockLevel()
    {
        int indexUnlockLevel = SaveManager.Instance.DataSave.IndexUnlockLevel;
        DataUnlockLevel dataUnlockLevel = SaveManager.Instance.DataSave.ListDataUnlockLevel[indexUnlockLevel];
        dataUnlockLevel.Unlock = true;
    }

    public void CheckWin()
    {
        SetBotAndTimePlay(1, 120);
        if (IsMaxLevel()) return;
        SaveManager.Instance.DataSave.IndexUnlockLevel += 1;
        CheckUnlockLevel();
    }

    private void SetBotAndTimePlay(int sumBot, float timePlay)
    {
        int indexUnlockLevel = SaveManager.Instance.DataSave.IndexUnlockLevel;
        DataUnlockLevel dataUnlockLevel = SaveManager.Instance.DataSave.ListDataUnlockLevel[indexUnlockLevel];
        dataUnlockLevel.SetBotAndTimePlay(sumBot, timePlay);
    }

    private bool IsMaxLevel()
    {
        return SaveManager.Instance.DataSave.IndexUnlockLevel >= SaveManager.Instance.DataSave.ListDataUnlockLevel.Count - 1;
    }


}
