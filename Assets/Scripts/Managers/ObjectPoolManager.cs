using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab; // 인스펙터창에서 지정 필요
    [SerializeField] GameObject _spawnerParent;

    const int POOLSIZE = 256;
    
    GameObject[] _enemyObjs;
    public Enemy[] _enemies;

    GameObject[] _spawners;

    void Awake()
    {
        _enemyObjs = new GameObject[POOLSIZE];
        _enemies = new Enemy[POOLSIZE];
        _spawners = new GameObject[_spawnerParent.transform.childCount];

        for (int i = 0; i < _spawners.Length; ++i)
            _spawners[i] = _spawnerParent.transform.GetChild(i).gameObject;
        Generate();
    }

    void Generate()
    {
        for(int i=0; i<_enemyObjs.Length; i++) 
        {
            _enemyObjs[i] = Instantiate(_enemyPrefab);
            _enemies[i] = _enemyObjs[i].GetComponent<Enemy>();  
            _enemyObjs[i].SetActive(false);
        }
    }

    public void PoolsOff()
    {
        for(int i=0; i<_enemyObjs.Length; i++)
        {
            _enemyObjs[i].SetActive(false); 
        }

        print("POOLS OFF");
    }

    public GameObject GetEnemyObjReady(float moveSpeed, LevelMode currentLevel)
    {
        float decisionTime = 0f;
        switch(currentLevel)
        {
            case LevelMode.Easy:
                decisionTime = 2.5f;
                break;
            case LevelMode.Normal:
                decisionTime = 2.0f;
                break;
            case LevelMode.Hard:
                decisionTime = 1.5f;
                break;
        }

        for(int i=0; i<_enemyObjs.Length; i++)
        {
            if (!_enemyObjs[i].activeSelf)
            {
                _enemyObjs[i].SetActive(true);
                _enemies[i].moveSpeed = moveSpeed;
                _enemies[i].decisionTime = decisionTime;
                return _enemyObjs[i];
            }
        }
        Debug.Log("ALL ENEMIES ARE ACTIVE NOW");
        return null;
    }

    public GameObject GetSpawner(int i)
    {
        return _spawners[i];
    }



}
