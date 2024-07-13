using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MusicData
{
    public float time;
    public int spawnPoint;
    public float speed;
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
    AttackEffect,
    Cheers,
    ClickButton,
    HeartBreak,
    ScorePopup,
    WinEffect,
    LoseEffect,
}

// Used in BGMManager
public enum EBGMType // Resources/Sound/BGM
{
    BGM,
    StarBubble,
}