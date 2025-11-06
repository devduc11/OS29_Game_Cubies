using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSelectLevelUIButton : BaseSoundButton
{
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void OnClick()
    {
        UIManager.Instance.Hide<SelectLevelUI>();
    }
}
