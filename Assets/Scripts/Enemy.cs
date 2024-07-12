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

    public UnityEvent<EHitState> OnDecided;

    float _moveSpeed;
    Rigidbody2D _enemyRb;

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public Rigidbody2D EnemyRb => _enemyRb;
    
    void Awake()
    {
        _decisionCircle = transform.GetChild(0).gameObject;
        _decCircleFirstScale = _decisionCircle.transform.lossyScale.x;
        _targetScale = transform.lossyScale.x;
        _enemyRb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        StartCoroutine("ShrinkCircle");
    }
    void OnDisable()
    {
        StopCoroutine("ShrinkCircle");
        ResetStatus_Disable();
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

            _decisionCircle.transform.parent = null;
            _decisionCircle.transform.localScale = new Vector3(newScale, newScale, newScale);
            _decisionCircle.transform.parent = transform;

            yield return null;
        }
    }

    void CheckCircle()
    {
        float currentScale = _decisionCircle.transform.lossyScale.x;
        float targetScale = transform.lossyScale.x;
        if (currentScale <= targetScale) 
        {
            OnDecided?.Invoke(EHitState.Fail);
            gameObject.SetActive(false);
        }
        else if (currentScale > targetScale)
        {
            // TODO: EhitState 판정 판단해서 각 경우에 대한 이벤트 전달
            
        }
    }

    void ResetStatus_Disable()
    {
        _decisionCircle.transform.localScale = new Vector3(_decCircleFirstScale, _decCircleFirstScale, _decCircleFirstScale);
        _enemyRb.velocity = Vector2.zero;

        transform.gameObject.SetActive(false);
    }

    public void Hit()
    {
        // TODO: 간격 판정 후 점수 invoke
        
    }



}

