using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    private readonly float negativePositionX = -PositionsConst.WrapsPositionX;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis(GeneralSettings.HorizontalInput);
        float verticalInput = Input.GetAxis(GeneralSettings.VerticalInput);

        transformTranslate(horizontalInput, Vector3.right);
        transformTranslate(verticalInput, Vector3.up);

        CheckPostion();
    }

    private void transformTranslate(float Input, Vector3 vector3)
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
        else if (transform.position.y < PositionsConst.limitPositionY)
        {
            transform.position = new Vector3(transform.position.x, PositionsConst.limitPositionY, 0);
        }

        //Position wraps X left to right and vice versa
        if (transform.position.x > PositionsConst.WrapsPositionX)
        {
            transform.position = new Vector3(negativePositionX, transform.position.y, 0);
        }
        else if (transform.position.x < negativePositionX)
        {
            transform.position = new Vector3(PositionsConst.WrapsPositionX, transform.position.y, 0);
        }
    }
}
