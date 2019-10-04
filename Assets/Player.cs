using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public enum RotationSensativity { Slow, Normal, Fast, ComiclyFast }
    public RotationSensativity CurrentRotationSensativity = RotationSensativity.Normal;
    private const float MoveForce = 4;
    private const float MaxMoveForce = 20;

    private Rigidbody2D rb;

    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;

    [HideInInspector]
    public int Lap = 0;
    [HideInInspector]
    public float StunnedTime = 0;
    [HideInInspector]
    public int ProjectileCount = 5;
    private float Rotation = 0;
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
            float SteeringSpeed = 2;
            if (CurrentRotationSensativity == RotationSensativity.Normal)
            {
                SteeringSpeed = 2;
            }
            else if (CurrentRotationSensativity == RotationSensativity.Slow)
            {
                SteeringSpeed = 1;
            }
            else if (CurrentRotationSensativity == RotationSensativity.Fast)
            {
                SteeringSpeed = 4;
            }
            else if (CurrentRotationSensativity == RotationSensativity.ComiclyFast)
            {
                SteeringSpeed = 100;
            }
            else
            {
                SteeringSpeed = 0;
            }
            if (Vector3.Distance(Vector3.zero, rb.velocity) < MaxMoveForce)
            {
                rb.AddForce(transform.up * MoveForce * Time.timeScale);
            }
            if (Input.GetKey(LeftKey))
            {
                Rotation += SteeringSpeed * Time.timeScale;
            }
            else if (Input.GetKey(RightKey))
            {
                Rotation -= SteeringSpeed * Time.timeScale;
            }
            transform.rotation = Quaternion.Euler(0, 0, Rotation);
        }
    }
}