using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    //MusicGame과 Timer은 한 게임 오브젝트에 존재해야 함
    public class MusicGame : MonoBehaviour
    {
        private List<MusicData> _musicPattern;
        private Timer timer;
        private int musicIndex = 0;
    
        public void Awake()
        {
            _musicPattern = GameManager.MusicPattern[LevelMode.Normal];
            timer = gameObject.GetComponent<Timer>();
            timer.RestartTimer();
            Debug.Log(_musicPattern.Count);
        }
    
        void FixedUpdate()
        {
            if (musicIndex > _musicPattern.Count) return;

            Debug.Log($"{_musicPattern[musicIndex].time}, {timer.currentTime}");

            if (_musicPattern[musicIndex].time <= timer.currentTime)
            {
                GameManager.Alerting.Alert(musicIndex.ToString(), AlertMode.Pop, 0.5f);

                GameManager.PoolManager.GetSpawner(_musicPattern[musicIndex].spawnPoint).
                    GetComponent<Spawner>().ReleaseEnemy(_musicPattern[musicIndex].speed);

                musicIndex++;
            }
        }
    }

}