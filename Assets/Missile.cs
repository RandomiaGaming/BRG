using UnityEngine;
public class Missile : MonoBehaviour
{
    public float MoveSpeed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = transform.up * MoveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().StunnedTime = 5;
        }
        Destroy(gameObject);
    }
}