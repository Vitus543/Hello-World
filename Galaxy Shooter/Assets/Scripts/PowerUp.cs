using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float Speed = 3.0f;

    [SerializeField]
    private PowerUps PowerUpID;
    private float randomX;

    [SerializeField]
    private AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = RandomPositionX();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
        if (transform.position.y < -EnemyConst.LimitPositionY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player shipPlayer = collision.GetComponent<Player>();
            if (shipPlayer != null)
            {
                shipPlayer.PowerUpOn(PowerUpID);
                AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position,1f);
            }
            Destroy(this.gameObject);
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
