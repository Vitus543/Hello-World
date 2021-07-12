using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool CanTripleShoot = false;

    [SerializeField]
    private bool UseMouse = true;

    [SerializeField]
    private GameObject laserPrefab;

    //Power Ups
    [SerializeField]
    private GameObject TripleShotPrefab;

    [SerializeField]
    private float fireRate = 0.25f;

    private float nextFire = 0.0f;

    [SerializeField]
    private float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        Shooting();
    }

    #region Movement
    private void Movement()
    {
        float horizontalInput = Input.GetAxis(ShipMovementInputsConst.HorizontalInput);
        float verticalInput = Input.GetAxis(ShipMovementInputsConst.VerticalInput);

        if (UseMouse)
        {
            horizontalInput = Input.GetAxis(ShipMovementInputsConst.MouseX);
            verticalInput = Input.GetAxis(ShipMovementInputsConst.MouseY);
        }

        TransformTranslate(Vector3.right, horizontalInput);
        TransformTranslate(Vector3.up, verticalInput);

        CheckPostion();
    }


    private void CheckPostion()
    {
        // limits position Y
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < ShipPositionsConst.LimitPositionY)
        {
            transform.position = new Vector3(transform.position.x, ShipPositionsConst.LimitPositionY, 0);
        }

        if (UseMouse)
        {
            // limits position X
            if (transform.position.x > ShipPositionsConst.LimitPositionX)
            {
                transform.position = new Vector3(ShipPositionsConst.LimitPositionX, transform.position.y, 0);
            }
            else if (transform.position.x < -ShipPositionsConst.LimitPositionX)
            {
                transform.position = new Vector3(-ShipPositionsConst.LimitPositionX, transform.position.y, 0);
            }
        }
        else
        {
            //Position wraps X left to right and vice versa
            if (transform.position.x > ShipPositionsConst.WrapsPositionX)
            {
                transform.position = new Vector3(-ShipPositionsConst.WrapsPositionX, transform.position.y, 0);
            }
            else if (transform.position.x < -ShipPositionsConst.WrapsPositionX)
            {
                transform.position = new Vector3(ShipPositionsConst.WrapsPositionX, transform.position.y, 0);
            }
        }
    }
    #endregion

    #region Shooting

    public void PowerUpOn(string tagPowerUp)
    {
        switch (tagPowerUp)
        {
            case "TripleShot_PowerUp":
                CanTripleShoot = true;
                StartCoroutine(TripleShotPowerDownRoutine());
                break;
        }
    }
    private void Shooting()
    {
        if (!UseMouse && Input.GetKeyDown(KeyCode.Space) || UseMouse && Input.GetMouseButtonDown(0))
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                if (CanTripleShoot)
                {
                    Instantiate(TripleShotPrefab, getPostion(transform.position), Quaternion.identity);
                }
                else
                {
                    Instantiate(laserPrefab, getPostion(transform.position, y: LaserPositionConst.ShootDefaultPosY), Quaternion.identity);
                }
            }
        }
    }

    #endregion

    #region Helpers
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(PowerUpsConst.TripleShotsTimeEnd);

        CanTripleShoot = false;
    }

    private void TransformTranslate(Vector3 vector3, float Input)
    {

        transform.Translate(vector3 * speed * Input * Time.deltaTime);
    }

    private Vector3 getPostion(Vector3 position, float x = 0, float y = 0, float z = 0)
    {
        return position + new Vector3(x, y, z);
    }


    #endregion
}
