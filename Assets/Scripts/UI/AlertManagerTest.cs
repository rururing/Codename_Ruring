using UnityEngine;

namespace UI
{
    public class AlertManagerTest : MonoBehaviour
    {
        void Start()
        {
            GameManager.Alerting.Alert("Hello!", AlertMode.Pop, 3f);
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.U))
                GameManager.Alerting.Alert("Hello!", AlertMode.Pop, 3f);
        }
    }
}