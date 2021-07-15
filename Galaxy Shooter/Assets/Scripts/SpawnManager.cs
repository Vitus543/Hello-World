using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyShipsPrefabs;

    [SerializeField]
    private GameObject[] PowerUpsPrefabs;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutine(SpawnManagerRoutine(EnemyConst.SpawnTime));

        StartCoroutine(SpawnPowerUp());
    }

    public void StartSpawnEnemysAndPowerUps()
    {
        StartCoroutine(SpawnManagerRoutine(EnemyConst.SpawnTime));

        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnManagerRoutine(float time)
    {
        while (gameManager.GameOver == false)
        {
            yield return new WaitForSeconds(time);
            Instantiate(EnemyShipsPrefabs);
        }
    }

    IEnumerator SpawnPowerUp()
    {
        while (gameManager.GameOver == false)
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(PowerUpsPrefabs[randomPowerUp]);
            yield return new WaitForSeconds(PowerUpsConst.SpawnTime);
        }
    }
}
