using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private float fireRate = 0.25f;

    private float nextFire = 0.0f;

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private readonly float negativePositionX = -ShipPositionsConst.WrapsPositionX;

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

    private void TransformTranslate(float Input, Vector3 vector3)
    {
        transform.Translate(vector3 * speed * Input * Time.deltaTime);
    }

    private void CheckPostion()
    {
        // limits position Y
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < ShipPositionsConst.limitPositionY)
        {
            transform.position = new Vector3(transform.position.x, ShipPositionsConst.limitPositionY, 0);
        }

        //Position wraps X left to right and vice versa
        if (transform.position.x > ShipPositionsConst.WrapsPositionX)
        {
            transform.position = new Vector3(negativePositionX, transform.position.y, 0);
        }
        else if (transform.position.x < negativePositionX)
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
            Vector3 position = transform.position + new Vector3(0, 0.88f, 0);
            Instantiate(laserPrefab, position, Quaternion.identity);
            //// Instantiate(projectile, transform.position, transform.rotation);
        }
    }
    #endregion
}
