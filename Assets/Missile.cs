using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    public float MoveSpeed;
    [HideInInspector]
    public string Launcher;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = transform.up * MoveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.name != Launcher)
        {
            collision.GetComponent<Player>().StunnedTime = 5;
        }
    }
}