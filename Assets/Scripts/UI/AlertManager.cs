using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI
{
    public enum AlertMode
    {
        Pop,
    }
    //알림창 팝업 - n초 후 삭제 기능
    public class AlertManager //GameManager object에 넣기
    {
        private Dictionary<AlertMode, GameObject> _alertObjects = new Dictionary<AlertMode, GameObject>();
        private GameObject canvas;
        public AlertManager()
        {
            //_alertObjects들 가져오기
            foreach (AlertMode mode in Enum.GetValues(typeof(LevelMode)))
            {
                _alertObjects.Add(mode, Resources.
                    Load<GameObject>($"Prefab/UI/AlertUI/{mode.ToString()}"));
            }
        }
        
        public void Alert(string sentence, AlertMode mode, float deleteTime)
        {
            if (canvas is null) 
                canvas = Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/AlertUI/Alert_Canvas"));

            GameObject createdGo = Object.Instantiate(_alertObjects[mode], canvas.transform);
            createdGo.GetComponent<TextMeshProUGUI>().text = sentence;
            Object.Destroy(createdGo, deleteTime);
        }
    }
}