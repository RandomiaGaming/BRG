using UnityEngine;
public class Missile : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D RB2D;
    private void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        RB2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        RB2D.velocity = transform.up * Speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerController" && collision.GetComponent<PlayerController>().Stunned_Time < 3)
        {
            collision.GetComponent<PlayerController>().Stunned_Time = 3;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        GetComponent<ParticleSystem>().Play();
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = false;
        RB2D.constraints = RigidbodyConstraints2D.FreezeAll;
        if (collision.gameObject.tag == "PlayerController")
        {
            collision.gameObject.GetComponent<PlayerController>().Stunned_Time = 5;
        }
    }
}