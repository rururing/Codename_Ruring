using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// TODO: 각 State 당 임계값 설정
public enum EHitState
{ 
    None,
    Fail,
    Bad,
    Good,
    Perfect
}

public class Enemy : MonoBehaviour
{
    [SerializeField] float _decisionTime = 5f;
    
    GameObject _decisionCircle;
    
    float _decCircleFirstScale;

    float _targetScale;

    float _scaleDiff;

    public UnityEvent<EHitState> OnDecided;

    void Start()
    {
        _decisionCircle = transform.GetChild(0).gameObject;

        _decCircleFirstScale = _decisionCircle.transform.localScale.x;

        _targetScale = transform.localScale.x;

        _scaleDiff = _decCircleFirstScale - _targetScale;

        StartCoroutine("ShrinkCircle");
    }

    void Update()
    {
        CheckCircle();
    }
    IEnumerator ShrinkCircle()
    {
        yield return null;
        float newScale = _decCircleFirstScale;

        while (true)
        {
            newScale -= (_decCircleFirstScale - Mathf.Lerp(_decCircleFirstScale, _targetScale, Time.deltaTime / _decisionTime));

            _decisionCircle.transform.localScale = new Vector3(newScale, newScale, newScale);

            yield return null;
        }
    }

    void CheckCircle()
    {
        if (_decisionCircle.transform.lossyScale.x <= transform.lossyScale.x)
        {
            OnDecided?.Invoke(EHitState.Fail);
            ResetStatus();
        }
    }

    void ResetStatus()
    {
        _decisionCircle.transform.localScale = new Vector3(_decCircleFirstScale, _decCircleFirstScale, _decCircleFirstScale);
        Debug.Log("OK");
        transform.gameObject.SetActive(false);
    }

}

