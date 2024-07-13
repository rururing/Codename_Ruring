using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    enum EPathMode
    { 
        None,
        Linear_Right,
        Linear_Left,
        Linear_Down,
        Curve_DownRight,
        Curve_DownLeft
    }

    [SerializeField] EPathMode _spawnerPathMode;
    //GameManager _gameManager;
    [SerializeField] ObjectPoolManager _poolManager;

    Vector3 _targetPos;

    void Start()
    {
        if (!_poolManager)
            _poolManager = FindObjectOfType<ObjectPoolManager>();

        _targetPos = transform.GetChild(0).transform.position; // WC

        // TEST
        ReleaseEnemy();
    }
    
    void ReleaseEnemy()
    {
        if (_spawnerPathMode == EPathMode.None) return;

        GameObject enemyObj = _poolManager.SetEnemyObjReady();
        Debug.Log(enemyObj.name);

        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.MoveSpeed = 4.0f; // TODO: Change by difficulty

        enemyObj.transform.position = transform.position;
        switch (_spawnerPathMode)
        {
            case EPathMode.Linear_Right:
                enemy.EnemyRb.velocity = new Vector2(enemy.MoveSpeed, 0);
                break;

            case EPathMode.Linear_Left:
                enemy.EnemyRb.velocity = new Vector2(-enemy.MoveSpeed, 0);
                break;

            case EPathMode.Linear_Down:
                enemy.EnemyRb.velocity = new Vector2(0, -enemy.MoveSpeed);
                break;

            case EPathMode.Curve_DownRight:
                enemy.transform.DOMoveX(_targetPos.x, 3).SetEase(Ease.InQuad);
                enemy.transform.DOMoveY(_targetPos.y, 3).SetEase(Ease.OutQuad);
                break;

            case EPathMode.Curve_DownLeft:
                enemy.transform.DOMoveX(_targetPos.x, 3).SetEase(Ease.InQuad);
                enemy.transform.DOMoveY(_targetPos.y, 3).SetEase(Ease.OutQuad);
                break;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.GetChild(0).position);
    }
}
