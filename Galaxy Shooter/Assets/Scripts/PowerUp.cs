using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float Speed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player shipPlayer = collision.GetComponent<Player>();
            if (shipPlayer != null)
            {
                shipPlayer.PowerUpOn(this.tag);
            }

            Destroy(this.gameObject);
        }
    }

}
