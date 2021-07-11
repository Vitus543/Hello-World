using UnityEngine;

public class Player : MonoBehaviour
{

    public bool CanTripleShoot = false;

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

        TransformTranslate(horizontalInput, Vector3.right);
        TransformTranslate(verticalInput, Vector3.up);

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
    #endregion

    #region Shooting
    private void Shooting()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Time.time > nextFire)
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
    #endregion

    #region Helpers
    private void TransformTranslate(float Input, Vector3 vector3)
    {
        transform.Translate(vector3 * speed * Input * Time.deltaTime);
    }

    private Vector3 getPostion(Vector3 position, float x = 0, float y = 0, float z = 0)
    {
        return position + new Vector3(x, y, z);
    }
    #endregion
}
