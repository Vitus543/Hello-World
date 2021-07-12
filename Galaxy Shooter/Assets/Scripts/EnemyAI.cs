using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private readonly float Speed = EnemyConst.DefaultSpeedEnemy;

    [SerializeField]
    private float randomX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = RandomPositionX();
    }

    // Update is called once per frame
    void Update()
    {

        //move down
        transform.Translate(Vector3.down * Speed * Time.deltaTime);

        // when off the screen on the bottom         
        // respawn back on top with a new x position between the bounds of the screen

        if (transform.position.y < -EnemyConst.LimitPositionY)
        {
            transform.position = RandomPositionX();
        }
    }

    #region helpers

    private Vector3 RandomPositionX()
    {
        randomX = Random.Range(-EnemyConst.LimitPositionX, EnemyConst.LimitPositionX);
        return new Vector3(randomX, EnemyConst.LimitPositionY, 0);
    }

    #endregion
}
