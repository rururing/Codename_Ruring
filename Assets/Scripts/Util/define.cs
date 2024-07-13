using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MusicData
{
    public float time;
    public string route;
}

public enum LevelMode
{
    Easy,
    Normal,
    Hard,
}
public enum EHitState
{
    None,
    Fail,
    Success
}
// Used in SoundManager
public enum ESoundType // Resources/Sound/Effect
{
    None,
}

// Used in BGMManager
public enum EBGMType // Resources/Sound/BGM
{
    None,
}