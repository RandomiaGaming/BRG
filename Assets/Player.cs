using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private const float MoveForce = 4;
    private const float MaxMoveForce = 20;

    private Rigidbody2D rb;

    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;

    [Space]
    public GameObject Bomb_Prefab;
    [Space]
    public int Lap = 0;
    public int Checkpoint = 0;
    public float Next_Checkpoint_Distance = 0;
    public int Place = 0;
    public float Stunned_Time = 0;
    public int Bomb_Count = 5;
    public int Player_ID = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Stunned_Time > 0)
        {
            Stunned_Time -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (Vector3.Distance(Vector3.zero, rb.velocity) < MaxMoveForce)
            {
                rb.AddForce(transform.up * MoveForce * Time.timeScale);
            }
            if (Input.GetKey(LeftKey))
            {
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 3 * Time.timeScale);
            }
            else if (Input.GetKey(RightKey))
            {
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 3 * Time.timeScale);
            }
            if (Input.GetKeyDown(UpKey) && Bomb_Count > 0)
            {
                Bomb_Count--;
                GameObject Bomb = Instantiate(Bomb_Prefab, transform.position + transform.up, transform.rotation);
                Bomb.GetComponent<Rigidbody2D>().velocity = transform.up * ((Vector3.Distance(Vector3.zero, rb.velocity) + 5));
            }
            else if (Input.GetKeyDown(DownKey) && Bomb_Count > 0)
            {
                Bomb_Count--;
                GameObject Bomb = Instantiate(Bomb_Prefab, transform.position - transform.up, transform.rotation);
                Bomb.GetComponent<Rigidbody2D>().velocity = transform.up * -5;
            }
        }
    }
}