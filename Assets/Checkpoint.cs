using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int Checkpoint_ID = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player Collided_Player = collision.GetComponent<Player>();
            Collided_Player.Checkpoint = Checkpoint_ID;
            if (Checkpoint_ID == 0)
            {
                Collided_Player.Lap++;
            }
        }
    }
}
