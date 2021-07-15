using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private bool UseMouse = false;

    [SerializeField]
    private GameObject laserPrefab;


    [SerializeField]
    private float fireRate = 0.25f;

    private float nextFire = 0.0f;

    [SerializeField]
    private int lifes = ShipConst.DefaultLifes;

    [SerializeField]
    private float speed = ShipConst.DefaultShipSpeed;

    [SerializeField]
    private GameObject explosionPrefab;

    //Power Ups
    [SerializeField]
    private GameObject TripleShotPrefab;
    [SerializeField]
    private GameObject ShieldGameObject;
    [SerializeField]
    private GameObject[] EngineFailureGameObject;

    [SerializeField]
    private bool UseTripleShotsPowerUp = false;
    [SerializeField]
    private bool UseSpeedPowerUp = false;
    [SerializeField]
    private bool UseShieldPowerUp = false;

    private UIManager UIManager;
    private GameManager GameManager;
    private SpawnManager SpawnManager;
    private AudioSource audioSourcePlayer;

    [SerializeField]
    private AudioClip audioClip;

    private int? olderValueRandom = null;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3();
        UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        audioSourcePlayer = GetComponent<AudioSource>();
        for (var i = 0; i < EngineFailureGameObject.Length; i++)
        {
            EngineFailureGameObject[i].SetActive(false);
        }
        ShieldGameObject.SetActive(false);
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
        // initialiation of shield
        if (UseShieldPowerUp)
        {
            ShieldGameObject.SetActive(true);
        }
        // limits position Y
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < ShipConst.LimitPositionY)
        {
            transform.position = new Vector3(transform.position.x, ShipConst.LimitPositionY, 0);
        }

        if (UseMouse)
        {
            // limits position X
            if (transform.position.x > ShipConst.LimitPositionX)
            {
                transform.position = new Vector3(ShipConst.LimitPositionX, transform.position.y, 0);
            }
            else if (transform.position.x < -ShipConst.LimitPositionX)
            {
                transform.position = new Vector3(-ShipConst.LimitPositionX, transform.position.y, 0);
            }
        }
        else
        {
            //Position wraps X left to right and vice versa
            if (transform.position.x > ShipConst.WrapsPositionX)
            {
                transform.position = new Vector3(-ShipConst.WrapsPositionX, transform.position.y, 0);
            }
            else if (transform.position.x < -ShipConst.WrapsPositionX)
            {
                transform.position = new Vector3(ShipConst.WrapsPositionX, transform.position.y, 0);
            }
        }
    }
    #endregion

    #region Shooting

    public void PowerUpOn(PowerUps powerUpId)
    {
        switch (powerUpId)
        {
            case PowerUps.TripleShots:
                UseTripleShotsPowerUp = true;
                StartCoroutine(PowerDownRoutine(PowerUpsConst.TripleShotsTimeEnd, PowerUps.TripleShots));
                break;
            case PowerUps.Speed:
                UseSpeedPowerUp = true;
                StartCoroutine(PowerDownRoutine(PowerUpsConst.SpeedTimeEnd, PowerUps.Speed));
                break;
            case PowerUps.Shield:
                UseShieldPowerUp = true;
                StartCoroutine(PowerDownRoutine(PowerUpsConst.ShieldTimeEnd, PowerUps.Shield));
                break;
        }
    }

    public void Damage()
    {
        if (UseShieldPowerUp)
        {
            UseShieldPowerUp = false;
            ShieldGameObject.SetActive(false);
            return;
        }

        lifes -= EnemyConst.DamageEnemyShip;

        if (lifes > 0 && lifes < ShipConst.DefaultLifes)
        {
            var engineFail = Random.Range(0, 2);
            if (olderValueRandom == null)
            {
                olderValueRandom = engineFail;
            }
            else if (olderValueRandom == engineFail && olderValueRandom == 0)//1==1 OR 0==0
            {
                engineFail = 1;
            }
            else
            {
                engineFail = 0;
            }
            ActiveFailEngine(engineFail);
            PowerUpOn(PowerUps.Shield);
        }
        else if (lifes == 0)
        {
            //Animation explosion KATSU
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
            if (GameManager != null && UIManager != null)
            {
                GameManager.GameOver = true;
                UIManager.ShowTitleScreen();
            };
        }

        if (UIManager != null)
        {
            UIManager.UpdateLives(lifes);
        }

    }

    private void ActiveFailEngine(int engineFail)
    {
        EngineFailureGameObject[engineFail].SetActive(true);
    }

    private void Shooting()
    {
        if (!UseMouse && Input.GetKeyDown(KeyCode.Space) || UseMouse && Input.GetMouseButtonDown(0))
        {
            if (Time.time > nextFire)
            {
                audioSourcePlayer.Play();
                nextFire = Time.time + fireRate;
                if (UseTripleShotsPowerUp)
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
    IEnumerator PowerDownRoutine(float time, PowerUps powerUpId)
    {
        yield return new WaitForSeconds(time);

        switch (powerUpId)
        {
            case PowerUps.TripleShots:
                UseTripleShotsPowerUp = false;
                break;
            case PowerUps.Speed:
                UseSpeedPowerUp = false;
                break;
            case PowerUps.Shield:
                UseShieldPowerUp = false;
                break;

        }
    }

    private void TransformTranslate(Vector3 vector3, float Input)
    {
        if (UseSpeedPowerUp)
        {
            speed = ShipConst.DefaultShipSpeed * PowerUpsConst.SpeedMultiplier;
        }
        else
        {
            speed = ShipConst.DefaultShipSpeed;
        }
        if (UseShieldPowerUp)
        {

        }
        transform.Translate(vector3 * speed * Input * Time.deltaTime);
    }

    private Vector3 getPostion(Vector3 position, float x = 0, float y = 0, float z = 0)
    {
        return position + new Vector3(x, y, z);
    }

    #endregion
}
