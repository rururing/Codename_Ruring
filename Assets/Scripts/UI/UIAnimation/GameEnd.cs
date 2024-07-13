using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameEnd : MonoBehaviour
{
    public RectTransform currentScore;
    public RectTransform bestScore;
    
    void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(currentScore.DOAnchorPos(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutExpo).SetDelay(2f));
        seq.Append(bestScore.DOAnchorPos(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutExpo));
        seq.Play();
    }
}
