using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataUnlockLevel
{
    public bool Unlock;
    public int SumBot;
    public float TimePlay;

    public void SetBotAndTimePlay(int sumBot, float timePlay)
    {
        SumBot = sumBot;
        TimePlay = timePlay;
    }
}
