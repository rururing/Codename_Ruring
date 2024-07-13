using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab; // 인스펙터창에서 지정 필요

    const int POOLSIZE = 256;
    GameObject[] _enemyPool;
    public Enemy[] _enemies;

    void Awake()
    {
        _enemyPool = new GameObject[POOLSIZE];
        _enemies = new Enemy[POOLSIZE];

        Generate();
    }
    void Generate()
    {
        for(int i=0; i<_enemyPool.Length; i++) 
        {
            _enemyPool[i] = Instantiate(_enemyPrefab);
            _enemies[i] = _enemyPool[i].GetComponent<Enemy>();  

            _enemyPool[i].SetActive(false);
        }
    }

    public void PoolsOff()
    {
        for(int i=0; i<_enemyPool.Length; i++)
        {
            _enemyPool[i].SetActive(false); 
        }

        print("POOLS OFF");
    }

    public GameObject SetEnemyObjReady()
    {
        for(int i=0; i<_enemyPool.Length; i++)
        {
            if (!_enemyPool[i].activeSelf)
            {
                _enemyPool[i].SetActive(true);
                return _enemyPool[i];
            }
        }
        Debug.Log("ALL ENEMIES ARE ACTIVE NOW");
        return null;
    }




}
