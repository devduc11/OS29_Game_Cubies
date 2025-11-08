using System.Collections;
using System.Collections.Generic;
using Teo.AutoReference;
using TMPro;
using UnityEngine;

public class ItemLevel : BaseSoundButton
{
    [SerializeField, GetInChildren, Name("LevelText")]
    private TextMeshProUGUI levelText;
    [SerializeField, GetInChildren, Name("SumText")]
    private TextMeshProUGUI sumText;
    [SerializeField, GetInChildren, Name("TimeText")]
    private TextMeshProUGUI timeText;
    [SerializeField, GetInChildren, Name("Unlock")]
    private GameObject unlockObj;
    [SerializeField, GetInChildren, Name("Lock")]
    private GameObject lockObj;
    private int indexLevel;
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void OnClick()
    {
        if (!unlockObj.activeSelf) return;

        Debug.Log($"pnad: {indexLevel}");
    }

    public void SetLevelText(int level)
    {
        levelText.text = $"{level + 1}";
        indexLevel = level;
    }

    public void SetSumText(int sum)
    {
        sumText.text = $"x{sum}";
    }

    public void SetUnlock(bool bl)
    {
        unlockObj.SetActive(bl);
        lockObj.SetActive(!bl);
    }

    public void UpdateTimer(float currentTime)
    {
        int hours = Mathf.FloorToInt(currentTime / 3600f);
        int minutes = Mathf.FloorToInt((currentTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        string text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        timeText.text = text;
    }

}
