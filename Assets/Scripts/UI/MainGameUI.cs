using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;

public class MainGameUI : UIManager
{
    public TextMeshProUGUI ComboText;
    public GameObject[] hearts = new GameObject[3];
    public int heartCount = 3;
    void Start()
    {
        GameManager.Fail -= HeartBreak;
        GameManager.Fail += HeartBreak;
        heartCount = GameManager._playerLife;
    }

    void Update()
    {
        ComboText.text = MusicGame.Combo.ToString();
    }

    public void HeartBreak()
    {
        hearts[heartCount].SetActive(false);
        heartCount--;
    }
}
