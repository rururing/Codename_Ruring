using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LobbyTest : MonoBehaviour
{
    void Start()
    {
        Invoke("StartGame", 5f);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(3);
    }
}
