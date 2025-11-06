using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSoundButton : BaseButton
{
    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    protected override void OnClick()
    {
        // throw new System.NotImplementedException();
    }
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();
        transform.localScale = Vector3.one;
    }

    protected override void OnClickListener()
    {
        PlaySound();
        base.OnClickListener();
    }

    protected virtual void PlaySound()
    {
        // SoundManager.Instance.PlaySFX(SoundType.Button);
    }
}
