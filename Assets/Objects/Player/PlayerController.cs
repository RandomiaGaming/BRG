using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float MoveForce = 0.4f;
    public float MaxMoveForce = 10;
    public float RotationSpeed = 10f;

    private new Rigidbody2D rigidbody2D;

    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;

    [Space]
    public GameObject Missile_Prefab;
    [Space]
    public int Lap = 0;
    public int Last_Touched_Checkpoint = 0;
    public float Place_Checkpoint_Distance = 0;
    public int Place_Checkpoint = 0;
    public int Place = 0;
    public float Stunned_Time = 0;
    public int PowerUp_Count = 5;
    public int Player_ID = 0;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Stunned_Time > 0)
        {
            Stunned_Time -= Time.deltaTime;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (Vector3.Distance(Vector3.zero, rigidbody2D.velocity) < MaxMoveForce)
        {
            rigidbody2D.velocity += (Vector2)transform.up * MoveForce * Time.deltaTime;
        }

        if (Input.GetKey(LeftKey))
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + (Time.deltaTime * RotationSpeed));
        }
        else if (Input.GetKey(RightKey))
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - (Time.deltaTime * RotationSpeed));
        }

       /* if (Input.GetKeyDown(UpKey) && PowerUp_Count > 0)
        {
            PowerUp_Count--;
            GameObject Missile = Instantiate(Missile_Prefab, transform.position + transform.up, transform.rotation);
            Missile.GetComponent<Missile>().Speed = (Vector3.Distance(Vector3.zero, rigidbody2D.velocity) + 5);
            Missile.transform.rotation = transform.rotation;
        }
        else if (Input.GetKeyDown(DownKey) && PowerUp_Count > 0)
        {
            PowerUp_Count--;
        }*/
    }
}