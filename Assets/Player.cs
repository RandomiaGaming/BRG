using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public enum RotationSensativity { Normal, Fast, SuperFast }
    public RotationSensativity CurrentRotationSensativity = RotationSensativity.Normal;
    private const float MoveForce = 4;
    private const float MaxMoveForce = 20;

    private Rigidbody2D rb;

    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;

    [Space]
    public GameObject Bomb;
    public GameObject Missile;
    [Space]
    public Transform BombLauncher;
    public Transform MissileLauncher;

    [HideInInspector]
    public int Lap = 0;
    [HideInInspector]
    public float StunnedTime = 0;
    [HideInInspector]
    public int ProjectileCount = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (StunnedTime > 0)
        {
            StunnedTime -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            float SteeringSpeed = 0;
            if (CurrentRotationSensativity == RotationSensativity.Normal)
            {
                SteeringSpeed = 2;
            }
            else if (CurrentRotationSensativity == RotationSensativity.Fast)
            {
                SteeringSpeed = 3;
            }
            else if (CurrentRotationSensativity == RotationSensativity.SuperFast)
            {
                SteeringSpeed = 4;
            }
            if (Vector3.Distance(Vector3.zero, rb.velocity) < MaxMoveForce)
            {
                rb.AddForce(transform.up * MoveForce * Time.timeScale);
            }
            if (Input.GetKey(LeftKey))
            {
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + SteeringSpeed * Time.timeScale);
            }
            else if (Input.GetKey(RightKey))
            {
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - SteeringSpeed * Time.timeScale);
            }
            if (Input.GetKeyDown(UpKey) && ProjectileCount > 0)
            {
                ProjectileCount--;
                Instantiate(Missile, MissileLauncher.position, transform.rotation);
            }
            else if (Input.GetKeyDown(DownKey) && ProjectileCount > 0)
            {
                ProjectileCount--;
                Instantiate(Bomb, BombLauncher.position, transform.rotation);
            }
        }
    }
}