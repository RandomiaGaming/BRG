using UnityEngine;

public class Coin : MonoBehaviour
{
    private CircleCollider2D cd;
    private SpriteRenderer sr;
    public float CurrentTime = 0;
    private const float RegenerateTime = 3;
    private const float AnimationTime = 0.5f;
    private void Start()
    {
        CurrentTime = AnimationTime + RegenerateTime;
        cd = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (CurrentTime >= AnimationTime + RegenerateTime)
        {
            cd.enabled = true;
            sr.enabled = true;
            transform.localScale = Vector3.one;
        }
        else if (CurrentTime < AnimationTime + RegenerateTime && CurrentTime >= RegenerateTime)
        {
            CurrentTime += Time.deltaTime;
            cd.enabled = false;
            sr.enabled = true;
            transform.localScale = new Vector3((CurrentTime - RegenerateTime) / AnimationTime, (CurrentTime - RegenerateTime) / AnimationTime, 1);
        }
        else if (CurrentTime < RegenerateTime)
        {
            CurrentTime += Time.deltaTime;
            transform.localScale = Vector3.zero;
            cd.enabled = false;
            sr.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CurrentTime = 0;
            collision.gameObject.GetComponent<Player>().Bomb_Count = 5;
        }
    }
}