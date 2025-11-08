using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class DataSave
{
    public bool IsSfxOff;
    public bool IsMusicOff;
    public bool IsVibrateOff;
    // public int IndexLevel = 0;
    [Header("---Select Level---")]
    public int IndexUnlockLevel = 0;
    public List<DataUnlockLevel> ListDataUnlockLevel =
        Enumerable.Range(0, 10)
                 .Select(_ => new DataUnlockLevel())
                 .ToList();

}
