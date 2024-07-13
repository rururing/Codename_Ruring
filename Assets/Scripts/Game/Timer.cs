using UnityEngine;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        public bool isTimerRunning { get; private set; }
        public float currentTime { get; private set; }
        
        void Start()
        {
            // 타이머 시작
            isTimerRunning = true;
            currentTime = 0.0f;
        }

        void FixedUpdate()
        {
            if (isTimerRunning)
            {
                currentTime += Time.deltaTime;
            }
        }

        public void StopTimer()
        {
            isTimerRunning = false;
        }

        public void RestartTimer()
        {
            currentTime = 0f;
            isTimerRunning = true;
        }

        public void ResetTimer()
        {
            currentTime = 0f;
            isTimerRunning = false;
        }

        public void StartTimer()
        {
            isTimerRunning = true;
        }
    }
}