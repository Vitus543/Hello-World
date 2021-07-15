using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GameOver = true;

    public GameObject Player;

    private UIManager uIManager;

    private SpawnManager SpawnManager;

    private void Start()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    private void Update()
    {
        if (GameOver == true)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Player, Vector3.zero, Quaternion.identity);
            GameOver = false;
            uIManager.HideTitleScreen();

        }
    }
}
