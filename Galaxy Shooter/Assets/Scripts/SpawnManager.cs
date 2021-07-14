using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyShipsPrefabs;

    [SerializeField]
    private GameObject[] PowerUpsPrefabs;
    public bool Stop = false;
    public void StartSpawnEnemysAndPowerUps() 
    {
        if (Stop)
        {
            Stop = false;
        }
        StartCoroutine(SpawnManagerRoutine(EnemyConst.SpawnTime));

        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnManagerRoutine(float time)
    {
        while (!Stop)
        {
            if (Stop)
            {
                yield break;
            }
            yield return new WaitForSeconds(time);
            Instantiate(EnemyShipsPrefabs);
        }
    }

    IEnumerator SpawnPowerUp()
    {
        while (!Stop)
        {
            if (Stop) 
            {
                yield break;
            }
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(PowerUpsPrefabs[randomPowerUp]);
            yield return new WaitForSeconds(PowerUpsConst.SpawnTime);
        }
    }
}
