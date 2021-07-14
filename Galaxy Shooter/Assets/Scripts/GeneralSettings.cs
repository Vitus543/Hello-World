
public class ShipMovementInputsConst
{
    public const string HorizontalInput = "Horizontal";

    public const string VerticalInput = "Vertical";

    public const string MouseX = "Mouse X";

    public const string MouseY = "Mouse Y";

}
public class ShipConst
{
    public const float LimitPositionY = -4.2f;

    public const float LimitPositionX = 8f;

    public const float WrapsPositionX = 8.65f;

    public const float DefaultShipSpeed = 5.0f;

    public const int DefaultLifes = 3;
}

public class LaserPositionConst
{
    public const float LimitPositionY = 6.0f;

    public const float ShootDefaultPosY = 0.88f;

}

public class PowerUpsConst
{
    //PowerUp Triple Shot Time in seconds
    public const float TripleShotsTimeEnd = 5.0f;

    //public const float SpawnTimeTripleShots = 10.0f;

    //PowerUp Speed Time in seconds
    public const float SpeedTimeEnd = 10.0f;

    //PowerUp Speed x1.5
    public const float SpeedMultiplier = 2.5f;

    //public const float SpawnTimeSpeed = 7.5f;

    //PowerUp shield time for 5 seconds
    public const float ShieldTimeEnd = 5.0f;

    public const float SpawnTime = 5.0f;

}

public class EnemyConst
{
    public const float DefaultSpeedEnemy = 6.0f;

    public const float LimitPositionY = 6.22f;

    public const float LimitPositionX = 7.20f;

    public const int DamageEnemyShip = 1;

    public const float SpawnTime = 5.0f;

    public const int Score = 10;
}
public enum PowerUps
{
    TripleShots,
    Speed,
    Shield
}