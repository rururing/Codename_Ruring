using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public ObjectPoolManager _poolManager;
    
    string _sceneShootingName = "Scene_Shooting";

    float _totalScore;
    int _failScore, _badScore, _goodScore, _perfectScore;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        // TODO: 값 변경
        _failScore = -5;
        _badScore = 1;
        _goodScore = 5;
        _perfectScore = 10;
    }

    void Update()
    {
        // TEST: Scene load 
        if (Input.GetKeyDown(KeyCode.V))
        {
            print("Scene loaded from GameManager");
            SceneManager.LoadScene(_sceneShootingName);
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == _sceneShootingName)
        {
            _poolManager = FindObjectOfType<ObjectPoolManager>();

            if (_poolManager)
            {
                for(int i=0; i<_poolManager._enemies.Length; ++i)
                {
                    _poolManager._enemies[i].OnDecided.AddListener(HandleScore);
                }
                _poolManager.PoolsOff();
            }
        }
        else
        {
            _poolManager = null;
        }
    }

    void HandleScore(EHitState hitState)
    {
        switch (hitState)
        {
            case EHitState.Fail:
                _totalScore += _failScore;
                break;
            case EHitState.Bad:
                _totalScore += _badScore;
                break;
            case EHitState.Good:
                _totalScore += _goodScore;
                break;
            case EHitState.Perfect:
                _totalScore += _perfectScore;
                break;

            default:
                break;
        }

    }



}
