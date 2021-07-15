using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private readonly float Speed = EnemyConst.DefaultSpeedEnemy;

    [SerializeField]
    private float randomX = 0f;

    public GameObject enemyExplosionPrefab;
    
    private UIManager UIManager;


    [SerializeField]
    private AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = RandomPositionX();

        UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }

    // Update is called once per frame
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * Speed * Time.deltaTime);

        if (transform.position.y < -EnemyConst.LimitPositionY)
        {
            transform.position = RandomPositionX();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player shipPlayer = collision.GetComponent<Player>();
            if (shipPlayer != null)
            {
                shipPlayer.Damage();
            }
        }
        else if (collision.CompareTag("Shot"))
        {

            if (collision.transform.parent != null)
            {
                Destroy(collision.transform.parent.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }

            if (UIManager != null)
            {
                UIManager.UpdateScore(EnemyConst.Score);
            }
        }

        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, 1f);
        //Animation explosion
        Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
    #region helpers

    private Vector3 RandomPositionX()
    {
        randomX = Random.Range(-EnemyConst.LimitPositionX, EnemyConst.LimitPositionX);
        return new Vector3(randomX, EnemyConst.LimitPositionY, 0);
    }

    #endregion
}
