using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _decisionTime = 5f;
    [SerializeField] float _decisionThreshold = 0.1f;

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
        DebugDecision();
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
    }

    void ResetStatus_Disable()
    {
        _decisionCircle.transform.localScale = new Vector3(_decCircleFirstScale, _decCircleFirstScale, _decCircleFirstScale);
        _enemyRb.velocity = Vector2.zero;

        transform.gameObject.SetActive(false);
    }

    public void Hit()
    {
        float decision = Mathf.InverseLerp(_targetScale, _decCircleFirstScale, _decisionCircle.transform.lossyScale.x);
        if (decision <= _decisionThreshold)
        {
            OnDecided?.Invoke(EHitState.Success);
        }
        else
        {
            OnDecided?.Invoke(EHitState.Fail);
        }

        Debug.Log("OK");
        transform.gameObject.SetActive(false);
    }

    void DebugDecision()
    {
        SpriteRenderer renderer = _decisionCircle.GetComponent<SpriteRenderer>();

        float decision = Mathf.InverseLerp(_targetScale, _decCircleFirstScale, _decisionCircle.transform.lossyScale.x);
        if (decision <= _decisionThreshold)
        {
            renderer.color = Color.green;
        }
        else
        {
            renderer.color = Color.grey;
        }
    }


}

