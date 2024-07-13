using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public ObjectPoolManager _poolManager;
    //SoundManager _soundManager;
    private AlertManager alert;
    public static AlertManager Alerting { get { return Instance.alert; } }
    
    public static Dictionary<LevelMode, List<MusicData>> MusicPattern { get; private set; }= new Dictionary<LevelMode, List<MusicData>>();

    string _sceneShootingName = "Scene_Shooting";

    int _totalScore;
    int _successScore;

    int _playerLife = 5;

    private void Awake()
    {
        base.Awake();
        alert = new AlertManager();
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //_soundManager = SoundManager.Instance;

        _successScore = 100;
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
            Cursor.visible = false;
            _playerLife = 3;

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
            Cursor.visible = true;
            _poolManager = null;
        }
    }

    void HandleScore(EHitState hitState)
    {
        switch (hitState)
        {
            case EHitState.Fail:
                if (--_playerLife <= 0)
                    print("GAME OVER");
                print("FAIL SCORE ADDED");
                break;

            case EHitState.Success:
                _totalScore += _successScore;
                print("SUCCESS SCORE ADDED");
                break;
            default:
                break;
        }

    }

}
