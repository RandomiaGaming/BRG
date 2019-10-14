using UnityEngine;
public class Bomb : MonoBehaviour
{
    private float Timer = 3f;
    private const float FlashLength = 0.25f;
    private bool StartedParticles = false;
    private SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        GetComponent<CircleCollider2D>().enabled = false;
    }
    private void Update()
    {
        if (!StartedParticles)
        {
            Timer -= Time.deltaTime;
            if (((int)(Timer / FlashLength)) % 2 != 0)
            {
                sr.color = Color.white;
            }
            else
            {
                sr.color = Color.red;
            }
            if (Timer <= 0)
            {
                StartedParticles = true;
                GetComponent<ParticleSystem>().Play();
                GetComponent<CircleCollider2D>().enabled = true;
            }
        }
        else
        {
            sr.enabled = false;
            sr.color = Color.white;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Timer <= 0)
        {
            collision.GetComponent<Player>().Stunned_Time = 3;
        }
    }
}